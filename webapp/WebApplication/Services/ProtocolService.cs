using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using K9.WebApplication.Config;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace K9.WebApplication.Services
{
    public class ProtocolService : IProtocolService
    {
        private readonly ILogger _logger;
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<ProductPack> _productPackRepository;
        private readonly IRepository<Contact> _contactsRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<Protocol> _protocolsRepository;
        private readonly IRepository<ProtocolProduct> _protocolProductsRepository;
        private readonly IRepository<ProtocolProductPack> _protocolProductPackRepository;
        private readonly IRepository<ProtocolProtocolSection> _protocolProtocolSectionRepository;
        private readonly IRepository<ProtocolSection> _protocolSectionRepository;
        private readonly IRepository<ProtocolProtocolSectionProduct> _protocolProtocolSectionProductsRepository;
        private readonly DefaultValuesConfiguration _defaultValues;

        public ProtocolService(ILogger logger, IRepository<Product> productsRepository, IRepository<ProductPack> productPackRepository, IOptions<DefaultValuesConfiguration> defaultValues, IRepository<Contact> contactsRepository, IRepository<User> usersRepository, IRepository<Protocol> protocolsRepository, IRepository<ProtocolProduct> protocolProductsRepository, IRepository<ProtocolProductPack> protocolProductPackRepository, IRepository<ProtocolProtocolSection> protocolProtocolSectionRepository, IRepository<ProtocolSection> protocolSectionRepository, IRepository<ProtocolProtocolSectionProduct> protocolProtocolSectionProductsRepository)
        {
            _logger = logger;
            _productsRepository = productsRepository;
            _productPackRepository = productPackRepository;
            _contactsRepository = contactsRepository;
            _usersRepository = usersRepository;
            _protocolsRepository = protocolsRepository;
            _protocolProductsRepository = protocolProductsRepository;
            _protocolProductPackRepository = protocolProductPackRepository;
            _protocolProtocolSectionRepository = protocolProtocolSectionRepository;
            _protocolSectionRepository = protocolSectionRepository;
            _protocolProtocolSectionProductsRepository = protocolProtocolSectionProductsRepository;
            _defaultValues = defaultValues.Value;
        }

        public Protocol Find(int id)
        {
            var protocol = _protocolsRepository.Find(id);
            if (protocol != null)
            {
                protocol = GetFullProtocol(protocol);
            }

            return protocol;
        }

        public Protocol FindNext(int id)
        {
            var protocol = _protocolsRepository.Find(e => e.Id > id).OrderBy(e => e.Id).FirstOrDefault() ?? _protocolsRepository.GetQuery("SELECT TOP 1 * FROM [Protocol] ORDER BY [Id]").FirstOrDefault();
            if (protocol != null)
            {
                protocol = GetFullProtocol(protocol);
            }

            return protocol;
        }

        public Protocol FindPrevious(int id)
        {
            var protocol = _protocolsRepository.Find(e => e.Id < id).OrderByDescending(e => e.Id).FirstOrDefault() ?? _protocolsRepository.GetQuery("SELECT TOP 1 * FROM [Protocol] ORDER BY [Id] DESC").FirstOrDefault();
            if (protocol != null)
            {
                protocol = GetFullProtocol(protocol);
            }

            return protocol;
        }

        public Protocol Find(Guid id)
        {
            var protocol = _protocolsRepository.Find(e => e.ExternalId == id).FirstOrDefault();
            if (protocol != null)
            {
                protocol = GetFullProtocol(protocol);
            }

            return protocol;
        }

        public Protocol GetFullProtocol(Protocol protocol)
        {
            protocol.Products = _protocolProductsRepository.Find(e => e.ProtocolId == protocol.Id).ToList();
            foreach (var protocolProduct in protocol.Products)
            {
                protocolProduct.Product = _productsRepository.Find(protocolProduct.ProductId);
            }

            protocol.ProductPacks = _protocolProductPackRepository.Find(e => e.ProtocolId == protocol.Id).ToList();
            foreach (var protocolProductPack in protocol.ProductPacks)
            {
                protocolProductPack.ProductPack = _productPackRepository.Find(protocolProductPack.ProductPackId);
            }

            protocol.ProtocolSections = _protocolProtocolSectionRepository.Find(e => e.ProtocolId == protocol.Id).ToList();
            foreach (var section in protocol.ProtocolSections)
            {
                section.ProtocolSection = _protocolSectionRepository.Find(section.ProtocolSectionId);

                section.ProtocolSectionProducts =
                    _protocolProtocolSectionProductsRepository.Find(e => e.ProtocolProtocolSectionId == section.Id).ToList();
            }

            protocol.Contact = _contactsRepository.Find(protocol.ContactId ?? 0);
            protocol.ContactName = protocol.Contact?.FullName;

            return protocol;
        }
        
        public List<Protocol> List(bool retrieveFullProtocol = false)
        {
            var protocols = _protocolsRepository.List().Where(e => !e.IsDeleted).OrderBy(e => e.Name).ToList();

            if (retrieveFullProtocol)
            {
                var fullProtocols = new List<Protocol>();
                foreach (var protocol in protocols)
                {
                    fullProtocols.Add(GetFullProtocol(protocol));
                }

                return fullProtocols;
            }

            return protocols;
        }

        public Protocol Duplicate(int id)
        {
            var protocol = _protocolsRepository.Find(id);
            if (protocol != null)
            {
                protocol = GetFullProtocol(protocol);
            }
            var newProtocolExternalId = Guid.NewGuid();

            var newProtocol = new Protocol
            {
                Name = $"{protocol.Name} (Copy)",
                ShortDescription = protocol.ShortDescription,
                Body = protocol.Body,
                ContactId = protocol.ContactId,
                ExternalId = newProtocolExternalId
            };

            _protocolsRepository.Create(newProtocol);

            // Get Id
            newProtocol = Find(newProtocolExternalId);

            // Copy products
            foreach (var protocolProduct in protocol.Products)
            {
                var newProduct = new ProtocolProduct
                {
                    ProtocolId = newProtocol.Id,
                    ProductId = protocolProduct.Id
                };

                _protocolProductsRepository.Create(newProduct);
            }

            // Copy product packs
            foreach (var protocolProductPack in protocol.ProductPacks)
            {
                var newProductPack = new ProtocolProductPack
                {
                    ProtocolId = newProtocol.Id,
                    ProductPackId = protocolProductPack.Id
                };

                _protocolProductPackRepository.Create(newProductPack);
            }

            // Copy sections
            foreach (var section in protocol.ProtocolSections)
            {
                var newProtocolSection = new ProtocolProtocolSection
                {
                    ProtocolId = newProtocol.Id,
                    ProtocolSectionId = section.ProtocolSectionId
                };

                _protocolProtocolSectionRepository.Create(newProtocolSection);

                // Copy products and product packs
                var protocolSection =
                    _protocolProtocolSectionRepository.Find(e => e.ProtocolSectionId == section.ProtocolSectionId).FirstOrDefault();

                var sectionProducts = _protocolProtocolSectionProductsRepository.Find(e =>
                    e.ProtocolProtocolSectionId == protocolSection.ProtocolSectionId);

                foreach (var product in sectionProducts)
                {
                    // ProtocolId = newProtocol.Id,
                    var newProtocolSectionProduct = new ProtocolProtocolSectionProduct
                    {
                        ProtocolProtocolSectionId = protocolSection.ProtocolSectionId,
                        ProductId = product.ProductId
                    };

                    _protocolProtocolSectionProductsRepository.Create(newProtocolSectionProduct);
                }
            }

            return newProtocol;
        }

        public void DeleteChildRecords(int id)
        {
            var protocol = Find(id);

            foreach (var product in protocol.Products)
            {
                _protocolProductsRepository.Delete(product.Id);
            }

            foreach (var pack in protocol.ProductPacks)
            {
                _protocolProductPackRepository.Delete(pack.Id);
            }

            foreach (var section in protocol.ProtocolSections)
            {
                _protocolProtocolSectionRepository.Delete(section.Id);
            }
        }

        public void AddDefaultSections(int id)
        {
            var protocol = Find(id);
            AddDefaultSections(protocol);
        }
        
        private void AddDefaultSections(Protocol protocol)
        {
            if (!protocol.ProtocolSections.Any())
            {
                var defaultSections = _protocolSectionRepository.List();

                // Copy default sections
                foreach (var section in defaultSections)
                {
                    var newProtocolSection = new ProtocolProtocolSection
                    {
                        ProtocolId = protocol.Id,
                        ProtocolSectionId = section.Id
                    };

                    _protocolProtocolSectionRepository.Create(newProtocolSection);
                }
            }
        }
    }
}
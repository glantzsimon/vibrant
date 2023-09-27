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
        private readonly IRepository<ProtocolSection> _protocolProtocolSectionRepository;
        private readonly IRepository<Section> _protocolSectionRepository;
        private readonly IRepository<ProtocolSectionProduct> _protocolProtocolSectionProductsRepository;
        private readonly IRepository<ProductPackProduct> _productPackProductRepository;
        private readonly IRepository<ProtocolActivity> _protocolActivitiesRepository;
        private readonly IRepository<Activity> _activitiesRepository;
        private readonly DefaultValuesConfiguration _defaultValues;

        public ProtocolService(ILogger logger, IRepository<Product> productsRepository, IRepository<ProductPack> productPackRepository, IOptions<DefaultValuesConfiguration> defaultValues, IRepository<Contact> contactsRepository, IRepository<User> usersRepository, IRepository<Protocol> protocolsRepository, IRepository<ProtocolProduct> protocolProductsRepository, IRepository<ProtocolProductPack> protocolProductPackRepository, IRepository<ProtocolSection> protocolProtocolSectionRepository, IRepository<Section> protocolSectionRepository, IRepository<ProtocolSectionProduct> protocolProtocolSectionProductsRepository, IRepository<ProductPackProduct> productPackProductRepository, IRepository<ProtocolActivity> protocolActivitiesRepository, IRepository<Activity> activitiesRepository)
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
            _productPackProductRepository = productPackProductRepository;
            _protocolActivitiesRepository = protocolActivitiesRepository;
            _activitiesRepository = activitiesRepository;
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
            protocol.Activities = _protocolActivitiesRepository.Find(e => e.ProtocolId == protocol.Id).ToList();
            foreach (var protocolActivity in protocol.Activities)
            {
                protocolActivity.Activity = _activitiesRepository.Find(protocolActivity.ActivityId);
            }

            protocol.Products = _protocolProductsRepository.Find(e => e.ProtocolId == protocol.Id).ToList();
            foreach (var protocolProduct in protocol.Products)
            {
                protocolProduct.Product = _productsRepository.Find(protocolProduct.ProductId);
            }

            protocol.ProductPacks = _protocolProductPackRepository.Find(e => e.ProtocolId == protocol.Id).ToList();
            foreach (var protocolProductPack in protocol.ProductPacks)
            {
                protocolProductPack.ProductPack = _productPackRepository.Find(protocolProductPack.ProductPackId);

                protocolProductPack.ProductPack.Products =
                    _productPackProductRepository.Find(e => e.ProductPackId == protocolProductPack.ProductPack.Id).ToList();

                foreach (var productPackProduct in protocolProductPack.ProductPack.Products)
                {
                    productPackProduct.Product = _productsRepository.Find(productPackProduct.ProductId);
                }
            }

            protocol.ProtocolSections = _protocolProtocolSectionRepository.Find(e => e.ProtocolId == protocol.Id).ToList();
            foreach (var section in protocol.ProtocolSections)
            {
                section.Section = _protocolSectionRepository.Find(section.SectionId);

                section.ProtocolSectionProducts =
                    _protocolProtocolSectionProductsRepository.Find(e => e.ProtocolSectionId == section.Id).ToList();

                foreach (var protocolProtocolSectionProduct in section.ProtocolSectionProducts)
                {
                    protocolProtocolSectionProduct.Product =
                        _productsRepository.Find(protocolProtocolSectionProduct.ProductId);
                }
            }

            protocol.Contact = _contactsRepository.Find(protocol.ContactId ?? 0);
            protocol.ContactName = protocol.Contact?.FullName;

            return protocol;
        }

        public Protocol GetProtocolWithProtocolSections(Guid id)
        {
            var protocol = _protocolsRepository.Find(e => e.ExternalId == id).FirstOrDefault();
            return GetProtocolWithProtocolSections(protocol?.Id ?? 0);
        }

        public Protocol GetProtocolWithProtocolSections(int id)
        {
            var protocol = _protocolsRepository.Find(id);
            if (protocol != null)
            {
                protocol = GetFullProtocol(protocol);
            }

            foreach (var protocolProtocolSection in protocol.ProtocolSections)
            {
                foreach (var protocolProduct in protocol.Products)
                {
                    var existing = protocolProtocolSection.ProtocolSectionProducts.FirstOrDefault(e =>
                        e.ProductId == protocolProduct.ProductId);

                    if (existing != null)
                    {
                        existing.IsVisible =
                            protocolProduct.Product.CheckRecommendations(
                                protocolProtocolSection.Section.Recommendations);
                    }
                    else
                    {
                        var protocolSectionProduct = new ProtocolSectionProduct
                        {
                            ProtocolSectionId = protocolProtocolSection.Id,
                            ProductId = protocolProduct.ProductId,
                            Product = protocolProduct.Product,
                            IsVisible = protocolProduct.Product.CheckRecommendations(protocolProtocolSection.Section.Recommendations)
                        };
                        protocolProtocolSection.ProtocolSectionProducts.Add(protocolSectionProduct);
                    }
                }
            }

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
                var newProtocolSection = new ProtocolSection
                {
                    ProtocolId = newProtocol.Id,
                    SectionId = section.SectionId
                };

                _protocolProtocolSectionRepository.Create(newProtocolSection);

                // Copy products and product packs
                var protocolSection =
                    _protocolProtocolSectionRepository.Find(e => e.SectionId == section.SectionId).FirstOrDefault();

                var sectionProducts = _protocolProtocolSectionProductsRepository.Find(e =>
                    e.ProtocolSectionId == protocolSection.SectionId);

                foreach (var product in sectionProducts)
                {
                    // ProtocolId = newProtocol.Id,
                    var newProtocolSectionProduct = new ProtocolSectionProduct
                    {
                        ProtocolSectionId = protocolSection.SectionId,
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

        public void UpdateSectionDetails(Protocol protocol)
        {
            foreach (var protocolProtocolSection in protocol.ProtocolSections)
            {
                foreach (var protocolProduct in protocolProtocolSection.ProtocolSectionProducts)
                {
                    var items = _protocolProtocolSectionProductsRepository.Find(e =>
                        e.ProtocolSectionId == protocolProtocolSection.Id && e.ProductId == protocolProduct.Product.Id);
                    var item = items.FirstOrDefault();
                    var product = _productsRepository.Find(protocolProduct.Product.Id);

                    if (protocolProduct.Amount == 0 && !items.Any())
                    {
                        continue;
                    }
                    if (protocolProduct.Amount > 0 && !items.Any())
                    {
                        // Create new
                        var newItem = new ProtocolSectionProduct
                        {
                            ProtocolSectionId = protocolProtocolSection.Id,
                            ProductId = protocolProduct.Product.Id,
                            Amount = protocolProduct.Amount
                        };

                        var sectionMessage =
                            $"{nameof(Product.ProductName)}: {product.Name}, {nameof(ProtocolSection.SectionName)}: {protocolProtocolSection.Section.Name}";
                        var acceptableMessage = string.Format(Globalisation.Dictionary.AcceptableValuesMessage,
                        product.MinDosage, product.MaxDosage);

                        if (newItem.Amount > product.MaxDosage)
                        {
                            throw new ArgumentOutOfRangeException("Amount", $"{Globalisation.Dictionary.ValueTooHighException} {acceptableMessage} {sectionMessage}"); ;
                        }

                        if (newItem.Amount < product.MinDosage)
                        {
                            throw new ArgumentOutOfRangeException("Amount", $"{Globalisation.Dictionary.ValueTooLowException} {acceptableMessage} {sectionMessage}"); ;
                        }

                        _protocolProtocolSectionProductsRepository.Create(newItem);
                    }
                    else if (items.Any() && protocolProduct.Amount == 0)
                    {
                        // Remove item
                        _protocolProtocolSectionProductsRepository.Delete(item.Id);
                    }
                    else if (items.Any() && item.Amount != protocolProduct.Amount)
                    {
                        // Change amount
                        item.Amount = protocolProduct.Amount;
                        _protocolProtocolSectionProductsRepository.Update(item);
                    }
                }
            }
        }

        private void AddDefaultSections(Protocol protocol)
        {
            if (!protocol.ProtocolSections.Any())
            {
                var defaultSections = _protocolSectionRepository.List();

                // Copy default sections
                foreach (var section in defaultSections)
                {
                    var newProtocolSection = new ProtocolSection
                    {
                        ProtocolId = protocol.Id,
                        SectionId = section.Id
                    };

                    _protocolProtocolSectionRepository.Create(newProtocolSection);
                }
            }
        }
    }
}
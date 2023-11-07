using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using WebMatrix.WebData;

namespace K9.WebApplication.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<Order> _ordersRepository;
        private readonly IRepository<OrderProduct> _orderProductsRepository;
        private readonly IRepository<OrderProductPack> _orderProductPacksRepository;

        private readonly IRepository<Client> _clientsRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly IOrderService _orderService;
        private readonly IClientService _clientService;

        public ShoppingCartService(IRepository<Order> ordersRepository, IRepository<OrderProduct> orderProductsRepository, IRepository<OrderProductPack> orderProductPacksRepository, IRepository<Client> clientsRepository, IRepository<User> usersRepository, IOrderService orderService, IClientService clientService)
        {
            _ordersRepository = ordersRepository;
            _orderProductsRepository = orderProductsRepository;
            _orderProductPacksRepository = orderProductPacksRepository;
            _clientsRepository = clientsRepository;
            _usersRepository = usersRepository;
            _orderService = orderService;
            _clientService = clientService;
        }

        public void SetShoppingCartIsPaid()
        {
            var cart = GetShoppingCart(WebSecurity.CurrentUserId);
            cart.PaidOn = DateTime.Now;
            _ordersRepository.Update(cart);
        }

        public void AddProductToCart(int productId, int amount)
        {
            var cart = GetShoppingCart(WebSecurity.CurrentUserId);
            var existing = _orderProductsRepository.Find(e => e.OrderId == cart.Id && e.ProductId == productId)
                .FirstOrDefault();

            if (existing != null)
            {
                existing.Amount += amount;
                _orderProductsRepository.Update(existing);
            }
            else
            {
                var orderProduct = new OrderProduct
                {
                    ProductId = productId,
                    Amount = amount,
                    PriceTier = cart.Client.PriceTier,
                    OrderId = cart.Id
                };
                _orderProductsRepository.Create(orderProduct);
            }
        }

        public void UpdateProductAmount(int productId, int amount)
        {
            var cart = GetShoppingCart(WebSecurity.CurrentUserId);
            var orderProduct = _orderProductsRepository.Find(e => e.OrderId == cart.OrderId && e.ProductId == productId)
                .FirstOrDefault();

            if (amount == 0)
            {
                _orderProductsRepository.Delete(orderProduct.Id);
            }
            else
            {
                orderProduct.Amount = amount;
                _orderProductsRepository.Update(orderProduct);
            }
        }

        public void AddProductPackToCart(int productPackId, int amount)
        {
            var cart = GetShoppingCart(WebSecurity.CurrentUserId);
            var existing = _orderProductPacksRepository.Find(e => e.OrderId == cart.Id && e.ProductPackId == productPackId)
                .FirstOrDefault();

            if (existing != null)
            {
                existing.Amount += amount;
                _orderProductPacksRepository.Update(existing);
            }
            else
            {
                var orderProductPack = new OrderProductPack
                {
                    ProductPackId = productPackId,
                    Amount = amount,
                    PriceTier = cart.Client.PriceTier,
                    OrderId = cart.Id
                };
                _orderProductPacksRepository.Create(orderProductPack);
            }
        }

        public void UpdateProductPackAmount(int productPackId, int amount)
        {
            var cart = GetShoppingCart(WebSecurity.CurrentUserId);
            var orderProductPack = _orderProductPacksRepository.Find(e => e.OrderId == cart.OrderId && e.ProductPackId == productPackId)
                .FirstOrDefault();

            if (amount == 0)
            {
                _orderProductPacksRepository.Delete(orderProductPack.Id);
            }
            else
            {
                orderProductPack.Amount = amount;
                _orderProductPacksRepository.Update(orderProductPack);
            }
        }

        public Order GetShoppingCart(int userId)
        {
            var shoppingCart = _ordersRepository
                .Find(e => e.UserId == userId && e.OrderType == EOrderType.ShoppingCart).FirstOrDefault();

            if (shoppingCart == null)
            {
                var user = _usersRepository.Find(userId);
                var shoppingCartId = Guid.NewGuid();
                var client = _clientsRepository.Find(e => e.UserId == userId).FirstOrDefault();

                if (client == null)
                {
                    client = _clientsRepository.Find(e => e.EmailAddress == user.EmailAddress).FirstOrDefault();

                    if (client == null)
                    {
                        client = _clientService.GetOrCreateClient("", user.FullName, user.EmailAddress,
                            user.PhoneNumber,
                            user.Id);
                    }
                }

                shoppingCart = new Order
                {
                    UserId = userId,
                    ClientId = client?.Id,
                    Products = new List<OrderProduct>(),
                    ProductPacks = new List<OrderProductPack>(),
                    ExternalId = shoppingCartId,
                    OrderType = EOrderType.ShoppingCart,
                    Name = $"{client.Name} - {Globalisation.Dictionary.ShoppingCart}",
                    FullName = $"{client.Name} - {Globalisation.Dictionary.ShoppingCart}",
                    ShortDescription = Globalisation.Dictionary.ShoppingCart,
                    RequestedOn = DateTime.Today,
                    DueBy = DateTime.Today.AddDays(7)
                };

                

                _ordersRepository.Create(shoppingCart);
                shoppingCart = _ordersRepository.Find(e => e.ExternalId == shoppingCartId).First();
            }

            return _orderService.Find(shoppingCart.Id);
        }
    }
}
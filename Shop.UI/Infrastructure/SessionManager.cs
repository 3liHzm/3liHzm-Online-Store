using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.UI.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private const string KeyCart = "cart";
        private const string KeyCustomerInfo = "customerInfo";
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public void AddCustomerInfo(CustomerInfo customer)
        {
            var stringObj = JsonConvert.SerializeObject(customer);

            _session.SetString(KeyCustomerInfo, stringObj);

        }

        public void AddProduct(CartProduct cartProduct)
        {
            var cartList = new List<CartProduct>();
            var stringObj = _session.GetString(KeyCart);
            if (!string.IsNullOrEmpty(stringObj))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObj);
            }
            if (cartList.Any(s => s.StockId == cartProduct.StockId)) //if the product is alredy in cart
            {
                cartList.Find(s => s.StockId == cartProduct.StockId).Qty += cartProduct.Qty;  //add the new qty to it
            }
            else
            {
                cartList.Add(cartProduct);
            }


            stringObj = JsonConvert.SerializeObject(cartList);

            _session.SetString(KeyCart, stringObj);
        }

        public void CleanCart()
        {
            _session.Remove(KeyCart);
        }

        public IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector)
        {
            var stringObj = _session.GetString(KeyCart);

            if (string.IsNullOrEmpty(stringObj))
            {
                return new List<TResult>() ;
            }

            var cartList = JsonConvert.DeserializeObject<IEnumerable<CartProduct>>(stringObj);
            return cartList.Select(selector);
        }

        public CustomerInfo GetCustomerInfo()
        {
            var stringObj = _session.GetString(KeyCustomerInfo);

            if (string.IsNullOrEmpty(stringObj))
            {
                return null;
            }

            var customerInfo = JsonConvert.DeserializeObject<CustomerInfo>(stringObj);
            return customerInfo;
        }

        public string GetId() => _session.Id;

        public void RemoveProduct(int stockId, int qty)
        {
            var cartList = new List<CartProduct>();
            var stringObj = _session.GetString(KeyCart);
            if (string.IsNullOrEmpty(stringObj)) return;

            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObj);

            if (!cartList.Any(s => s.StockId == stockId)) return; //if the product is alredy in cart

            var product = cartList.First(s => s.StockId == stockId);  //remove the new qty to it
            product.Qty -= qty;

            if (product.Qty <= 0)
            {
                cartList.Remove(product);
            }
            stringObj = JsonConvert.SerializeObject(cartList);

            _session.SetString(KeyCart, stringObj);
            
        }
    }
}

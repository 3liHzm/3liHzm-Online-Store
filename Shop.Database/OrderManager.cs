using Microsoft.EntityFrameworkCore;
using Shop.Domain.Enums;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shop.Database
{
    public class OrderManager : IOrderManager
    {
        private readonly ApplicationDbContext _ctx;
        public OrderManager(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public Task<int> CreatOrder(Order order)
        {
            _ctx.Orders.Add(order);

            return  _ctx.SaveChangesAsync();
        }

        private TResult GetOrder<TResult>(
           Expression<Func<Order, bool>> condition,
            Func<Order, TResult> selector)
        {
            return _ctx.Orders.
               Where(condition)
               .Include(condition=>condition.OrderStocks)
               .ThenInclude(condition => condition.Stock)
               .ThenInclude(condition => condition.Product)
               .Select(selector)
               .FirstOrDefault();
        }

        public TResult GetOrderById<TResult>(int id, Func<Order, TResult> selector)
        {
            return GetOrder( order => order.Id == id, selector);
            //return _ctx.Orders.
            //   Where(s => s.Id == id)
            //   .Include(s => s.OrderStocks)
            //   .ThenInclude(s => s.Stock)
            //   .ThenInclude(s => s.Product)
            //   .AsEnumerable()
            //   .Select(selector)
            //   .FirstOrDefault();
        }

        public TResult GetOrderByRef<TResult>(string reference, Func<Order, TResult> selector)
        {
            return GetOrder(order => order.OrderRef == reference, selector);
            //return _ctx.Orders.
            // Where(s => s.OrderRef == reference)
            // .Include(s => s.OrderStocks)
            // .ThenInclude(s => s.Stock)
            // .ThenInclude(s => s.Product)
            // .AsEnumerable()
            // .Select(selector)
            // .FirstOrDefault();
        }

        public bool OrderRefIsExist(string reference)
        {
           return _ctx.Orders.Any(s => s.OrderRef == reference);
        }

        public IEnumerable<TResult> GetOrdersByStatus<TResult>(OrderStatus status, Func<Order, TResult> selector)
        {
            return _ctx.Orders.
                Where(s => s.Status == status)
               .Select(selector).ToList();
        }

        public Task<int> UpdateOrder(int id)
        {
           _ctx.Orders.FirstOrDefault(s => s.Id == id).Status++;
            
            return  _ctx.SaveChangesAsync() ;
        }
    }
}

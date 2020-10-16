using Shop.Application;
using Shop.Database;
using Shop.Domain.Infrastructure;
using Shop.UI.Infrastructure;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            //@this.AddTransient<CreateUser>();

            //@this.AddTransient<Shop.Application.Orders.GetOrder>();
            //@this.AddTransient<CreateOrder>();


            //@this.AddTransient<Shop.Application.OrdersAdmin.GetOrder>();
            //@this.AddTransient<GetOrders>();
            //@this.AddTransient<UpdateOrder>();


            //@this.AddTransient<Shop.Application.Products.GetProduct>();
            //@this.AddTransient<Shop.Application.Products.GetProducts>();


            //@this.AddTransient<CreateProduct>();
            //@this.AddTransient<DeleteProduct>();
            //@this.AddTransient<UpdateProduct>();
            //@this.AddTransient<Shop.Application.ProductsAdmin.GetProduct>();
            //@this.AddTransient<Shop.Application.ProductsAdmin.GetProducts>();


            //@this.AddTransient<CreateStock>();
            //@this.AddTransient<DeletStock>();
            //@this.AddTransient<GetStocks>();
            //@this.AddTransient<UpdateStock>();


            //@this.AddTransient<GetCart>();
            //@this.AddTransient<AddToCart>();
            //@this.AddTransient<RemoveFromCart>();
            //  @this.AddTransient<Shop.Application.Cart.GetOrder>();

            //@this.AddTransient<AddCustomerInfo>();
            //@this.AddTransient<GetCustomerInfo>();

            var serviceType = typeof(Service);
            var defineTypes = serviceType.Assembly.DefinedTypes;

           var services = defineTypes.Where(s => s.GetTypeInfo().GetCustomAttribute<Service>() != null);

            foreach (var service in services)
            {
                @this.AddTransient(service);
            }

            @this.AddTransient<IStockManager, StockManager>();
            @this.AddTransient<IOrderManager, OrderManager>();
            @this.AddTransient<IProductManager, ProductManager>();
            @this.AddTransient<ICatagoryManager, CatagoryManager>();
         //   @this.AddTransient<IUserManager, UserManager>();
            @this.AddScoped<ISessionManager, SessionManager>();


            return @this;
        }
    }
}

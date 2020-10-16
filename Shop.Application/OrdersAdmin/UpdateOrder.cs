using Shop.Domain.Infrastructure;
using System.Threading.Tasks;

namespace Shop.Application.OrdersAdmin
{
    [Service]
    public class UpdateOrder
    {
        private readonly IOrderManager _orderManager;

        public UpdateOrder(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public Task<int> Do(int id)
        {
            return _orderManager.UpdateOrder(id);
        }
    }
}

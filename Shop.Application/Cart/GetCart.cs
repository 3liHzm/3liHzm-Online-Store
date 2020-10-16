using Shop.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Application.Cart
{
    [Service]
    public class GetCart
    {
 
        private readonly ISessionManager _sessionManager;

        public GetCart(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;

        }

        public class Response
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public decimal TheValue { get; set; }
            public int Qty { get; set; }
            public int StockId { get; set; }
            public string ImgUrl { get; set; }

        }
        public IEnumerable<Response> Do()
        {
            return _sessionManager.GetCart(s => new Response
            {
                Name = s.ProductName,
                Value = $"${ s.TheValue.ToString("N2") }",
                TheValue = s.TheValue,
                Qty = s.Qty,
                StockId = s.StockId,
                ImgUrl = s.ImgUrl
            });
        }    

    }
}


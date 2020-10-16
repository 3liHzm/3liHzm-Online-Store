using FluentValidation;
using Shop.Application.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.ValidationContexts
{
    public class AddCustomerInfoValidation : AbstractValidator<AddCustomerInfo.Request>
    {
        public AddCustomerInfoValidation()
        {
            RuleFor(s => s.FirstName).NotNull();
            RuleFor(s => s.LastName).NotNull();
            RuleFor(s => s.PhoneNumber).NotNull().Length(11);
            RuleFor(s => s.City).NotNull();
            RuleFor(s => s.Address1).NotNull();
 
        }
    }
}

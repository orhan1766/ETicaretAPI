
using Business.Handlers.Customers.Commands;
using FluentValidation;

namespace Business.Handlers.Customers.ValidationRules
{

    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.CreatedDate).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Orders).NotEmpty();

        }
    }
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(x => x.CreatedDate).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Orders).NotEmpty();

        }
    }
}
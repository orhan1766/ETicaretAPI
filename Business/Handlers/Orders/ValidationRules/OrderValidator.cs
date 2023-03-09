
using Business.Handlers.Orders.Commands;
using FluentValidation;

namespace Business.Handlers.Orders.ValidationRules
{

    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.CreatedDate).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Products).NotEmpty();

        }
    }
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.CreatedDate).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Products).NotEmpty();

        }
    }
}
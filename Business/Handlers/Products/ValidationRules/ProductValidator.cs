
using Business.Handlers.Products.Commands;
using FluentValidation;

namespace Business.Handlers.Products.ValidationRules
{

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.CreatedDate).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Stock).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.Orders).NotEmpty();

        }
    }
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.CreatedDate).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Stock).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.Orders).NotEmpty();

        }
    }
}
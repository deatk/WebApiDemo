using FluentValidation;
using WebApiDemoModels.Enums;
using WebApiDemoModels.Requests;

namespace WebApiDemoModels.Validators
{
    public class PizzaRequestValidator : AbstractValidator<CreatePizzaRequest>
    {
        public PizzaRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Ingredients)
                .NotEmpty().WithMessage("At least one ingredient is required")
                .Must(x => !x.Contains(Toppings.Pineapple)).WithMessage("Don't you dare adding Pineapple on Pizza!")
            ;
        }
    }
}
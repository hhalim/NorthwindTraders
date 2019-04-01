using FluentValidation;
using FluentValidation.Validators;

namespace Northwind.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.Customer.CustomerId).MaximumLength(5).NotEmpty();
            RuleFor(x => x.Customer.Address).MaximumLength(60);
            RuleFor(x => x.Customer.City).MaximumLength(15);
            RuleFor(x => x.Customer.CompanyName).MaximumLength(40).NotEmpty();
            RuleFor(x => x.Customer.ContactName).MaximumLength(30);
            RuleFor(x => x.Customer.ContactTitle).MaximumLength(30);
            RuleFor(x => x.Customer.Country).MaximumLength(15);
            RuleFor(x => x.Customer.Fax).MaximumLength(24).NotEmpty();
            RuleFor(x => x.Customer.Phone).MaximumLength(24).NotEmpty();
            RuleFor(x => x.Customer.PostalCode).MaximumLength(10);
            RuleFor(x => x.Customer.Region).MaximumLength(15);

            RuleFor(c => c.Customer.PostalCode).Matches(@"^\d{4}$")
                .When(c => c.Customer.Country == "Australia")
                .WithMessage("Australian Postcodes have 4 digits");

            RuleFor(c => c.Customer.Phone)
                .Must(HaveQueenslandLandLine)
                .When(c => c.Customer.Country == "Australia" && c.Customer.PostalCode.StartsWith("4"))
                .WithMessage("Customers in QLD require at least one QLD landline.");
        }

        private static bool HaveQueenslandLandLine(UpdateCustomerCommand model, string phoneValue, PropertyValidatorContext ctx)
        {
            return model.Customer.Phone.StartsWith("07") || model.Customer.Fax.StartsWith("07");
        }
    }
}

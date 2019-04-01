using MediatR;
using Northwind.Domain.Entities;

namespace Northwind.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest
    {
        public Customer Customer { get; }

        public UpdateCustomerCommand(Customer customer)
        {
            this.Customer = customer;
        }
    }
}

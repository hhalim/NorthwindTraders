using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Exceptions;
using Northwind.Domain.Entities;
using Northwind.Persistence;

namespace Northwind.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Unit>
    {
        private readonly NorthwindDbContext _context;

        public UpdateCustomerCommandHandler(NorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers
                .SingleOrDefaultAsync(c => c.CustomerId == request.Customer.CustomerId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Customer), request.Customer.CustomerId);
            }

            entity.Address = request.Customer.Address;
            entity.City = request.Customer.City;
            entity.CompanyName = request.Customer.CompanyName;
            entity.ContactName = request.Customer.ContactName;
            entity.ContactTitle = request.Customer.ContactTitle;
            entity.Country = request.Customer.Country;
            entity.Fax = request.Customer.Fax;
            entity.Phone = request.Customer.Phone;
            entity.PostalCode = request.Customer.PostalCode;

            _context.Customers.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

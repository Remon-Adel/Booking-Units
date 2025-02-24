using Booking.Core.Entities;
using Booking.Core.Repositories;
using Booking.Core.Specifications;
using Booking.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Repository
{
    class GenericRepository<T>: IGenericRepository<T> where T :BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> Spec)
       => await ApplySpecification(Spec).ToListAsync();

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> Spec)
        => await ApplySpecification(Spec).FirstOrDefaultAsync();

       


        private IQueryable<T> ApplySpecification(ISpecification<T> Spec)
       => SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), Spec);
    }
}

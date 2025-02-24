using Booking.Core.Entities;
using Booking.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Repositories
{
    public interface IGenericRepository<T> where T :BaseEntity
    {
        //Task<IEnumerable<T>> GetAllAsync();
        //Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> Spec);
        Task<T> GetByIdWithSpecAsync(ISpecification<T> Spec);

    }
}

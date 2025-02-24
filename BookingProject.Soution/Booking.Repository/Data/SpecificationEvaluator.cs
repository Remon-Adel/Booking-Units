using Booking.Core.Entities;
using Booking.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Repository.Data
{
    class SpecificationEvaluator<TEntity> where TEntity:BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery, ISpecification<TEntity> Spec)
        {
            var Query = InputQuery;// await _Context.Set<Residential>()

            if (Spec.Criteria != null)
                Query = Query.Where(Spec.Criteria);//Where(R => R.Id == 4)
            // await _Context.Set<Residential>().Where(R => R.Id == 4)


            Query = Spec.Includes.Aggregate(Query, (Currentquery, include) => Currentquery.Include(include));

            return Query;
        }
    }
}

using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }


        public async Task<List<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            // return await ApplySpecification(spec).ToListAsync();
            return await SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec).ToListAsync();
        }
        public async Task<T> GetWithSpecAsync(ISpecification<T> spec)
        {
            // return await ApplySpecification(spec).FirstOrDefaultAsync();
            return await SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}

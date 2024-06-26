﻿using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _storeContext;

        public ProductRepository(StoreContext storeContext) 
        {
            _storeContext = storeContext;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _storeContext.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _storeContext.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();
        }

        public Task<List<ProductBrand>> GetProductBrandsAsync()
        {
            return _storeContext.ProductBrands.ToListAsync();
        }

        public Task<List<ProductType>> GetProductTypesAsync()
        {
            return _storeContext.ProductTypes.ToListAsync();
        }
    }
}

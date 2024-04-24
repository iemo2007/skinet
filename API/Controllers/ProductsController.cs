using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> productRepository, 
            IGenericRepository<ProductBrand> productBrandRepository, 
            IGenericRepository<ProductType> productTypeRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetProductDTO>>> GetProducts()
        {
            var spec = new ProductsWithBrandsAndTypesSpecification();

            List<Product> products = await _productRepository.GetAllWithSpecAsync(spec);
            List<GetProductDTO> productDTOs = _mapper.Map<List<GetProductDTO>>(products);
            //List<GetProductDTO> productDTOs = products.Select(product => new GetProductDTO
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Description = product.Description,
            //    PictureUrl = product.PictureUrl,
            //    Price = product.Price,
            //    ProductBrandId = product.ProductBrandId,
            //    ProductBrandName = product.ProductBrand.Name,
            //    ProductTypeId = product.ProductTypeId,
            //    ProductTypeName = product.ProductType.Name
            //}).ToList();
            return Ok(productDTOs);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetProductDTO>> GetProduct(int id)
        {
            var spec = new ProductsWithBrandsAndTypesSpecification(id);
            Product product =  await _productRepository.GetWithSpecAsync(spec);
            GetProductDTO productDTO = _mapper.Map<GetProductDTO>(product);

            //GetProductDTO productDTO = new GetProductDTO
            //{
            //    Id = id,
            //    Name = product.Name,
            //    Description = product.Description,
            //    PictureUrl = product.PictureUrl,
            //    Price = product.Price,
            //    ProductBrandId = product.ProductBrandId,
            //    ProductBrandName = product.ProductBrand.Name,
            //    ProductTypeId = product.ProductTypeId,
            //    ProductTypeName = product.ProductType.Name
            //};
            return productDTO;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            return await _productBrandRepository.GetAllAsync();
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            return await _productTypeRepository.GetAllAsync();
        }
    }
}

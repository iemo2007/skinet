using AutoMapper;
using AutoMapper.Execution;
using Core.DTOs;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, GetProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, GetProductDTO destination, string destMember, ResolutionContext context)
        {
            return $"{_configuration["APIUrl"]}/{source.PictureUrl}";
        }
    }
}

﻿using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Products.Commands;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Products.Handlers.Commands_Handlers
{
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public ProductUpdateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product is null)
            {
                throw new ApplicationException("Entity could not be found");
            }

            product.Update(request.Name, request.Description, request.Price, request.Stock, request.Image, request.CategoryId);
            return await _productRepository.UpdateAsync(product);
        }
    }
}

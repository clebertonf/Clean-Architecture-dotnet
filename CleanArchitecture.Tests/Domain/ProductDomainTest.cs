﻿using CleanArchitecture.Domain.Validation;
using System.Linq;
using System;
using Xunit;
using FluentAssertions;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Tests.Domain
{
    public class ProductDomainTest
    {
        [Fact]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product("Product test", "Description test", 99, 10, "image test");
            action.Should()
                  .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_WithIValidId_ResultException()
        {
            Action action = () => new Product(-2, "Product test", "Description test", 99, 10, "image test");
            action.Should()
                  .ThrowExactly<DomainExceptionValidation>()
                  .WithMessage("Invalid Id Value!");
        }

        [Fact]
        public void CreateProduct_WithEmptyName_ResultException()
        {
            Action action = () => new Product(2, string.Empty, "Description test", 99, 10, "image test");
            action.Should()
                  .ThrowExactly<DomainExceptionValidation>()
                  .WithMessage("Name is required!");
        }

        [Fact]
        public void CreateProduct_NameWithLessThanThreeCharacters_ResultException()
        {
            Action action = () => new Product(2, "pr", "Description test", 99, 10, "image test");
            action.Should()
                  .ThrowExactly<DomainExceptionValidation>()
                  .WithMessage("Name cannot be less than 3 characters!");
        }

        [Fact]
        public void CreateProduct_WithEmptyDescription_ResultException()
        {
            Action action = () => new Product(2, "Product 2", string.Empty, 99, 10, "image test");
            action.Should()
                  .ThrowExactly<DomainExceptionValidation>()
                  .WithMessage("Description is required!");
        }

        [Fact]
        public void CreateProduct_DescriptionWithLessThanTenCharacters_ResultException()
        {
            Action action = () => new Product(2, "Product 3", "Descr", 99, 10, "image test");
            action.Should()
                  .ThrowExactly<DomainExceptionValidation>()
                  .WithMessage("Description cannot be less than 10 characters!");
        }

        [Fact]
        public void CreateProduct_WithInvalidPrice_ResultException()
        {
            Action action = () => new Product(2, "Product 3", "Description teste", -9.99m, 10, "image test");
            action.Should()
                  .ThrowExactly<DomainExceptionValidation>()
                  .WithMessage("Invalid price value.");
        }

        [Fact]
        public void CreateProduct_WithInvalidStock_ResultException()
        {
            Action action = () => new Product(2, "Product 3", "Description teste", 10, -3, "image test");
            action.Should()
                  .ThrowExactly<DomainExceptionValidation>()
                  .WithMessage("Invalid stock value.");
        }

        [Fact]
        public void CreateProduct_WithNullImage_NotResultException()
        {
            Action action = () => new Product(2, "Product 3", "Description teste", 10, 5, null);
            action.Should()
                  .NotThrow<NullReferenceException>();
        }

        [Fact]
        public void CreateProduct_ImageUrlExceedingLimit_ResultException()
        {
            Action action = () => new Product(2, "Product 3", "Description teste", 10, 5, RandonString());
            action.Should()
                  .ThrowExactly<DomainExceptionValidation>()
                  .WithMessage("Image too long, maximum is 250 charactres!");
        }

        private string RandonString()
        {
            var caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(caracteres, 251)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return result;
        }
    }
}

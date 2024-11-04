using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArchitecture.Tests.Domain
{
    public class CategoryDomainTest
    {
        [Fact]
        public void CreateCategory_WhithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Category test");
            action.Should()
                  .NotThrow<DomainExceptionValidation>();
        }
        
        [Fact]
        public void CreateCategory_WhithShortName_DomainExceptionInvalidName()
        {
            Action action = () => new Category(2, "Ca");
            action.Should()
                  .Throw<DomainExceptionValidation>()
                  .WithMessage("Name cannot be less than 3 characters!");
        }

        [Fact]
        public void CreateCategory_WhithEmptyName_DomainExceptionInvalidName()
        {
            Action action = () => new Category(2, string.Empty);
            action.Should()
                  .Throw<DomainExceptionValidation>()
                  .WithMessage("Name is required!");
        }
    }
}

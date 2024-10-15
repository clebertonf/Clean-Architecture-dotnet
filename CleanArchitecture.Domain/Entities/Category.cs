using CleanArchitecture.Domain.Validation;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }

        public Category(string name)
        {
            ValidateDomain(name);
        }

        public Category(int id, string name)
        {
            DomainExceptionValidation.When(id <= 0, "Invalid id!");
            ValidateDomain(name);
            Id = id;
        }

        public void Update(string name)
        {
            ValidateDomain(name);
        }

        public void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Name is required!");
            DomainExceptionValidation.When(name.Length <= 3, "Name cannot be less than 3 characters!");
            Name = name;
        }
    }
}

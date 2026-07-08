using Domain.Models.Base;
using Domain.ValueObjects.Products;

namespace Domain.Models.Products
{
    public partial class Product : DomainModelBase
    {
        private Product(ProductName name, ProductPrice price)
        {
            Name = name;
            Price = price;

            _initialized = true;
        }

        private Product(ProductId id, ProductName name, ProductPrice price)
        {
            Id = id;
            Name = name;
            Price = price;

            _initialized = true;
        }

        public static Product Restore(ProductId id, ProductName name, ProductPrice price)
            => new(id, name, price);

        public static Product Create(ProductName name, ProductPrice price)
            => new(name, price);
    }
}

using Domain.ValueObjects.Products;

namespace Domain.Models.Products
{
    public partial class Product
    {
        public ProductId? Id
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }

        public ProductName Name
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }

        public ProductPrice Price
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;

                OnPropertyChanged();
            }
        }
    }
}

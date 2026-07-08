using Domain.ValueObjects.PoprornShiftProducts;
using Domain.ValueObjects.Products;
using Domain.ValueObjects.Shifts;

namespace Domain.Models.PopcornShiftProducts
{
    public partial class PopcornShiftProduct
    {
        public ShiftId ShiftId
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

        public ProductId ProductId
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

        public ProductQuantity Quantity
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

using Domain.Models.Base;
using Domain.ValueObjects.PoprornShiftProducts;
using Domain.ValueObjects.Products;
using Domain.ValueObjects.Shifts;

namespace Domain.Models.PopcornShiftProducts
{
    public partial class PopcornShiftProduct : DomainModelBase
    {
        private PopcornShiftProduct(ShiftId shiftId, ProductId productId, ProductQuantity quantity)
        {
            ShiftId = shiftId;
            ProductId = productId;
            Quantity = quantity;

            _initialized = true;
        }

        public static PopcornShiftProduct Create(ShiftId shiftId, ProductId productId, ProductQuantity quantity)
            => new(shiftId, productId, quantity);
    }
}

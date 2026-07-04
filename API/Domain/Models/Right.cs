using Domain.Tools;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Models
{
    public class Right
    {
        // ctors
        private Right(int id, string code)
        {
            Id = id;
            Code = code;
        }

        public Right(string code)
        {
            SetCode(code);
        }

        // statics

        public static Right Restore(int id, string code)
            => new(id, code);

        public static Right Create(string code)
            => new(code);

        // props

        public int Id { get; private set; } = default;
        public string Code { get; private set; }

        // private setters

        [MemberNotNull(nameof(Code))]
        private void SetCode(string value)
        {
            ValidateCode(value);

            Code = value;
        }

        // public setters

        public void ChangeCode(string value)
        {
            if (Code == value)
                return;

            SetCode(value);
        }

        // validators

        private void ValidateCode(string code)
        {
            DataValidator.NullOrWhiteSpace(code, nameof(code));
            DataValidator.MaxLength(code, 50, nameof(code));
        }
    }
}

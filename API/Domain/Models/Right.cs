using Domain.Tools;

namespace Domain.Models
{
    public class Right
    {
        private Right() { }

        public Right(string code)
        {
            ChangeCode(code);
        }

        public static Right Restore(int id, string code) => new() { Id = id, Code = code };

        public int Id { get; private set; }

        public string Code { get; private set; }

        private void ValidateCode(string code)
        {
            DataValidator.NullOrWhiteSpace(code, nameof(code));
            DataValidator.MaxLength(code, 50, nameof(code));
        }

        public void ChangeCode(string value)
        {
            ValidateCode(value);

            Code = value;
        }
    }
}

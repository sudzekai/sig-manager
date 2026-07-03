using Domain.Tools;

namespace Domain.Models
{
    public class Role
    {
        private Role() { }

        public Role(string name)
        {
            ChangeName(name);
        }

        public static Role Restore(int id, string name, IEnumerable<Right> rights) => new() { Id = id, Name = name, _rights = [.. rights] };

        public int Id { get; private set; }

        public string Name { get; private set; } 

        private void ValidateName(string name)
        {
            DataValidator.NullOrWhiteSpace(name, nameof(name));
            DataValidator.MaxLength(name, 25, nameof(name));
        }

        public void ChangeName(string value)
        {
            ValidateName(value);

            Name = value;
        }

        private List<Right> _rights = [];
        public IReadOnlyCollection<Right> Rights { get => _rights; }

        public void AddRight(Right right)
        {
            if (_rights.Contains(right)) return;
            _rights.Add(right);
        }

        public void RemoveRight(Right right)
        {
            if (!_rights.Contains(right)) return;
            _rights.Remove(right);
        }
    }
}

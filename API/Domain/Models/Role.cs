using Domain.Tools;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Models
{
    public class Role
    {
        // ctors

        private Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        private Role(string name)
        {
            SetName(name);
        }

        // statics
        public static Role Restore(int id, string name) 
            => new(id, name);

        public static Role Create(string name) 
            => new(name);

        // fields

        private readonly List<Right> _rights = [];

        // props

        public int Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<Right> Rights { get => _rights; }

        // private setters

        [MemberNotNull(nameof(Name))]
        private void SetName(string value)
        {
            ValidateName(value);

            Name = value;
        }

        // public setters

        public void ChangeName(string value)
        {
            if (Name == value)
                return;

            SetName(value);
        }

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

        // validators

        private void ValidateName(string name)
        {
            DataValidator.NullOrWhiteSpace(name, nameof(name));
            DataValidator.MaxLength(name, 25, nameof(name));
        }
    }
}

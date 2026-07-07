using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Domain.Models.Shifts
{
    public partial class Shift : INotifyPropertyChanged
    {
        // fields

        private int _id;

        private string _type;

        private string _status;


        // proprs

        public int Id { get => _id; private set => _id = value; }

        public string Type
        {
            get => _type;
            [MemberNotNull(nameof(_type))]
            set
            {
                if (_type == value)
                    return;

                ValidateType(value);

                _type = value;

                OnPropertyChanged();
            }
        }

        public string Status
        {
            get => _status;
            [MemberNotNull(nameof(_status))]
            set
            {
                if (_status == value)
                    return;

                ValidateStatus(value);

                _status = value;

                OnPropertyChanged();
            }
        }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public DateTime? ClosedAt
        {
            get;
            private set
            {
                field = value;
                OnPropertyChanged();
            }
        }


        // events

        public event PropertyChangedEventHandler? PropertyChanged;


        // logic

        public void SetClosed()
        {
            Status = "closed";
            ClosedAt = DateTime.Now;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (_initialized)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                UpdatedAt = DateTime.Now;
            }
        }
    }
}

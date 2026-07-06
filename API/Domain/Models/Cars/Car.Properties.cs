using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Domain.Models.Cars
{
    public partial class Car : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _plate;
        private string _status;

        public int Id
        {
            get => _id;
            set
            {
                if (_id == value)
                    return;

                ValidateId(value);

                _id = value;

                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;

            [MemberNotNull(nameof(_name))]
            set
            {
                if (_name == value)
                    return;

                ValidateName(value);

                _name = value;

                OnPropertyChanged();
            }
        }

        public string Plate
        {
            get => _plate;

            [MemberNotNull(nameof(_plate))]
            set
            {
                if (_plate == value)
                    return;

                ValidatePlate(value);

                _plate = value;

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

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (_initialized)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

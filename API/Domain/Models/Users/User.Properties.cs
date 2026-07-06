using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Domain.Models.Users
{
    public partial class User : INotifyPropertyChanged
    {
        private int _id;
        private int _roleId;
        private string _username;
        private string _fullName;
        private string _email;
        private string _phoneNumber;
        private string _passwordHash;
        private string _phoneNumberLastFour;
        private string _verificationCode;
        private DateTime _createdAt;
        private DateTime _updatedAt;

        public int Id
        {
            get => _id;
            private set
            {
                if (_id == value)
                    return;

                _id = value;
            }
        }

        public int RoleId
        {
            get => _roleId;
            set
            {
                if (_roleId == value)
                    return;

                ValidateRoleId(value);

                _roleId = value;

                OnPropertyChanged();
            }
        }

        public string Username
        {
            get => _username;
            [MemberNotNull(nameof(_username))]
            set
            {
                if (_username == value)
                    return;

                ValidateUsername(value);

                _username = value;

                OnPropertyChanged();
            }
        }

        public string FullName
        {
            get => _fullName;
            [MemberNotNull(nameof(_fullName))]
            set
            {
                if (_fullName == value)
                    return;

                ValidateFullName(value);

                _fullName = value;

                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            [MemberNotNull(nameof(_email))]
            set
            {
                if (_email == value)
                    return;

                ValidateEmail(value);

                _email = value;

                OnPropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            [MemberNotNull(nameof(_phoneNumber), nameof(_phoneNumberLastFour))]
            set
            {
                if (value is not null
                    && _phoneNumber == value
                    && _phoneNumberLastFour == value[^4..])
                    return;

                ValidatePhoneNumber(value);

                _phoneNumber = value;
                PhoneNumberLastFour = value[^4..];

                OnPropertyChanged();
            }
        }

        public string PasswordHash
        {
            get => _passwordHash;
            [MemberNotNull(nameof(_passwordHash))]
            set
            {
                if (_passwordHash == value)
                    return;

                ValidatePasswordHash(value);

                _passwordHash = value;

                OnPropertyChanged();
            }
        }

        public string PhoneNumberLastFour
        {
            get => _phoneNumberLastFour;
            [MemberNotNull(nameof(_phoneNumberLastFour))]
            set
            {
                if (_phoneNumberLastFour == value)
                    return;

                _phoneNumberLastFour = value;

                OnPropertyChanged();
            }
        }

        public string VerificationCode
        {
            get => _verificationCode;
            [MemberNotNull(nameof(_verificationCode))]
            set
            {
                if (_verificationCode == value)
                    return;

                ValidateVerificationCode(value);

                _verificationCode = value;

                OnPropertyChanged();
            }
        }

        public DateTime CreatedAt
        {
            get => _createdAt;
            private set
            {
                if (_createdAt == value)
                    return;

                _createdAt = value;
            }
        }

        public DateTime UpdatedAt
        {
            get => _updatedAt;
            private set
            {
                if (_updatedAt == value)
                    return;

                _updatedAt = value;
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (_initialized)
            {
                UpdatedAt = DateTime.Now;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
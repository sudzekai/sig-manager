using Domain.Tools;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Models
{
    public class User
    {
        private User(int id, string username, string fullName, string email, string phoneNumber, string phoneNumberLastFour, string passwordHash, string? verificationCode, DateTime createdAt, DateTime updatedAt, int roleId)
        {
            Id = id;
            Username = username;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            PhoneNumberLastFour = phoneNumberLastFour;
            PasswordHash = passwordHash;
            VerificationCode = verificationCode;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            RoleId = roleId;
        }

        private User(string username, string fullName, string email, string phoneNumber, string passwordHash, int roleId)
        {
            SetUsername(username);
            SetFullName(fullName);
            SetEmail(email);
            SetPhoneNumber(phoneNumber);
            SetPasswordHash(passwordHash);
            SetRoleId(roleId);

            PhoneNumberLastFour = phoneNumber[^4..];
            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
        }

        // statics

        internal static User Restore(int id, string username, string fullName, string email, string phoneNumber, string phoneNumberLastFour, string passwordHash, string? verificationCode, DateTime createdAt, DateTime updatedAt, int roleId)
            => new(id, username, fullName, email, phoneNumber, phoneNumberLastFour, passwordHash, verificationCode, createdAt, updatedAt, roleId);

        public static User Create(string username, string fullName, string email, string phoneNumber, string passwordHash, int roleId)
            => new(username, fullName, email, phoneNumber, passwordHash, roleId);


        // props

        public int Id { get; private set; } = default;
        public string Username { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string PhoneNumberLastFour { get; private set; }
        public string PasswordHash { get; private set; }
        public string? VerificationCode { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public int RoleId { get; private set; }

        // private setters

        [MemberNotNull(nameof(Username))]
        private void SetUsername(string value)
        {
            ValidateUsername(value);
            Username = value;
        }

        [MemberNotNull(nameof(FullName))]
        private void SetFullName(string value)
        {
            ValidateFullName(value);

            FullName = value;
        }

        [MemberNotNull(nameof(Email))]
        private void SetEmail(string value)
        {
            ValidateEmail(value);

            Email = value;
        }

        [MemberNotNull(nameof(PhoneNumber))]
        private void SetPhoneNumber(string value)
        {
            ValidatePhoneNumber(value);

            PhoneNumber = value;
        }

        [MemberNotNull(nameof(PasswordHash))]
        private void SetPasswordHash(string value)
        {
            ValidatePasswordHash(value);

            PasswordHash = value;
        }

        [MemberNotNull(nameof(VerificationCode))]
        private void SetVerificationCode(string value)
        {
            ValidateVerificationCode(value);

            VerificationCode = value;
        }

        [MemberNotNull(nameof(UpdatedAt))]
        private void Touch()
        {
            UpdatedAt = DateTime.Now;
        }

        [MemberNotNull(nameof(RoleId))]
        public void SetRoleId(int roleId)
        {
            ValidateRoleId(roleId);

            RoleId = roleId;
        }

        // public setters

        public void ChangeUsername(string value)
        {
            if (Username == value)
                return;

            SetUsername(value);

            Touch();
        }
        public void ChangeFullName(string value)
        {
            if (FullName == value)
                return;

            SetFullName(value);

            Touch();
        }
        public void ChangeEmail(string value)
        {
            if (Email == value)
                return;

            SetEmail(value);

            Touch();
        }
        public void ChangePhoneNumber(string value)
        {
            if (PhoneNumber == value)
                return;

            SetPhoneNumber(value);

            Touch();
        }

        public void ChangePasswordHash(string value)
        {
            if (PasswordHash == value)
                return;

            SetPasswordHash(value);

            Touch();
        }

        public void ChangeVerificationCode(string value)
        {
            if (VerificationCode == value)
                return;

            SetVerificationCode(value);

            Touch();
        }

        public void ClearVerificationCode()
        {
            VerificationCode = null;

            Touch();
        }

        public void ChangeRoleId(int roleId)
        {
            if (RoleId == roleId) return;

            SetRoleId(roleId);

            Touch();
        }

        // validators

        private static void ValidateUsername(string value)
        {
            DataValidator.NullOrWhiteSpace(value, nameof(Username));
            DataValidator.MaxLength(value, 25, nameof(Username));
        }

        private static void ValidateFullName(string value)
        {
            DataValidator.NullOrWhiteSpace(value, nameof(FullName));
            DataValidator.MaxLength(value, 255, nameof(FullName));
        }

        private static void ValidateEmail(string value)
        {
            DataValidator.NullOrWhiteSpace(value, nameof(Email));
            DataValidator.MaxLength(value, 255, nameof(Email));
        }

        private static void ValidatePhoneNumber(string value)
        {
            DataValidator.NullOrWhiteSpace(value, nameof(PhoneNumber));
            DataValidator.MaxLength(value, 15, nameof(PhoneNumber));
            DataValidator.MinLength(value, 12, nameof(PhoneNumber));
        }

        private static void ValidatePasswordHash(string value)
        {
            DataValidator.NullOrWhiteSpace(value, nameof(PasswordHash));
        }

        private static void ValidateVerificationCode(string value)
        {
            DataValidator.NullOrWhiteSpace(value, nameof(VerificationCode));
            DataValidator.LengthEquals(value, 6, nameof(VerificationCode));
        }

        private static void ValidateRoleId(int roleId)
        {
            DataValidator.Min(roleId, 1, nameof(roleId));
        }
    }
}
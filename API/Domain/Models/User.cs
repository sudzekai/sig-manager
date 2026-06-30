using Domain.Tools;

namespace Domain.Models
{
    public class User
    {
        private User() { }

        public User(
            string username,
            string fullName,
            string email,
            string phoneNumber,
            string passwordHash,
            int roleId)
        {
            ChangeUsername(username);
            ChangeFullName(fullName);
            ChangeEmail(email);
            ChangePhoneNumber(phoneNumber);
            ChangePasswordHash(passwordHash);
            ChangeRoleId(roleId);

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = CreatedAt;
        }

        internal static User Restore(
            int id,
            string username,
            string fullName,
            string email,
            string phoneNumber,
            string passwordHash,
            string? verificationCode,
            DateTime createdAt,
            DateTime updatedAt,
            int roleId)
            => new User
            {
                Id = id,
                Username = username,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                PasswordHash = passwordHash,
                VerificationCode = verificationCode,
                CreatedAt = createdAt,
                UpdatedAt = updatedAt,
                RoleId = roleId
            };

        public int Id { get; private set; }

        #region Username

        public string Username { get; private set; }

        private void ValidateUsername(string value)
        {
            DataValidator.NullOrWhiteSpace(value, nameof(Username));
            DataValidator.MaxLength(value, 25, nameof(Username));
        }

        public void ChangeUsername(string value)
        {
            if (Username == value) return;

            ValidateUsername(value);

            Username = value;
            Touch();
        }

        #endregion

        #region FullName

        public string FullName { get; private set; }

        private void ValidateFullName(string value)
        {
            DataValidator.NullOrWhiteSpace(value, nameof(FullName));
            DataValidator.MaxLength(value, 255, nameof(FullName));
        }

        public void ChangeFullName(string value)
        {
            if (FullName == value) return;

            ValidateFullName(value);

            FullName = value;
            Touch();
        }

        #endregion

        #region Email

        public string Email { get; private set; }

        private void ValidateEmail(string value)
        {
            DataValidator.NullOrWhiteSpace(value, nameof(Email));
            DataValidator.MaxLength(value, 255, nameof(Email));
        }

        public void ChangeEmail(string value)
        {
            if (Email == value) return;

            ValidateEmail(value);

            Email = value;
            Touch();
        }

        #endregion

        #region PhoneNumber

        public string PhoneNumber { get; private set; }

        private void ValidatePhoneNumber(string value)
        {
            DataValidator.NullOrWhiteSpace(value, nameof(PhoneNumber));
            DataValidator.MaxLength(value, 15, nameof(PhoneNumber));
            DataValidator.MinLength(value, 12, nameof(PhoneNumber));
        }

        public void ChangePhoneNumber(string value)
        {
            if (PhoneNumber == value) return;

            ValidatePhoneNumber(value);

            PhoneNumber = value;
            Touch();
        }

        #endregion

        #region PasswordHash

        public string PasswordHash { get; private set; }

        private void ValidatePasswordHash(string value)
        {
            DataValidator.NullOrWhiteSpace(value, nameof(PasswordHash));
        }

        public void ChangePasswordHash(string value)
        {
            if (PasswordHash == value) return;

            ValidatePasswordHash(value);

            PasswordHash = value;
            Touch();
        }

        #endregion

        #region VerificationCode

        public string? VerificationCode { get; private set; }

        private void ValidateVerificationCode(string value)
        {
            DataValidator.NullOrWhiteSpace(value, nameof(VerificationCode));
            DataValidator.LengthEquals(value, 6, nameof(VerificationCode));
        }

        public void ChangeVerificationCode(string value)
        {
            if (VerificationCode == value) return;

            ValidateVerificationCode(value);

            VerificationCode = value;
            Touch();
        }

        public void ClearVerificationCode()
        {
            VerificationCode = null;
            Touch();
        }

        #endregion

        #region RoleId

        public int RoleId { get; private set; }

        private void ValidateRoleId(int roleId)
        {
            DataValidator.Min(roleId, 1, nameof(roleId));
        }

        public void ChangeRoleId(int roleId)
        {
            if (RoleId == roleId) return;

            ValidateRoleId(roleId);

            RoleId = roleId;
            Touch();
        }

        #endregion

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private void Touch()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
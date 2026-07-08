using Domain.Models.Base;
using Domain.ValueObjects.Roles;
using Domain.ValueObjects.Users;

namespace Domain.Models.Users
{
    public partial class User : DomainModelBase
    {
        private User(
            UserId id, 
            Username username, 
            UserFullName fullName, 
            UserEmail email, 
            UserPhoneNumber phoneNumber, 
            UserPasswordHash passwordHash,
            UserVerificationCode verificationCode, 
            DateTime createdAt, 
            DateTime updatedAt, 
            RoleId roleId)
        {
            Id = id;
            Username = username;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            PasswordHash = passwordHash;
            VerificationCode = verificationCode;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            RoleId = roleId;

            _initialized = true;
        }

        private User(
            Username username, 
            UserFullName fullName, 
            UserEmail email, 
            UserPhoneNumber phoneNumber, 
            UserPasswordHash passwordHash, 
            RoleId roleId)
        {
            Username = username;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            PasswordHash = passwordHash;
            RoleId = roleId;
            VerificationCode = UserVerificationCode.Empty;

            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;

            _initialized = true;
        }

        internal static User Restore(
            UserId id, 
            Username username, 
            UserFullName fullName, 
            UserEmail email, 
            UserPhoneNumber phoneNumber, 
            UserPasswordHash passwordHash, 
            UserVerificationCode verificationCode, 
            DateTime createdAt, 
            DateTime updatedAt, 
            RoleId roleId)
            => new(id, 
                username, 
                fullName, 
                email, 
                phoneNumber, 
                passwordHash, 
                verificationCode, 
                createdAt, 
                updatedAt, 
                roleId);

        public static User Create(
            Username username, 
            UserFullName fullName, 
            UserEmail email,
            UserPhoneNumber phoneNumber, 
            UserPasswordHash passwordHash, 
            RoleId roleId)
            => new(username, 
                fullName, 
                email, 
                phoneNumber, 
                passwordHash, 
                roleId);
    }
}

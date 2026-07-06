namespace Domain.Models.Users
{
    public partial class User
    {
        private User(
            int id, 
            string username, 
            string fullName, 
            string email, 
            string phoneNumber, 
            string phoneNumberLastFour, 
            string passwordHash,
            string verificationCode, 
            DateTime createdAt, 
            DateTime updatedAt, 
            int roleId)
        {
            _id = id;
            _username = username;
            _fullName = fullName;
            _email = email;
            _phoneNumber = phoneNumber;
            _phoneNumberLastFour = phoneNumberLastFour;
            _passwordHash = passwordHash;
            _verificationCode = verificationCode;
            _createdAt = createdAt;
            _updatedAt = updatedAt;
            _roleId = roleId;

            _initialized = true;
        }

        private User(
            string username, 
            string fullName, 
            string email, 
            string phoneNumber, 
            string passwordHash, 
            int roleId)
        {
            Username = username;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            PasswordHash = passwordHash;
            RoleId = roleId;
            _verificationCode = "";

            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;

            _initialized = true;
        }

        internal static User Restore(
            int id, 
            string username, 
            string fullName, 
            string email, 
            string phoneNumber, 
            string phoneNumberLastFour, 
            string passwordHash, 
            string verificationCode, 
            DateTime createdAt, 
            DateTime updatedAt, 
            int roleId)
            => new(id, 
                username, 
                fullName, 
                email, 
                phoneNumber, 
                phoneNumberLastFour, 
                passwordHash, 
                verificationCode, 
                createdAt, 
                updatedAt, 
                roleId);

        public static User Create(
            string username, 
            string fullName, 
            string email,
            string phoneNumber, 
            string passwordHash, 
            int roleId)
            => new(username, 
                fullName, 
                email, 
                phoneNumber, 
                passwordHash, 
                roleId);

        private readonly bool _initialized = false;
    }
}

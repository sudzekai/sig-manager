namespace Infrastructure.Schema.User
{
    internal static class UserSchema
    {
        public const string TableName = "users";

        public const string Id = "id";
        public const string Username = "username";
        public const string Email = "email";
        public const string FullName = "full_name";
        public const string PhoneNumber = "phone_number";
        public const string PasswordHash = "password_hash";
        public const string VerificationCode = "verification_code";
        public const string CreatedAt = "created_at";
        public const string UpdatedAt = "updated_at";
        public const string RoleId = "role_id";
    }
}

namespace Infrastructure.Schema.User
{
    internal static class UserSchema
    {
        public static readonly string TableName = "users";

        public static readonly string Id = "id";
        public static readonly string Username = "username";
        public static readonly string Email = "email";
        public static readonly string FullName = "full_name";
        public static readonly string PhoneNumber = "phone_number";
        public static readonly string PasswordHash = "password_hash";
        public static readonly string VerificationCode = "verification_code";
        public static readonly string CreatedAt = "created_at";
        public static readonly string UpdatedAt = "updated_at";
        public static readonly string RoleId = "role_id";
    }
}

namespace Infrastructure.Schema.User
{
    internal static class UserSelects
    {
        public static readonly IReadOnlyList<string> Insertation = [
            UserSchema.Username, 
            UserSchema.FullName, 
            UserSchema.PhoneNumber, 
            UserSchema.Email, 
            UserSchema.PasswordHash, 
            UserSchema.CreatedAt, 
            UserSchema.UpdatedAt, 
            UserSchema.RoleId
        ];

        public static readonly IReadOnlyList<string> Full = [
            UserSchema.Id, 
            UserSchema.Username, 
            UserSchema.FullName, 
            UserSchema.PhoneNumber, 
            UserSchema.Email, 
            UserSchema.PasswordHash, 
            UserSchema.VerificationCode, 
            UserSchema.CreatedAt, 
            UserSchema.UpdatedAt, 
            UserSchema.RoleId
        ];

        public static readonly IReadOnlyList<string> AdminInfo = [
            UserSchema.Id, 
            UserSchema.Username, 
            UserSchema.FullName, 
            UserSchema.PhoneNumber, 
            UserSchema.Email, 
            UserSchema.VerificationCode, 
            UserSchema.CreatedAt, 
            UserSchema.UpdatedAt, 
            UserSchema.RoleId
        ];

        public static readonly IReadOnlyList<string> Info = [
            UserSchema.Id, 
            UserSchema.Username, 
            UserSchema.FullName, 
            UserSchema.PhoneNumber, 
            UserSchema.Email
        ];

        public static readonly IReadOnlyList<string> Simple = [
            UserSchema.Id, 
            UserSchema.Username, 
            UserSchema.FullName
        ];
    }
}

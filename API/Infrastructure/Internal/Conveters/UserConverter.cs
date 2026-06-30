using Domain.Models;
using Infrastructure.Internal.Extensions;
using Infrastructure.Schema.User;
using System.Data;

namespace Infrastructure.Internal.Conveters
{
    internal static class UserConverter
    {
        public static User? FromReader(IDataReader reader)
        {
            if (!reader.Read())
                return null;

            var user = User.Restore(
                reader.TryGetInt32(UserSchema.Id),
                reader.TryGetString(UserSchema.Username),
                reader.TryGetString(UserSchema.FullName),
                reader.TryGetString(UserSchema.Email),
                reader.TryGetString(UserSchema.PhoneNumber),
                reader.TryGetString(UserSchema.PasswordHash),
                reader.TryGetString(UserSchema.VerificationCode),
                reader.TryGetDateTime(UserSchema.CreatedAt),
                reader.TryGetDateTime(UserSchema.UpdatedAt),
                reader.TryGetInt32(UserSchema.RoleId)
            );

            return user;
        }

        public static IReadOnlyList<User> ListFromReader(IDataReader reader)
        {
            reader.TryGetOrdinal(UserSchema.Id, out int idOrdinal);
            reader.TryGetOrdinal(UserSchema.Username, out int usernameOrdinal);
            reader.TryGetOrdinal(UserSchema.FullName, out int fullNameOrdinal);
            reader.TryGetOrdinal(UserSchema.Email, out int emailOrdinal);
            reader.TryGetOrdinal(UserSchema.PhoneNumber, out int phoneNumberOrdinal);
            reader.TryGetOrdinal(UserSchema.PasswordHash, out int passwordHashOrdinal);
            reader.TryGetOrdinal(UserSchema.VerificationCode, out int verificationCodeOrdinal);
            reader.TryGetOrdinal(UserSchema.CreatedAt, out int createdAtOrdinal);
            reader.TryGetOrdinal(UserSchema.UpdatedAt, out int updatedAtOrdinal);
            reader.TryGetOrdinal(UserSchema.RoleId, out int roleIdOrdinal);

            List<User> users = [];

            while (reader.Read())
            {
                users.Add(User.Restore(
                    reader.TryGetInt32(idOrdinal),
                    reader.TryGetString(usernameOrdinal),
                    reader.TryGetString(fullNameOrdinal),
                    reader.TryGetString(emailOrdinal),
                    reader.TryGetString(phoneNumberOrdinal),
                    reader.TryGetString(passwordHashOrdinal),
                    reader.TryGetString(verificationCodeOrdinal),
                    reader.TryGetDateTime(createdAtOrdinal),
                    reader.TryGetDateTime(updatedAtOrdinal),
                    reader.TryGetInt32(roleIdOrdinal)
                ));
            }

            return users;
        }
    }
}

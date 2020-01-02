using LiteDB;
using System;
using Vault.Core.Models;

namespace Vault.Infrastructure
{
    public class PasswordRepository : IPasswordRepository
    {
        public Password Add(Password password)
        {
            using (var db = new LiteDatabase(@"vault.db"))
            {
                var passwords = db.GetCollection<Password>(nameof(Password));
                passwords.Insert(password);
                passwords.EnsureIndex(e => e.Id);

                return password;
            }
        }

        public Password GetById(Guid id)
        {
            using (var db = new LiteDatabase(@"vault.db"))
            {
                var passwords = db.GetCollection<Password>(nameof(Password));
                return passwords.FindOne(e => e.Id == id);
            }
        }

        public void Remove(Password password)
        {
            using (var db = new LiteDatabase(@"vault.db"))
            {
                var passwords = db.GetCollection<Password>(nameof(Password));
                passwords.Delete(e => e.Id == password.Id);
            }
        }

        public void Update(Password password)
        {
            using (var db = new LiteDatabase(@"vault.db"))
            {
                var passwords = db.GetCollection<Password>(nameof(Password));
                passwords.Update(password);
            }
        }
    }
}

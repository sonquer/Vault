using LiteDB;
using System;
using System.Collections.Generic;
using Vault.Core.Models;

namespace Vault.Infrastructure
{
    /// <summary>
    /// Password repository
    /// </summary>
    public class PasswordRepository : IPasswordRepository
    {
        /// <summary>
        /// Add new password entity to database
        /// </summary>
        /// <param name="password">Password</param>
        /// <returns>Created password in database</returns>
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

        /// <summary>
        /// Get password entity by unique password id from database
        /// </summary>
        /// <param name="id">Password unique id</param>
        /// <returns>Profile entity from database</returns>
        public Password GetById(Guid id)
        {
            using (var db = new LiteDatabase(@"vault.db"))
            {
                var passwords = db.GetCollection<Password>(nameof(Password));
                return passwords.FindOne(e => e.Id == id);
            }
        }

        /// <summary>
        /// Get all password entities by profile id from database
        /// </summary>
        /// <param name="profileId">Profile id</param>
        /// <returns>Collection of password entities</returns>
        public IEnumerable<Password> GetByProfileId(Guid profileId)
        {
            using (var db = new LiteDatabase(@"vault.db"))
            {
                var profiles = db.GetCollection<Password>(nameof(Password));
                return profiles.Find(e => e.ProfileId == profileId);
            }
        }

        /// <summary>
        /// Remove password entity from database
        /// </summary>
        /// <param name="password">Password entity</param>
        public void Remove(Password password)
        {
            using (var db = new LiteDatabase(@"vault.db"))
            {
                var passwords = db.GetCollection<Password>(nameof(Password));
                passwords.Delete(e => e.Id == password.Id);
            }
        }

        /// <summary>
        /// Update password entity in database
        /// </summary>
        /// <param name="password">Password entity</param>
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

using LiteDB;
using System;
using System.Collections.Generic;
using Vault.Core.Models;

namespace Vault.Infrastructure
{
    /// <summary>
    /// Profile repository
    /// </summary>
    public class ProfileRepository : IProfileRepository
    {
        /// <summary>
        /// Add profile entity to database
        /// </summary>
        /// <param name="profile">Profile entity</param>
        /// <returns>Created profile in database</returns>
        public Profile Add(Profile profile)
        {
            using (var db = new LiteDatabase(@"vault.db"))
            {
                var profiles = db.GetCollection<Profile>(nameof(Profile));
                profiles.Insert(profile);
                profiles.EnsureIndex(e => e.Id);

                return profile;
            }
        }

        /// <summary>
        /// Get all profile entities from database
        /// </summary>
        /// <returns>Collection of profile entities</returns>
        public IEnumerable<Profile> GetAll()
        {
            using (var db = new LiteDatabase(@"vault.db"))
            {
                var profiles = db.GetCollection<Profile>(nameof(Profile));
                return profiles.FindAll();
            }
        }

        /// <summary>
        /// Get Profile by id from database
        /// </summary>
        /// <param name="id">Profile id</param>
        /// <returns>Profile entity from database</returns>
        public Profile GetById(Guid id)
        {
            using (var db = new LiteDatabase(@"vault.db"))
            {
                var profiles = db.GetCollection<Profile>(nameof(Profile));
                return profiles.FindOne(e => e.Id == id);
            }
        }

        /// <summary>
        /// Remove profile from database
        /// </summary>
        /// <param name="profile">Profile entity</param>
        public void Remove(Profile profile)
        {
            using (var db = new LiteDatabase(@"vault.db"))
            {
                var profiles = db.GetCollection<Profile>(nameof(Profile));
                profiles.Delete(e => e.Id == profile.Id);
            }
        }

        /// <summary>
        /// Update profile in database
        /// </summary>
        /// <param name="profile">Profile entity</param>
        public void Update(Profile profile)
        {
            using (var db = new LiteDatabase(@"vault.db"))
            {
                var profiles = db.GetCollection<Profile>(nameof(Profile));
                profiles.Update(profile);
            }
        }
    }
}

using LiteDB;
using System;
using System.Collections.Generic;
using Vault.Core.Models;

namespace Vault.Infrastructure
{
    public class ProfileRepository : IProfileRepository
    {
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

        public IEnumerable<Profile> GetAll()
        {
            using (var db = new LiteDatabase(@"vault.db"))
            {
                var profiles = db.GetCollection<Profile>(nameof(Profile));
                return profiles.FindAll();
            }
        }

        public Profile GetById(Guid id)
        {
            using (var db = new LiteDatabase(@"vault.db"))
            {
                var profiles = db.GetCollection<Profile>(nameof(Profile));
                return profiles.FindOne(e => e.Id == id);
            }
        }

        public void Remove(Profile profile)
        {
            using (var db = new LiteDatabase(@"vault.db"))
            {
                var profiles = db.GetCollection<Profile>(nameof(Profile));
                profiles.Delete(e => e.Id == profile.Id);
            }
        }

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

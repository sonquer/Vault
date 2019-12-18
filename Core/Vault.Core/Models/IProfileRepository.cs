using System;
using System.Collections.Generic;

namespace Vault.Core.Models
{
    public interface IProfileRepository
    {
        Profile Add(Profile profile);

        void Remove(Profile profile);

        Profile GetById(Guid id);

        IEnumerable<Profile> GetAll();

        void Update(Profile profile);
    }
}

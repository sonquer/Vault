using System;
using System.Collections.Generic;

namespace Vault.Core.Models
{
    public interface IPasswordRepository
    {
        Password Add(Password password);

        void Remove(Password password);

        Password GetById(Guid id);

        IEnumerable<Password> GetByProfileId(Guid profileId);

        void Update(Password password);
    }
}

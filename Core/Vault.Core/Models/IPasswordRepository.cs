using System;

namespace Vault.Core.Models
{
    public interface IPasswordRepository
    {
        Password Add(Password password);

        void Remove(Password password);

        Password GetById(Guid id);

        void Update(Password password);
    }
}

namespace Vault.Core.Dtos
{
    /// <summary>
    /// Password dto entity
    /// </summary>
    public class PasswordDto
    {
        /// <summary>
        /// Password entity name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Plaintext password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
    }
}

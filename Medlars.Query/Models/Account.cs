using System;

namespace Medlars.Query.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Account
    {
        [Key]
        public Guid AccountId { get; set; }

        [Required, StringLength(128)]
        public string Email { get; set; }

        [Required, StringLength(128)]
        public string Secret { get; set; }

        [Required, StringLength(256)]
        public string PasswordHash { get; set; }

        [Required, StringLength(256)]
        public string PasswordSalt { get; set; }

        [Required, StringLength(256)]
        public string AllowedIps { get; set; }
    }
}

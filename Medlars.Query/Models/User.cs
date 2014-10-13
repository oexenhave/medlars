using System;

namespace Medlars.Query.Models
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required, StringLength(150)]
        public string Email { get; set; }

        [Required, StringLength(150)]
        public string Secret { get; set; }
    }
}

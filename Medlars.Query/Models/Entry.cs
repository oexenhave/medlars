namespace Medlars.Query.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Entry
    {
        [Key]
        public Guid EntryId { get; set; }

        [Required, StringLength(128)]
        public string Service { get; set; }

        [Required, MaxLength]
        public string Message { get; set; }

        [Required]
        public int Severity { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public Guid AccountId { get; set; }
    }
}

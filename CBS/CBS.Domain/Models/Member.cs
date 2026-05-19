using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CBS.Domain.Enums;

namespace CBS.Domain.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public Areas Area { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Suffix { get; set; } = string.Empty;

        public string FullName => string.Join(" ",
            new[] { FirstName, LastName, Suffix }.Where(s => !string.IsNullOrWhiteSpace(s)));

        public virtual ICollection<Fund>? Contributions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CBS.Domain.Models;

namespace CBS.Domain.Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public DateTime? DateAdded { get; set; }

        public virtual ICollection<Expense>? Expenses { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using CBS.Domain.Enums;

namespace CBS.Domain.Models
{
    public class Fund
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Member")]
        public int MemberId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public FundTypes ContributionType { get; set; } 

        public DateTime ContributionDate { get; set; }

        public Months Month { get; set; }

        public virtual Member Member { get; set; } = default!;
    }
}

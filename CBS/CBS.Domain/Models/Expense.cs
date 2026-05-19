using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;
using CBS.Domain.Enums;

namespace CBS.Domain.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Vendor")]
        public int VendorId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        
        public string Description { get; set; } = string.Empty;

        public DateTime ExpenseDate { get; set; }

        public ExpenseTypes ExpenseType { get; set; }

        public string? ReceiptImage { get; set; }

        public bool IsRecurring { get; set; }
        public RecurrenceType RecurrenceType { get; set; }
        public DateTime? RecurrenceEndsOn { get; set; }
        public int? RecurrenceSourceId { get; set; }

        public virtual Vendor Vendor { get; set; } = default!;
    }
}

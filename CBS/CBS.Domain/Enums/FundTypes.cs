using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CBS.Domain.Enums
{
    public enum FundTypes
    {
        Offering,
        
        Tithe,
        
        Donation,
        
        [Display(Name = "Other Income")]
        OtherIncome
    }
}

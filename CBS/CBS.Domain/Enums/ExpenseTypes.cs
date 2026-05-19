using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CBS.Domain.Enums
{
    public enum ExpenseTypes
    {
        [Display(Name = "Communication Services")]
        CommunicationServices,

        [Display(Name = "Repair and Maintenance")]
        RepairAndMaintenance,

        [Display(Name = "Transportation Services")]
        TransportationServices,

        [Display(Name = "Supplies and Materials")]
        SuppliesAndMaterials,

        [Display(Name = "Rents")]
        Rents,

        [Display(Name = "Love Gifts and Contributions")]
        LoveGiftsAndContributions,

        [Display(Name = "Water, Light and Power Services")]
        WaterLightAndPower,

        [Display(Name = "Taxes and Licenses")]
        TaxesAndLicenses,

        [Display(Name = "Ministry Services")]
        MinistryServices,

        [Display(Name = "Other Services")]
        OtherServices,

        [Display(Name = "Furniture, Fixture, Book and Equipment")]
        FurnitureFixtureBookAndEquipment,

        [Display(Name = "Building Structure Outlays")]
        BuildingStructureOutlays,

        [Display(Name = "Motor Vehicles and Other Heavy Equipment")]
        MotorVehiclesAndOtherHeavyEquipment
    }
}

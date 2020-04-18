using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HomeAssignmentAPI.Models
{
    public class RentedHistory
    {
        [Key]
        public Guid Id { get; set; }

        public string UserName { get; set; }

        [ForeignKey("EquipmentList")]
        public string Name  { get; set; }

        public int RentedDays { get; set; }

        public virtual EquipmentList EquipmentList { get; set; }

        [NotMapped]
        public InvoiceModel InvoiceModel { get; set; }
    }

    public partial class InvoiceModel
    {
        public string Title { get; set; }

        public int TotalPrice { get; set; }

        public int TotalBonus { get; set; }
    }
}
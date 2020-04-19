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

    }

}
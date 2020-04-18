using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeAssignmentAPI.Models
{
    public class EquipmentList
    {
        [Key]
        public string Name { get; set; }

        public string EquipmentType { get; set; }

        public virtual List<RentedHistory> RentedHistories {get; set;}
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace HomeAssingmentFE.Models
{
    public class ListModel
    {
        public string EquipmentType { get; set; }

        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int RentedDays { get; set; }

    }
}
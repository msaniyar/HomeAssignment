using System;

namespace HomeAssingmentFE.Models
{
    public class RentModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public int RentedDays { get; set; }

        public string EquipmentList { get; set; }
    }
}
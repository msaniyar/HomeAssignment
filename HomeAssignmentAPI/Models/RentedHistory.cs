using System.ComponentModel.DataAnnotations;

namespace HomeAssignmentAPI.Models
{
    public class RentedHistory
    {
        [Key]
        public int Name  { get; set; }

        public int RentedDays { get; set; }

        public virtual EquipmentList EquipmentList { get; set; }
    }
}
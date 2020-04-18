using System.Collections.Generic;

namespace HomeAssingmentFE.Models
{
    public class InvoiceModel
    {
        public string Title { get; set; }

        public List<Items> RentedList { get; set; }

        public int TotalPrice { get; set; }

        public int TotalPoints { get; set; }

    }

    public class Items
    {
        public string RentedName { get; set; }

        public string RentedPrice { get; set; }

    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeAssignmentAPI.Models
{
    [NotMapped]
    public class GeneratedInvoice
    {
        public string Title { get; set; }

        public List<InvoiceItems> Items { get; set; }

        public int TotalPrice { get; set; }

        public int TotalBonus { get; set; }
    }

    [NotMapped]
    public partial class InvoiceItems
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
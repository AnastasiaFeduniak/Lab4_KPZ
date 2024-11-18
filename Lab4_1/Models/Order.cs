using System;
using System.Collections.Generic;

namespace Lab4_1.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int ClientId { get; set; }

    public int? LocationId { get; set; }

    public DateTime OrderDate { get; set; }

    public string DeliveryAddress { get; set; } = null!;

    public string ReceiptNumber { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? ReceivingDate { get; set; }

    public virtual ICollection<BookAmountOrder> BookAmountOrders { get; set; } = new List<BookAmountOrder>();

    public virtual Client Client { get; set; } = null!;

    public virtual Location? Location { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderWebAPI.Models
{
    public class OrderDetail
    {
        [Key]
        [Column("ordetails_id")]
        public int OrderDetailId { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("quantity")]
        public decimal Quantity { get; set; }

        [Column("unitprice")]
        public decimal UnitPrice { get; set; }


        [ForeignKey("OrderId")]
        [Column("order_id")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}

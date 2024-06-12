namespace E_Commerce.Models
{
	public class OrderItems
	{
		public int Id { get; set; }

		public int MedicineId { get; set; }
		public string Medicine { get; set; }
		public decimal UnitPrice { get; set; }	

		public decimal Discount { get; set; } 

		public int Quantity { get; set; }

		public decimal TotalPrice { get; set; }
		public int OrderId { get; set; }
		public order order { get; set; }

    }
}

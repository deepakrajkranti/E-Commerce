namespace E_Commerce.Models
{
	public class Cart
	{
		public int Id { get; set; }

		public int MedicineId { get; set; }
		public decimal UnitPrice { get; set; }
		public int Quantity { get; set; }

		public decimal TotalPrice { get; set; }

		public int UserId { get; set; }
		public Users users { get; set; }

	}
}

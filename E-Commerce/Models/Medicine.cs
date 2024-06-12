namespace E_Commerce.Models
{
	public class Medicine
	{
        public int Id { get; set; }

		public string Name { get; set; }

		public string Manufacturer { get; set; }

		public decimal UnitPrice { get; set; }

		public decimal Discount { get; set; }

		public int Quantity { get; set; }

		public DateTime ExpDate { get; set; }

		public int isActive { get; set; }

		
    }
}

using System.Diagnostics;

namespace E_Commerce.Models
{
	public class order
	{
		public int Id { get; set; }

		public int OrderNo { get; set; }
		public decimal orderTotal { get; set; }
		public string OrderStatus { get; set; }

		public int usersId { get; set; }
	//	public Users users { get; set; }



	}
}

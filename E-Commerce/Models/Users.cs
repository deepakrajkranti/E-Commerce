namespace E_Commerce.Models
{
	public class Users
	{
		
        public int Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }

		public string Email { get; set; }
		public string Role { get; set; }
		public int IsActive { get; set; }

		public ICollection<order> Order { get; set; }

		public Cart cart { get; set; }

    }
}

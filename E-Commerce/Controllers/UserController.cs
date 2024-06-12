using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly ECommerceContext _eCommerceContext;

		public UserController(ECommerceContext eCommerceContext) {
			_eCommerceContext = eCommerceContext;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
		{
			var users = await _eCommerceContext.Users.Include(x=>x.Order).ToListAsync();

			return Ok(users);

		}

		[HttpGet("{id}")]

		public async Task<ActionResult<Users>> GetUser(int id)
		{
			var emp = await _eCommerceContext.Users.FindAsync(id);

			return Ok(emp);

		}

		[HttpGet("orderItem/{OrderId}")]
		public async Task<ActionResult<IEnumerable<OrderItems>>> GetAll(int OrderId)
		{
		 var orders= await _eCommerceContext.OrderItems.Where(x=>x.OrderId == OrderId).ToListAsync();
			return Ok(orders);
		}



		[HttpPost]

		public async Task<ActionResult<Cart>> AddCart(CartDto cartDto)
		{

			//var medicine = await _eCommerceContext.Medicines.Where(a => a.Id == cartDto.Id).ToListAsync();

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
			Medicine medicine = await _eCommerceContext.Medicines.SingleOrDefaultAsync(a => a.Id == cartDto.Id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

			if (medicine == null)
			{
				return NotFound();
			}

			if (medicine.Quantity >= cartDto.quantity)
			{
				var cart1 = new Cart() {
					MedicineId = cartDto.Id,
					Quantity = cartDto.quantity,
					UserId = cartDto.userID,
					UnitPrice = medicine.UnitPrice,
					TotalPrice = medicine.UnitPrice * cartDto.quantity - medicine.Discount
				};

				_eCommerceContext.Carts.Add(cart1);
				await _eCommerceContext.SaveChangesAsync();
				return Ok(cart1);
			}
			else
			{
				return BadRequest();
			}

		}
		[HttpPost("CreateOrder")]
		public async Task<ActionResult<order>> CreateOrder(int id)
		{
			var order = await _eCommerceContext.Carts.Where(a => a.UserId == id).AsNoTracking().ToListAsync();

			Random rnd = new Random();
			int num = (int)rnd.Next();
			decimal Totalcost = 0;



			//	await _eCommerceContext.SaveChangesAsync();

			foreach (var a in order)
			{
				var medicine1 = await _eCommerceContext.Medicines.FindAsync(a.MedicineId);
				Totalcost += a.TotalPrice;
				var orderitem1 = new OrderItems
				{
					MedicineId = a.MedicineId,
					Medicine = medicine1.Name,
					UnitPrice = a.UnitPrice,
					Discount = medicine1.Discount,
					Quantity = a.Quantity,
					TotalPrice = a.TotalPrice,
					OrderId = num
				};
				_eCommerceContext.OrderItems.Add(orderitem1);
				medicine1.Quantity = medicine1.Quantity - a.Quantity;
				await PutMedicine(medicine1);
				//await _eCommerceContext.SaveChangesAsync();
			}
			var orderfinal = new order
			{
				OrderNo = num,
				orderTotal = Totalcost,
				OrderStatus = "Pending",
				usersId = id
			};
			_eCommerceContext.orders.Add(orderfinal);
			await _eCommerceContext.SaveChangesAsync();

			 await DeleteFromCart(id);

			return Ok(orderfinal);
		}

		[HttpPut]

		public async Task<ActionResult<Medicine>> PutMedicine(Medicine medicine)
		{
			_eCommerceContext.Entry(medicine).State = EntityState.Modified;
			try
			{
				await _eCommerceContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw;
			}

			return Ok();
		}
		[HttpDelete("{id}")]

		public async Task<ActionResult> DeleteFromCart(int id)
		{

			if (_eCommerceContext.Carts == null)
			{
				return NotFound();
			}
			var employee = await _eCommerceContext.Carts.Where(x => x.UserId == id).ToListAsync();

			if (employee == null)
			{
				return NotFound();
			}
			_eCommerceContext.Carts.RemoveRange(employee);
			await _eCommerceContext.SaveChangesAsync();

			return Ok();

		}






	}
}

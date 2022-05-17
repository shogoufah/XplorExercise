using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		public CustomerService CustomerService;

		[BindProperty]
		public string customerId { get; set; }

		[BindProperty]
		public string password { get; set; } 
		public IEnumerable<Customer> Costumers { get; private set; }	 

		public IndexModel(ILogger<IndexModel> logger, CustomerService costumerService)
		{
			_logger = logger;
			CustomerService = costumerService;
		}

		public IActionResult OnGet()
		{
			string id = Request.Query["id"];
			string action = Request.Query["action"];
			if(!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(action))
			{
				if(action == "Delete")
				{
					CustomerService.DeleteCustomer(id);
				}else if(action == "Edit")
				{
					return RedirectToPage("/Customer", new { id = id });
				}
			}
		    Costumers = CustomerService.GetCostumers();
			return Page();
		}


	}
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class CustomerModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private string _customerId; 
        public CustomerService _customerService;

        [BindProperty]
        public Customer? _customer { get; set; }
        public CustomerModel(ILogger<IndexModel> logger, CustomerService costumerService)
		{
            _logger = logger;
            _customerService = costumerService;
            _customer = new Customer();
        }
        public void OnGet()
        {
            _customerId = Request.Query["id"];
            string action = Request.Query["action"];
            if (!string.IsNullOrEmpty(_customerId))
            {
                _customer = _customerService.GetCostumer(_customerId);
                //CustomerService.UpdateCustomer(customer, id);
            }
        }

        

        public IActionResult OnPostAsync()
        {
            _customer = new Customer();
            _customer.CustomerId = Request.Form["customerId"];
            if (string.IsNullOrEmpty(_customer.CustomerId))
			{
				return Page();
			}
            
			
            _customer.FirstName = Request.Form["firstname"];
            _customer.LastName = Request.Form["lastname"];
            _customer.Email = Request.Form["email"];
            _customer.PhoneNumber = Request.Form["phonenumber"];
            _customer.CountryCode = Request.Form["countrycode"];
            _customer.Gender = Request.Form["gender"];
            _customer.Balance = Convert.ToDecimal(Request.Form["balance"].ToString());
			if (!string.IsNullOrEmpty(_customerId))
			{
                _customerService.UpdateCustomer(_customer,_customerId);
			}
			else
			{
                _customerService.AddNewCustomer(_customer);
            }
            return RedirectToPage("./Index");
        }
    }
}

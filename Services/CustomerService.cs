using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using WebApplication1.Models;

namespace WebApplication1.Services
{
	public class CustomerService
	{
		private string _baseUrl = "https://getinvoices.azurewebsites.net/api/";
		private string _customers_request_uri = "Customers";
		private string _customer_request_uri = "Customer/{0}";
		private string _post_uri = "Customer";




		private async Task<string> GetAsync(string requestUri)
		{
			using(var client = new HttpClient())
			{
				client.BaseAddress = new Uri(_baseUrl);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage response = await client.GetAsync(requestUri);
				if (response.IsSuccessStatusCode)
				{
					return await response.Content.ReadAsStringAsync();
				}
				else
				{
					return response.StatusCode.ToString();
				}
			}
		}

		private async Task<string> HttpClientRequestAsync(Customer customer, string customerId = "", string method = "GET")
		{
			if(customer == null)
			{
				return null;
			}
			using (var client = new HttpClient())
			{
				HttpResponseMessage response = null;
				client.BaseAddress = new Uri(_baseUrl);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent stringContent = new StringContent(JsonConvert.SerializeObject(customer, Formatting.Indented));
				if (!string.IsNullOrEmpty(customerId))
				{
					_post_uri = string.Format(_post_uri, customerId);
				}
				switch (method)
				{
					case "POST":
						response = await client.PostAsync(_post_uri, stringContent);
						break;
					case "DELETE":
						response = await client.DeleteAsync(string.Format(_customer_request_uri, customerId));
						break;
					default:
						if (string.IsNullOrEmpty(customerId))
						{
							response = await client.GetAsync(_customers_request_uri);
						}
						else
						{
							response = await client.GetAsync(string.Format(_customer_request_uri, customerId));
						}
						break;
				}
				
				if (response.IsSuccessStatusCode)
				{
					return await response.Content.ReadAsStringAsync();
				}
				else
				{
					return response.StatusCode.ToString();
				}
			}
		}

		public IEnumerable<Customer> GetCostumers()
		{
			string jsonStr = HttpClientRequestAsync(new Customer()).Result;
			try
			{
				IEnumerable<Customer> customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(jsonStr);
				return JsonConvert.DeserializeObject<IEnumerable<Customer>>(jsonStr);
			}
			catch (Exception e)
			{
				return null;
			}
		}

		public Customer GetCostumer(string customerId)
		{
			if (string.IsNullOrEmpty(customerId))
			{
				return null;
			}
			string jsonStr = HttpClientRequestAsync(new Customer(),customerId).Result;
			try
			{
				return JsonConvert.DeserializeObject<Customer>(jsonStr);
			}
			catch (Exception e)
			{
				return null;
			}
		}

		public void AddNewCustomer(Customer customer)
		{
			HttpClientRequestAsync(customer,"", "POST").Wait();
		}

		public void UpdateCustomer(Customer customer, string customerId)
		{
			HttpClientRequestAsync(customer,customerId, "POST").Wait();
		}

		public void DeleteCustomer(string customerId)
		{
			HttpClientRequestAsync(new Customer(), customerId, "DELETE").Wait();
		}

	}
}

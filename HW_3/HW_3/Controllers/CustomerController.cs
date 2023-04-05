using HW_3.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HW_3.Controllers {
    [Route("customers")]
    [ApiController]
    public class CustomerController : ControllerBase {
        private readonly DataContext _dataContext;

        public CustomerController(DataContext dataContext) {
            _dataContext = dataContext;
        }
        [HttpGet("{id:long}")]
        public async Task<Customer> GetCustomerAsync([FromRoute] long id) {
            return await _dataContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost("")]
        public async Task<long> CreateCustomerAsync([FromBody] Customer customer) {
            await _dataContext.Customers.AddAsync(customer);
            await _dataContext.SaveChangesAsync();
            return customer.Id;
        }

    }
}

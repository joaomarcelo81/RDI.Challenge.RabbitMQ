using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RDI.Challenge.Domain;
using RDI.Challenge.Domain.Entities;
using RDI.Challenge.Domain.Interfaces.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDI.Challenge.UI.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {

        private readonly IOrderBusiness _OrderBusiness;
        public OrderController(IOrderBusiness OrderBusiness)
        {
            _OrderBusiness = OrderBusiness;
        }
        [HttpGet]
        public async Task<IEnumerable<Order>> Get()
        {
            return await _OrderBusiness.GetAll();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order Order)
        {
            try
            {
               await _OrderBusiness.AddAsync(Order);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Oops, Something went wrong.");
            }
            

            return Ok();
        }

        [Route("{area}/Items")]
        [HttpGet]
        public async Task<IEnumerable<QueueItemOrder>> GetMenuItemByArea(string area)
        {

            return await _OrderBusiness.GetMenuItemsFromOrder(area);

        }
    }
}

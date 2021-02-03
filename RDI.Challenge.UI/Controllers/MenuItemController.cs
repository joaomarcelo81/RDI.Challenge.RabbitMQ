using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class MenuItemController : Controller
    {

        private readonly IMenuItemBusiness _MenuItemBusiness;
        public MenuItemController(IMenuItemBusiness MenuItemBusiness)
        {
            _MenuItemBusiness = MenuItemBusiness;
        }
        [HttpGet]
        public async Task<IEnumerable<MenuItem>> Get()
        {
            return await _MenuItemBusiness.GetAll();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using SIG.Data.Entity.Identity;
using SIG.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIG.WebAPICore2.Controllers
{
   
    [Route("api/[controller]")]

    public class ValuesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ValuesController> _logger;
        public ValuesController(IUnitOfWork unitOfWork, ILogger<ValuesController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;

        // seeding
            var repo = _unitOfWork.GetRepository<Role>();
            if (repo.Count() == 0)
            {
                repo.Insert(new Role{
               
                    RoleName = "系统管理员",
                    IsSys = true,
                    Description = ""
                     
                }, new Role
                {
                
                    RoleName = "用户",
                    IsSys = true,
                    Description = ""

                });
                _unitOfWork.SaveChanges();
            }
        }
        // GET: api/values
        [HttpGet]
       // [Produces("application/json")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Value), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(typeof(void), 500)]
        public IActionResult Get(int id)
        {
            return Ok(new Value { Id = id, Text = $"value {id}" });
        }

        // POST api/values
        [HttpPost]
        [ApiExplorerSettings(GroupName = "v2")]
        public IActionResult Post([FromBody]Value value)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException("模型验证失败！");
            }

            return CreatedAtAction("Get",new { id=value.Id}, value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Value value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Role1,Role2,Role3")]
        public void Delete(int id)
        {
        }
    }
    public class Value
    {
        public int Id { get; set; }
        [MinLength(3)]
        public string Text { get; set; }
    }
}

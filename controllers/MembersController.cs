using API.data;
using API.entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.controllers
{
    [Route("api/[controller]")]//localhost:7202/api/members
    [ApiController]
    public class MembersController(AppDBcontext context) : ControllerBase
    {
        [HttpGet]

        public async Task <ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.Users.ToListAsync();
            return members;
        }
        [HttpGet("{id}")]//localhost:7202/api/members/bob-id
        public async Task <ActionResult<AppUser>> GetMember(string id)
        {
            var member =  await context.Users.FindAsync(id);
            if (member == null) return NotFound();
            return member;
            
        }
    }
}

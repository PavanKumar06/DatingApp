using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.api.Data;
using DatingApp.api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository Repo;
        private readonly IMapper Mapper;
        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            Mapper = mapper;
            Repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await Repo.GetUsers();
            var usersToReturn = Mapper.Map<IEnumerable<UserForListDto>>(users);// This alone is not enough we also need to tell AutoMapper about these Mappings, so create a class
           
           return Ok(usersToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await Repo.GetUser(id);
            var userToReturn = Mapper.Map<UserForDetailedDto>(user);// The job of this function is to execute a maping from the source objects to destination object

            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) 
            {
                return Unauthorized();
            }

            var userFromRepo = await Repo.GetUser(id);

            Mapper.Map(userForUpdateDto, userFromRepo);

            if (await Repo.SaveAll())
            {
                return NoContent();
            }
            throw new Exception($"Updating user {id} failed on save");
        }
    }
}
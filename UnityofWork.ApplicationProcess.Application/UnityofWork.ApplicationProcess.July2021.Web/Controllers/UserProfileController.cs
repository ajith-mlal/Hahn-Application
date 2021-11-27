using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UnityofWork.ApplicationProcess.July2021.Data.Configuration;
using UnityofWork.ApplicationProcess.July2021.Data.Entities;
using UnityofWork.ApplicationProcess.July2021.Domain.Dto;
using AutoMapper;

namespace UnityofWork.ApplicationProcess.July2021.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUnitofWork _unitofwork;
        private readonly IMapper _mapper;
        public UserProfileController(IUnitofWork unitofwork,IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;

        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> Get()
        {
            var users = await _unitofwork.UserProfile.All();
            return Ok(users);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(UserProfileDto user)
        {


            await _unitofwork.UserProfile.Add(_mapper.Map<UserProfile>(user));
            await _unitofwork.CompleteAsync();

            return new JsonResult("Created Succesfully") { StatusCode = 200 };

        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var item = await _unitofwork.UserProfile.GetById(userId);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> Update(UserProfileDto user)
        {
            var item = await _unitofwork.UserProfile.Update(_mapper.Map<UserProfile>(user));
            await _unitofwork.CompleteAsync();

            return Ok(item);
        }
    }
}

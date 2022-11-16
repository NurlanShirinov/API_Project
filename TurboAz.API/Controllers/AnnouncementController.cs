using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;
using TurboAz.Service.Services.Abstract;
using TurboAz.Service.Services.Concrete;

namespace TurboAz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;
        private readonly IPaymentService _paymentService;


        public AnnouncementController(IAnnouncementService announcementService, IPaymentService paymentService)
        {
            _announcementService = announcementService;
            _paymentService = paymentService;
        }

        [HttpGet("GetAllAnnouncement")]
        public async Task<IActionResult> GetAll()
        {
            var res = await _announcementService.GetAll();
            return Ok(res);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var res = await _announcementService.GetById(id);
            return Ok(res);
        }

        [HttpDelete("DeleteAnnouncement")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var res = await _announcementService.Delete(id);
            return Ok(res);
        }

        [HttpPut("UpdateAnnouncement")]
        public async Task<IActionResult> UpdateAnnouncement([FromBody] Announcement announcement)
        {
            var res = await _announcementService.Update(announcement);
            return Ok(res);
        }

        [HttpPost("Add")]
        public async  Task<IActionResult> AddAnnouncement([FromBody] Announcement announcement)
        {
            var res = await _announcementService.Add(announcement);
            return Ok(res);
        }


        //[HttpPost("GetFilteredData")]
        //public async Task<IActionResult> GetFilteredData([FromBody] GetFilteredDataRequestModel model)
        //{
        //    var res = await _announcementService.Filtered(model);
        //    return Ok(res);
        //}

        [HttpPost("SetAnnouncmentToVip")]
        public async Task<IActionResult> SetAnnouncmentToVip([FromBody] SetAnnouncmentVipRequestModel model)
        {
            var paymentStatus = _paymentService.Pay(model.CardNumber, model.Email);

            if (paymentStatus is true)
            {
                await _announcementService.SetVip(model.AnnouncmentId);
                return Ok($"Announcment set VIP with id : {model.AnnouncmentId}");
            }
            return BadRequest($"Announcment cannot set VIP with id : {model.AnnouncmentId}");
        }

    }
}

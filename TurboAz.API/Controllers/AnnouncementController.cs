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
        public IActionResult GetAll()
        {
            var res = _announcementService.GetAll();
            return Ok(res);
        }

        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery]int id)
        {
            var res = _announcementService.GetById(id);
            return Ok(res);
        }

        [HttpDelete("DeleteAnnouncement")]
        public IActionResult Delete([FromQuery]int id)
        {
            var res = _announcementService.Delete(id);
            return Ok(res);
        }

        [HttpPut("UpdateAnnouncement")]
        public IActionResult UpdateAnnouncement([FromBody]Announcement announcement)
        {
            var res = _announcementService.Update(announcement);
            return Ok(res);
        }

        [HttpPost("Add")]
        public IActionResult AddAnnouncement([FromBody]Announcement announcement)
        {
            var res = _announcementService.Add(announcement);
            return Ok(res);
        }


        [HttpPost("GetFilteredData")]
        public IActionResult GetFilteredData([FromBody] GetFilteredDataRequestModel model)
        {
            var res = _announcementService.Filtered(model);
            return Ok(res);
        }

        [HttpPost("SetAnnouncmentToVip")]
        public IActionResult SetAnnouncmentToVip([FromBody] SetAnnouncmentVipRequestModel model)
        {
            var paymentStatus = _paymentService.Pay(model.CardNumber, model.Email);

            if (paymentStatus is true)
            {
                _announcementService.SetVip(model.AnnouncmentId);
                return Ok($"Announcment set VIP with id : {model.AnnouncmentId}");
            }
            return BadRequest($"Announcment cannot set VIP with id : {model.AnnouncmentId}");
        }

    }
}

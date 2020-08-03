using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotelfinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllHotels()
        {
          var hotels= await _hotelService.getAllHotels();
            return Ok(hotels);
        }


        //[HttpGet("{id}")]
        [HttpGet]
        [Route("[action]/{id}")]

        public async Task<IActionResult>  GetHotelById(int id)
        {
            var hotel= await _hotelService.GetHotelById(id);
            if (hotel!=null)
            {
                return Ok(hotel);

            }
            return NotFound();

        }

        [HttpGet]
        [Route("[action]/{name}")]

        public async Task<IActionResult> GetHotelByName(string name)
        {
            var hotel =await _hotelService.GetHotelByName(name);
            if (hotel != null)
            {
                return Ok(hotel);

            }
            return NotFound();

        }


        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody]Hotel hotel)
        {
           
                var createdHotel =await _hotelService.CreateHotel(hotel);
                return CreatedAtAction("Get", new { id = createdHotel.Id },createdHotel);         
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel hotel)
        {
            if (await _hotelService.GetHotelById(hotel.Id)!=null)
            {
                return Ok(await _hotelService.UpdateHotel(hotel));
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (await _hotelService.GetHotelById(id) != null)
            {
               await _hotelService.DeleteHotel(id);
                return Ok();
            }
            return NotFound();
           
        }

    }
}

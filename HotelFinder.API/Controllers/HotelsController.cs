using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelFinder.API.Controllers
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

        /// <summary>
        /// Get Hotels All
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hotels = await _hotelService.GetAllHotels();
           // var hotels = _hotelService.GetAllHotels();
            return Ok(hotels); // 200  + data
        }

        [HttpGet]
        [Route("GetHotelById/{id}")] //api/hotels/gethotelbyid/2
        public IActionResult Get(int id)
        {
            var hotel = _hotelService.GetHotelById(id);
            if (hotel != null)
            {
                return Ok(hotel); // 200 + data

            }
            return NotFound(); //404
            
        }
        [HttpGet]
        [Route("GetHotelById/{name}")]
        public IActionResult Get(string name)
        {
            var hotel = _hotelService.GetHotelByName(name);
            if(hotel != null)
            {
                return Ok(hotel);
            }
            return NotFound();

        }
        [HttpPost]
        [Route("[action]")]//[Route("CreateHotel")]
        public IActionResult CreateHotel([FromBody]Hotel hotel)
        {
            var createdHotel =  _hotelService.CreateHotel(hotel);
            return CreatedAtAction("Get", new { id = createdHotel.Id }, createdHotel);//201 + data

        }
        [HttpPut]
        [Route("[action]")]//[Route("UpdateHotel")]
        public IActionResult UpdateHotel([FromBody]Hotel hotel)
        {
            if(_hotelService.GetHotelById(hotel.Id) != null)
            {
                return Ok(_hotelService.UpdateHotel(hotel)); //201 + data
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("[action]/{id}")]
        public IActionResult Delete(int id) 
        {
            if (_hotelService.GetHotelById(id) != null)
            {
                return Ok(); //201 + data
            }
            return NotFound();
        }
    }
}

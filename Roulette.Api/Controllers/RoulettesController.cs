using Roulette.Api.Entities;
using Roulette.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using Roulette.Api.Dtos;
using System.Threading.Tasks;

namespace Roulette.Api.Controllers
{
    [ApiController]
    [Route("roulettewheels")]
    public class RoulettesController : ControllerBase
    {
        private readonly IIRoulettesRepository repository;

        public RoulettesController(IIRoulettesRepository repository)
        {
           this.repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<RouletteWheelDto>> GetRouletteWheelsAsync()
        {
            var rouletteWheels = (await repository.GetRouletteWheelsAsync())
                                 .Select(roulettewheel => roulettewheel.AsDto());
            return rouletteWheels;
        }
        
        //GET /roulettes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RouletteWheelDto>> GetRouletteWheelAsync(Guid id)
        {
            var roulette = await repository.GetRouletteWheelAsync(id);

            if(roulette is null){
                return NotFound();
            }
            return roulette.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<RouletteWheel>> CreateRouletteWheelAsync()
        {
            RouletteWheel rouletteWheel = new()
            {
                Id = Guid.NewGuid(), 
                CreatedDate = DateTimeOffset.UtcNow, 
                IsOpen = false
            };
            await repository.CreateRouletteWheelAsync(rouletteWheel);
            return CreatedAtAction(nameof(GetRouletteWheelAsync), new { id = rouletteWheel.Id }, rouletteWheel.AsDto());            
        }
         
         //PUT /roulettes/{id}
        [HttpPut("/roulettewheels/open/{id}")]
        public async Task<ActionResult> OpenRouletteWheelAsync(Guid id)
        {
            var existingRouletteWheel = await repository.GetRouletteWheelAsync(id);
            if(existingRouletteWheel is null) 
            {
                return NotFound();
            }
            RouletteWheel updatedItem = existingRouletteWheel with {
                IsOpen = true
            };
            await repository.OpenRouletteWheelAsync(updatedItem);
            return NoContent();
        }
         
        [HttpPut("/roulettewheels/close/{id}")]
        public async Task<ActionResult> CloseRouletteWheelAsync(Guid id)
        {
            var existingRouletteWheel = await repository.GetRouletteWheelAsync(id);
            if(existingRouletteWheel is null) 
            {
                return NotFound();
            }
            RouletteWheel updatedItem = existingRouletteWheel with {
                IsOpen = false
            };
            await repository.CloseRouletteWheelAsync(updatedItem);
            return NoContent();
        }
        
        [HttpPost("/roulettewheels/createbet/{id}")]
        public async Task<ActionResult<CreateBetDto>> CreateBetAsync(Guid id, CreateBetDto createBetDto)
        {
            var rouletteWheel = repository.GetRouletteWheelAsync(id);
            if(rouletteWheel is null) 
            {
                return NotFound();
            }
            if(!rouletteWheel.Result.IsOpen)
            {
                
            }  
     //       ColorsTypesEnum myColors;
      //      Enum.TryParse("Active", out myColors);     

            Bet bet = new()
            {
                UserId = Guid.NewGuid(), 
                Number = createBetDto.Number,
                Color = createBetDto.Color,
                Amount = createBetDto.Amount,
                CreatedDate = DateTimeOffset.UtcNow
            };
        //     repository.CreateBetAsync(bet);
            await repository.CreateBetAsync(id,bet);
            
            return NoContent(); 
        }
    }
}
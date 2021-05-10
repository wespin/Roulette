using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Roulette.Api.Entities;

namespace Roulette.Api.Repositories
{    public interface IIRoulettesRepository
    {
        Task<RouletteWheel> GetRouletteWheelAsync(Guid id);
        Task<IEnumerable<RouletteWheel>> GetRouletteWheelsAsync();
        Task CreateRouletteWheelAsync(RouletteWheel rouletteWheel);
        Task OpenRouletteWheelAsync(RouletteWheel rouletteWheel);  
        Task CloseRouletteWheelAsync(RouletteWheel rouletteWheel); 
        Task CreateBetAsync (Guid id, Bet bet);
    }
}
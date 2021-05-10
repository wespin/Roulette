using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Roulette.Api.Entities;

namespace Roulette.Api.Repositories
{
    public class InMemRoulettesRepository : IIRoulettesRepository
    {
        private readonly List<RouletteWheel> roulettes = new()
        {
            new RouletteWheel { Id = Guid.NewGuid(), CreatedDate = DateTimeOffset.UtcNow, IsOpen = false },
            new RouletteWheel { Id = Guid.NewGuid(), CreatedDate = DateTimeOffset.UtcNow, IsOpen = false },
            new RouletteWheel { Id = Guid.NewGuid(), CreatedDate = DateTimeOffset.UtcNow, IsOpen = false }
        };
        private readonly List<Bet> bets = new();

        public async Task<IEnumerable<RouletteWheel>> GetRouletteWheelsAsync()
        {
            return await Task.FromResult(roulettes);
        }
        public async Task<RouletteWheel> GetRouletteWheelAsync(Guid id)
        {
            var roulette =roulettes.Where(roulette => roulette.Id == id).SingleOrDefault();
            return await Task.FromResult(roulette);
        }
        public async Task CreateRouletteWheelAsync(RouletteWheel rouletteWheel)
        {
            roulettes.Add(rouletteWheel);
            await Task.CompletedTask;
        }
        public async Task OpenRouletteWheelAsync(RouletteWheel rouletteWheel)   
        {
            var index = roulettes.FindIndex(existingRouletteWheel => existingRouletteWheel.Id == rouletteWheel.Id);
            roulettes[index] = rouletteWheel;
            await Task.CompletedTask;
        }
        public async Task CloseRouletteWheelAsync(RouletteWheel rouletteWheel)   
        {
            var index = roulettes.FindIndex(existingRouletteWheel => existingRouletteWheel.Id == rouletteWheel.Id);
            roulettes[index] = rouletteWheel;
            await Task.CompletedTask;
        }
        public async Task CreateBetAsync(Guid id, Bet bet)
        {
            bets.Add(bet);
            await Task.CompletedTask;
        }
        // public Bet GetBetAsync(Guid id)
        // {
        //     return bets.Where(bet => bet.Id == id).SingleOrDefault();
        // }     
        // public IEnumerable<Bet> GetBetsAsync(Guid rouletteWheelId)
        // {
        //     if(rouletteWheelId != Guid.Empty)
        //     {
        //         var betFilter = bets.Where(p => p.RouletteWheelId == rouletteWheelId).ToList();
        //         return betFilter;
        //     }
        //     return bets;
        // }        
    }
}
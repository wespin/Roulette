using Roulette.Api.Dtos;
using Roulette.Api.Entities;

namespace Roulette.Api
{
    public static class Extensions
    {
        public static RouletteWheelDto AsDto(this RouletteWheel roulettewheel)
        {
            return new RouletteWheelDto
            {
                Id = roulettewheel.Id,
                CreatedDate = roulettewheel.CreatedDate,
                FinalizedDate  = roulettewheel.FinalizedDate,
                IsOpen  = roulettewheel.IsOpen,
                WinAmount  = roulettewheel.WinAmount,
                WinNumber  = roulettewheel.WinNumber,
                WinColor  = roulettewheel.WinColor,
                Bets = roulettewheel.Bets
            };
        }
         public static CreateBetDto AsDto(this Bet bet)
        {
            return new CreateBetDto
            {
                Number = bet.Number,
                Color = bet.Color,
                Amount = bet.Amount,
            };
        }
    }
}
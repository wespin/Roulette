using System;
using System.Collections.Generic;
using Roulette.Api.Entities;

namespace Roulette.Api.Dtos
{
    public record RouletteWheelDto
    {
        public Guid Id { get; init;}
        public DateTimeOffset CreatedDate { get; init;}   
        public DateTimeOffset FinalizedDate { get; init;}
        public bool IsOpen  { get; init;}
        public decimal WinAmount {get; init;}
        public int WinNumber {get; init;}
        public string WinColor  {get; init;}
        public List<Bet> Bets { get; init; }
    }
}
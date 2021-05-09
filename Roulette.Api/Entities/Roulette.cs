using System;
using System.Collections.Generic;
namespace Roulette.Api.Entities
{
    public record Roulette
    {
        public Roulette()
        {
            Bets = new List<Bet>();
        }
        public Guid Id { get; init;}
        public DateTimeOffset CreatedDate { get; init;}   
        public DateTimeOffset FinalizedDate { get; init;}
        public bool isOpen  { get; init;}
        public List<Bet> Bets { get; init; }

        public decimal WinAmount {get; init;}
        public int WinNumber {get; init;}
        public string WinColor  {get; init;}
    }
}
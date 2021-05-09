
using System;

namespace Roulette.Api.Entities
{
    public record Bet
    {
        public Guid UserId { get; init;}
        public int Number { get; init;}
        public string Color { get; init;}
        public decimal Amount { get; init;}
        public DateTimeOffset CreatedDate { get; init;}
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Roulette.Api.Entities;

namespace Roulette.Api.Dtos
{
    public record CreateBetDto
    {
        [Required]    
        [Range(0, 36)]    
        public int Number { get; init;}

        [RegularExpression("black|red", ErrorMessage = "The color must be either 'black' or 'red' only.")]
        public String Color { get; init;}
        [Required]        
        [Range(1, 10000)]
        public decimal Amount { get; init;}
    }
}
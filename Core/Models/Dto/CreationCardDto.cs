﻿using Microsoft.AspNetCore.Http;

namespace Core.Models.Dto
{
    public record CreationCardDto
    {
        public CardType Type { get; set; }
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public IFormFile? Image { get; set; }
    }
}
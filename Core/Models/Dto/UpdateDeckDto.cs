﻿using System.ComponentModel.DataAnnotations;
using Core.Models.Validation;

namespace Core.Models.Dto
{
    public record UpdateDeckDto
    {
        [Required]
        [OnlyLettersAndNumbers]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
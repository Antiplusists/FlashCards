﻿using System.ComponentModel.DataAnnotations;
using Core.Models.Validation;
using Microsoft.AspNetCore.Http;

namespace Core.Models.Dto
{
    public record CreationCardDto
    {
        [OnlyLettersAndNumbers]
        [StringLength(500)]
        public string? Question { get; set; } = null!;
        [Required]
        [OnlyLettersAndNumbers]
        [StringLength(500)]
        public string Answer { get; set; } = null!;
        [Validation.FileExtensions("jpg", "jpeg", "png")]
        public IFormFile? Image { get; set; }
    }
}
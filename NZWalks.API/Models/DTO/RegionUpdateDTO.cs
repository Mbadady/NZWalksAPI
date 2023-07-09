using System;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
	public class RegionUpdateDTO
	{
        //public Guid Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name should be a maximum of 100 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }

        [Required]
        [MaxLength(3, ErrorMessage = "Code should have a maximum of 3 characters")]
        [MinLength(3, ErrorMessage = "Code should have a minimum of 3 characters")]
        public string Code { get; set; }
    }
}


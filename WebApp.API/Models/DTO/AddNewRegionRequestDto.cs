﻿namespace WebApp.API.Models.DTO
{
	public class AddNewRegionRequestDto
	{
		public string Code { get; set; }

		public string Name { get; set; }

		public string? RegionImageUrl { get; set; }
	}
}

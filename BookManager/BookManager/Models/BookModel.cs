﻿using System.ComponentModel.DataAnnotations;

namespace BookManager.Models
{
	public class BookModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string? Description { get; set; }
		public string Author { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
	}
}

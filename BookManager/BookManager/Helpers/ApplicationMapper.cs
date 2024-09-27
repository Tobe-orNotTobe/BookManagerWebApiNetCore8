using AutoMapper;
using BookManager.Data;
using BookManager.Models;

namespace BookManager.Helpers
{
	public class ApplicationMapper : Profile
	{
		public ApplicationMapper()
		{
			CreateMap<Book, BookModel>().ReverseMap();
		}
	}
}

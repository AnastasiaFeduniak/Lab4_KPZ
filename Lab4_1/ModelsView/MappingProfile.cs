using AutoMapper;
using Lab4_1.Models;
using Lab4_1.ModelsUpdate;

namespace Lab4_1.ModelsView
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorViewModel>(); 
            CreateMap<AuthorUpdateViewModel, Author>();
            CreateMap<Book, BookViewModel>();
            CreateMap<BookCreateUpdateViewModel, Book>();
            CreateMap<BookUpdate, Book>();
        }
    }
}
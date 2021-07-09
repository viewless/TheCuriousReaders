using AutoMapper;
using System;
using TheCuriousReaders.DataAccess.Entities;
using TheCuriousReaders.Models.RequestModels;
using TheCuriousReaders.Models.ResponseModels;
using TheCuriousReaders.Models.ServiceModels;

namespace TheCuriousReaders.API.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserEntity, UserModel>();
            CreateMap<UserModel, UserEntity>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailAddress));

            CreateMap<RequestRegisterModel, UserModel>();
            CreateMap<RequestLoginModel, UserLoginModel>();

            CreateMap<AddressEntity, AddressModel>();
            CreateMap<AddressModel, AddressEntity>();
            CreateMap<RequestAddressModel, AddressModel>();

            CreateMap<BookEntity, BookModel>();
            CreateMap<AuthorEntity, AuthorModel>();
            CreateMap<GenreEntity, GenreModel>();

            CreateMap<BookModel, BookEntity>();
            CreateMap<AuthorModel, AuthorEntity>();
            CreateMap<GenreModel, GenreEntity>();

            CreateMap<RequestBookModel, BookModel>();
            CreateMap<RequestAuthorModel, AuthorModel>();
            CreateMap<RequestGenreModel, GenreModel>();

            CreateMap<BookModel, AuthorResponse>();
            CreateMap<BookModel, NewBookResponse>();
            CreateMap<BookModel, PaginatedNewBookModel>();
            CreateMap<CommentEntity, CommentModel>();
            CreateMap<CommentModel, CommentEntity>();
            CreateMap<RequestCommentModel, CommentModel>();
            CreateMap<CommentModel, CommentResponse>();
            CreateMap<PaginatedNewBookModel, PaginatedNewBookResponse>();
            CreateMap<CommentEntity, PaginatedCommentModel>()
               .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.User.FirstName))
               .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.User.LastName));

            CreateMap<UserSubscriptionEntity, SubscriptionModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.RequestedSubscriptionDays, opt => opt.MapFrom(src => src.RequestedDays));

            CreateMap<UserSubscriptionEntity, ApprovedSubscriptionModel>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.ReturnBookDate, opt => opt.MapFrom(src => src.SubscriptionEnd))
                .ForMember(dest => dest.RemainingDays, opt => opt.MapFrom(src => ((src.SubscriptionEnd - DateTime.Now).Days < 0 ? 0 : (src.SubscriptionEnd - DateTime.Now).Days)));

            CreateMap<SubscriptionModel, SubscriptionResponse>();
            CreateMap<ApprovedSubscriptionModel, ApprovedSubscriptionResponse>();
            CreateMap<BookModel, UpdateBookModel>();

            CreateMap<BookModel, BookResponse>();
            CreateMap<RequestBookPatch, BookModel>();
            CreateMap<BookModel, RequestBookPatch>();
            CreateMap<AuthorModel, AuthorResponse>();
            CreateMap<AuthorModel, RequestAuthorModel>();
            CreateMap<GenreModel, RequestGenreModel>();
            CreateMap<GenreModel, GenreResponse>();
            CreateMap<BookEntity, SearchBookModel>();
        }
    }
}

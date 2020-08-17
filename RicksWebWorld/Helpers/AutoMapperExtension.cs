using AutoMapper;
using RicksWebWorld.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.Models
{
    public class AutoMapperExtension
    {
		public static void Initialize()
		{
			Mapper.Initialize(cfg => {

				//Product to productviewmodel
				cfg.CreateMap<Product, ProductViewModel>().ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.RetrieveProductId()));

				//Userviewmodel to UserOrderViewModel
				cfg.CreateMap<UserViewModel, UserOrderViewModel>()
				.ForMember(dest => dest.SendAddress, opt => opt.MapFrom(src => src.Address))
				.ForMember(dest => dest.SendZipcode, opt => opt.MapFrom(src => src.Zipcode))
				.ForMember(dest => dest.SendPlace, opt => opt.MapFrom(src => src.Place));

			});
		}

		public IMapper ProductToProductviewmodel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductViewModel>()
			.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.RetrieveProductId()))
			.ForMember(dest => dest.BtwPercentage, opt => opt.MapFrom(src => src.BtwPercentage))
			.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
			.ForMember(dest => dest.IsSpecialOffer, opt => opt.MapFrom(src => src.RetrieveSpecialOffer()))
			.ForMember(dest => dest.ProductAmount, opt => opt.MapFrom(src => src.ProductAmount))
			.ForMember(dest => dest.ProductDesc, opt => opt.MapFrom(src => src.ProductDesc))
			.ForMember(dest => dest.ProductDiscount, opt => opt.MapFrom(src => src.ProductDiscount))
			.ForMember(dest => dest.ProductInStock, opt => opt.MapFrom(src => src.ProductInStock))
			.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
			.ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.ProductPrice))
			.ForMember(dest => dest.ProductStatus, opt => opt.MapFrom(src => src.ProductStatus))
			.ForMember(dest => dest.UserLoggedIn, opt => opt.Ignore())
			.ForMember(dest => dest.OfferEnd, opt => opt.Ignore())
			.ForMember(dest => dest.OfferPrice, opt => opt.Ignore())
			.ForMember(dest => dest.OfferStart, opt => opt.Ignore())
			.ForMember(dest => dest.AmountToOrder, opt => opt.Ignore())
			.ForMember(x => x.ProductCategories, opt => opt.Ignore())
			.ForMember(x => x.RevisionsInProduct, opt => opt.Ignore())
			.ForMember(x => x.ListOfAllOffers, opt => opt.Ignore())
			.ForMember(x => x.ListOfAllCategories, opt => opt.Ignore()));
		
			var mapper = map.CreateMapper();
			return mapper;
		}

		public IMapper SpecialOfferToProductviewmodel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<SpecialOffer, ProductViewModel>()
			.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.RetrieveProductId()))
			.ForMember(dest => dest.OfferEnd, opt => opt.MapFrom(src => src.EndTime))
			.ForMember(dest => dest.OfferStart, opt => opt.MapFrom(src => src.StartTime))
			.ForMember(dest => dest.OfferPrice, opt => opt.MapFrom(src => src.RetrieveOfferPrice()))
			.ForMember(x => x.ProductCategories, opt => opt.Ignore()));
			var mapper = map.CreateMapper();
			return mapper;
		}

		public IMapper UserViewModelToUserOrderViewModel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, UserOrderViewModel>()
			.ForMember(dest => dest.SendAddress, opt => opt.MapFrom(src => src.Address))
			.ForMember(dest => dest.SendZipcode, opt => opt.MapFrom(src => src.Zipcode))
			.ForMember(dest => dest.SendPlace, opt => opt.MapFrom(src => src.Place)));

			var mapper = map.CreateMapper();
			return mapper;
		}

		public IMapper CategoryToCategoryOverviewViewModel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryOverviewViewModel>()
			.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.RetrieveCategoryId()))
			.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.RetrieveCategoryName())));

			var mapper = map.CreateMapper();
			return mapper;
		}

		public IMapper CategoryToCategoryViewModel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryViewModel>()
			.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.RetrieveCategoryId()))
			.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.RetrieveCategoryName())));

			var mapper = map.CreateMapper();
			return mapper;
		}

		public IMapper PaymentToPaymentViewModel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<Payment, PaymentMethodViewModel>()
			.ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.RetrieveName()))
			.ForMember(dest => dest.PaymentMethodId, opt => opt.MapFrom(src => src.RetrieveID())));

			var mapper = map.CreateMapper();
			return mapper;
		}

		public IMapper PaymentToOptionsViewModel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<Payment, OptionsViewModel>()
			.ForMember(dest => dest.PaymentMethodId, opt => opt.MapFrom(src => src.RetrieveID()))
			.ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.RetrieveName()))
			.ForMember(dest => dest.PaymentMethodDescription, opt => opt.MapFrom(src => src.RetrieveDesc())));

			var mapper = map.CreateMapper();
			return mapper;
		}

		public IMapper PermissionToPermissionViewModel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<Permission, PermissionViewModel>()
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RetrievePermissionId()))
			.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RetrievePermissionName()))
			.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.RetrievePermissionDescription())));

			var mapper = map.CreateMapper();
			return mapper;
		}

		public IMapper RoleToRoleViewModel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleViewModel>()
			.ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RetrieveRoleId()))
			.ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RetrieveRoleName()))
			.ForMember(dest => dest.RoleDescription, opt => opt.MapFrom(src => src.RetrieveRoleDesc())));

			var mapper = map.CreateMapper();
			return mapper;
		}

		public IMapper RoleToRoleOverviewViewModel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleOverviewViewModel>()
			.ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RetrieveRoleId()))
			.ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RetrieveRoleName())));

			var mapper = map.CreateMapper();
			return mapper;
		}

		public IMapper RevisionToRevisionViewModel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<Revision, RevisionViewModel>()
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RetrieveId()))
			.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.RetrieveProduct()))
			.ForMember(dest => dest.RevisionDateTime, opt => opt.MapFrom(src => src.RevisionDateTime)));

			var mapper = map.CreateMapper();
			return mapper;
		}

		public IMapper UserToUserRoleViewModel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<User, UserRoleViewModel>()
			.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.ReturnUsername()))
			.ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.ReturnPassword())));

			var mapper = map.CreateMapper();
			return mapper;
		}

		public IMapper UserToUserOverviewViewModel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<User, UserOverviewViewModel>()
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ReturnUserId()))
			.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
			.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.ReturnUsername())));

			var mapper = map.CreateMapper();
			return mapper;
		}

		public IMapper UserToUserViewModel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<User, UserViewModel>()
			.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.ReturnUserId()))
			.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
			.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.ReturnUsername())));

			var mapper = map.CreateMapper();
			return mapper;
		}

		public IMapper CartItemToProductViewModel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<CartItem,ProductViewModel> ()
			.ForMember(dest => dest.BtwPercentage, opt => opt.MapFrom(src => src.Product.BtwPercentage))
			.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl))
			.ForMember(dest => dest.ProductAmount, opt => opt.MapFrom(src => src.Product.ProductAmount))
			.ForMember(dest => dest.ProductCategories, opt => opt.MapFrom(src => src.Product.ProductCategories))
			.ForMember(dest => dest.ProductDesc, opt => opt.MapFrom(src => src.Product.ProductDesc))
			.ForMember(dest => dest.ProductDiscount, opt => opt.MapFrom(src => src.Product.ProductDiscount))
			.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.RetrieveProductId()))
			.ForMember(dest => dest.ProductInStock, opt => opt.MapFrom(src => src.Product.ProductInStock))
			.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
			.ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.ProductPrice))
			.ForMember(dest => dest.ProductStatus, opt => opt.MapFrom(src => src.Product.ProductStatus))
			.ForMember(dest => dest.IsSpecialOffer, opt => opt.MapFrom(src => src.Product.RetrieveSpecialOffer())));

			var mapper = map.CreateMapper();
			return mapper;
		}

		public IMapper OrdertoOrderViewModel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderViewModel>()
			.ForMember(dest => dest.ItemsInOrder, opt => opt.Ignore())
			.ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails))
			.ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.RetrieveOrderId()))
			.ForMember(dest => dest.SendAddress, opt => opt.MapFrom(src => src.SendAddress))
			.ForMember(dest => dest.SendPlace, opt => opt.MapFrom(src => src.SendPlace))
			.ForMember(dest => dest.SendZipcode, opt => opt.MapFrom(src => src.SendZipcode))
			.ForMember(dest => dest.User, opt => opt.Ignore()));

			var mapper = map.CreateMapper();
			return mapper;
		}

		public IMapper OrdertoUserOrderViewModel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<Order, UserOrderViewModel>()
			.ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.RetrieveOrderId()))
			.ForMember(dest => dest.SendAddress, opt => opt.MapFrom(src => src.SendAddress))
			.ForMember(dest => dest.SendPlace, opt => opt.MapFrom(src => src.SendPlace))
			.ForMember(dest => dest.SendZipcode, opt => opt.MapFrom(src => src.SendZipcode))
			.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.RetrieveCustomer().Email))
			.ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.RetrieveCustomer().FirstName))
			.ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.RetrieveCustomer().LastName))
			.ForMember(dest => dest.OrderDescription, opt => opt.MapFrom(src => src.OrderDetails))
			.ForMember(dest => dest.OrdersInSystem, opt => opt.Ignore())
			.ForMember(dest => dest.PaymentMethods, opt => opt.Ignore())
			.ForMember(dest => dest.ProductsInSystem, opt => opt.Ignore())
			.ForMember(dest => dest.ShoppingCart, opt => opt.Ignore())
			.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.RetrieveCustomer().ReturnUsername())));

			var mapper = map.CreateMapper();
			return mapper;
		}


		public IMapper PostToPostViewModel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<Post, PostViewModel>()
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RetrieveId()))
			.ForMember(dest => dest.Postdate, opt => opt.MapFrom(src => src.RetrieveDatePosted()))
			.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.RetrieveStatus()))
			.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.RetrieveTitle()))
			.ForMember(dest => dest.PostImage, opt => opt.MapFrom(src => src.RetrieveImage()))
			.ForMember(dest => dest.Excerpt, opt => opt.MapFrom(src => src.RetrieveExcerpt()))
			.ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.RetrieveContent())));
			
			var mapper = map.CreateMapper();
			return mapper;
		}

		public IMapper PostToPostMailViewModel()
		{
			var map = new MapperConfiguration(cfg => cfg.CreateMap<Post, PostMailViewModel>()
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RetrieveId()))
			.ForMember(dest => dest.Postdate, opt => opt.MapFrom(src => src.RetrieveDatePosted()))
			.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.RetrieveStatus()))
			.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.RetrieveTitle()))
			.ForMember(dest => dest.PostImage, opt => opt.MapFrom(src => src.RetrieveImage()))
			.ForMember(dest => dest.Excerpt, opt => opt.MapFrom(src => src.RetrieveExcerpt()))
			.ForMember(dest => dest.Email, opt => opt.Ignore())
			.ForMember(dest => dest.FullName, opt => opt.Ignore())
			.ForMember(dest => dest.Details, opt => opt.Ignore())
			.ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.RetrieveContent())));

			var mapper = map.CreateMapper();
			return mapper;
		}
	}
}

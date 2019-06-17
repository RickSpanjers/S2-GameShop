using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RicksWebWorld.Context.Release;
using RicksWebWorld.Models;
using RicksWebWorld.Repositories;
using RicksWebWorld.ViewModels;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using AutoMapper;

namespace RicksWebWorld.Controllers
{
	public class ProductController : Controller
	{
		//Make instances of repositories
		private readonly CategoryRepository categoryRep = new CategoryRepository(new CategoryMssqlContext());
		private readonly ProductRepository productRep = new ProductRepository(new ProductMssqlContext());
		private readonly SpecialOfferRepository offerRep = new SpecialOfferRepository(new SpecialOfferMssqlContext());
		private readonly RevisionRepository revisionRep = new RevisionRepository(new RevisionMssqlContext());
		private readonly AutoMapperExtension mapextension = new AutoMapperExtension();


		/// <summary>
		///     Return productoverview
		/// </summary>
		/// <returns>Actionresult productoverview</returns>
		public IActionResult Overview()
		{
			var model = new ProductCategoryViewModel();
			model.ListOfProducts = new List<ProductViewModel>();
			model.ListOfCategories = new List<CategoryOverviewViewModel>();

			foreach (var singleProduct in productRep.RetrieveAllProducts())
			{
				singleProduct.ProductCategories = categoryRep.RetrieveCategoriesFromProduct(singleProduct);
				var mapperOne = mapextension.ProductToProductviewmodel();
				ProductViewModel pmodel  = mapperOne.Map<ProductViewModel>(singleProduct);
				pmodel.IsSpecialOffer = singleProduct.RetrieveSpecialOffer();

				pmodel.ProductCategories = new List<CategoryOverviewViewModel>();
				foreach(Category c in categoryRep.RetrieveCategoriesFromProduct(singleProduct))
				{
					CategoryOverviewViewModel cmodel = new CategoryOverviewViewModel();
					cmodel.CategoryId = c.RetrieveCategoryId();
					cmodel.CategoryName = c.RetrieveCategoryName();
					pmodel.ProductCategories.Add(cmodel);
				}

				model.ListOfProducts.Add(pmodel);
			}
			foreach (Category c in categoryRep.RetrieveAllCategories())
			{
				var mapperTwo = mapextension.CategoryToCategoryOverviewViewModel();
				CategoryOverviewViewModel cmodel = mapperTwo.Map<CategoryOverviewViewModel>(c);
				model.ListOfCategories.Add(cmodel);
			}

			return View("Productoverview", model);
		}


		/// <summary>
		///     Return a single product
		/// </summary>
		/// <returns>Actionresult single product</returns>
		public IActionResult Single()
		{
			var model = new ProductViewModel();
			model.ListOfAllCategories = new List<CategoryOverviewViewModel>();
			foreach(Category c in categoryRep.RetrieveAllCategories())
			{
				var mapper = mapextension.CategoryToCategoryOverviewViewModel();
				CategoryOverviewViewModel cmodel = mapper.Map<CategoryOverviewViewModel>(c);
				model.ListOfAllCategories.Add(cmodel);
			}

			return View("SingleProduct", model);
		}


		/// <summary>
		///     Return a singleproduct
		/// </summary>
		/// <param name="selectedProduct">Identification of selected product</param>
		/// <returns>Return actionresult singleproduct edit</returns>
		public IActionResult SingleEdit(int selectedProduct)
		{
			Product selected = productRep.RetrieveProductById(selectedProduct);
			if (selected.RetrieveProductId() != 0)
			{
				IMapper mapper = mapextension.ProductToProductviewmodel();
				ProductViewModel model = mapper.Map<ProductViewModel>(selected);
				model.RevisionsInProduct = AutoMappingProductViewModel(selected).RevisionsInProduct;
				model.ProductCategories = AutoMappingProductViewModel(selected).ProductCategories;
				model.ListOfAllCategories = AutoMappingProductViewModel(selected).ListOfAllCategories;

				if (selected.ProductInStock != 0) { model.ProductInStock = "Yes"; }
				else { model.ProductInStock = "No"; }

				//Add offer if it has special offer
				var offer = offerRep.RetrieveOfferByProductID(selected.RetrieveProductId());
				if(offer != null)
				{
					model.OfferStart = offer.StartTime;
					model.OfferEnd = offer.EndTime;
					model.OfferPrice = offer.RetrieveOfferPrice();
				}			
				return View("SingleProduct", model);
			}
			return View("SingleProduct");
		}


		/// <summary>
		///     Delete product from system
		/// </summary>
		/// <param name="productToDelete">Identification of product</param>
		/// <returns>Actionresult productoverview</returns>
		[HttpPost]
		public ActionResult Delete(int productToDelete)
		{
			var selectedProduct = productRep.RetrieveProductById(productToDelete);
			if (selectedProduct.RetrieveProductId() != 0)
			{
				offerRep.DeleteSpecialOfferByProductId(selectedProduct);
				revisionRep.DeleteRevisionsByProductId(selectedProduct.RetrieveProductId());
				if (productRep.DeleteProductById(selectedProduct.RetrieveProductId()))
				{
					return RedirectToAction("Overview", "Product");
				}
				return RedirectToAction("Overview", "Product");
			}
			return RedirectToAction("Overview", "Product");
		}


		/// <summary>
		///     Save product to system
		/// </summary>
		/// <param name="model">ProductViewModel</param>
		/// <param name="selectedCategory">List of category identifications</param>
		/// <param name="uploadedImage">Image url</param>
		/// <returns>Actionresult productoverview</returns>
		[HttpPost]
		public ActionResult Add(ProductViewModel model, List<int> selectedCategory, string uploadedImage)
		{
			/*New product to add*/
			var product = new Product(1, model.ProductName);
			product.ProductAmount = model.ProductAmount;
			product.ProductDesc = model.ProductDesc;
			product.ProductCategories = new List<Category>();
			product.ProductDiscount = model.ProductDiscount;
			product.ProductPrice = model.ProductPrice;
			product.ProductStatus = model.ProductStatus;
			product.ImageUrl = uploadedImage;
			product.BtwPercentage = model.BtwPercentage;

			//Add all selected categories in system to model via viewmodels
			foreach (CategoryOverviewViewModel c in model.ProductCategories)
			{
				Category newcategory = categoryRep.RetrieveCategoryById(c.CategoryId);
				product.ProductCategories.Add(newcategory);
			}

			if (product.ProductDesc == null){product.ProductDesc = "No description";}
			if (product.ImageUrl == null){product.ImageUrl = "noimage.jpg";}
			if (model.ProductInStock == "Yes"){product.ProductInStock = 1;}
			else{product.ProductInStock = 0;}

			var offerController = new SpecialOfferController();
			var revisionController = new RevisionController();

			/*Creating the product*/
			if (productRep.CreateNewProduct(product))
			{
				var selectedProduct = productRep.RetrieveProductByName(product.ProductName);
				InsertingCategoriesIntoProduct(selectedCategory, selectedProduct);
				offerController.Add(model, selectedProduct);
				revisionController.Add(selectedProduct);
				return RedirectToAction("Overview", "Product");
			}
			return RedirectToAction("Single", "Product");
		}


		/// <summary>
		///     Insert categories for products
		/// </summary>
		/// <param name="categories">List of identifications for categories</param>
		/// <param name="p">Product</param>
		private void InsertingCategoriesIntoProduct(List<int> categories, Product p)
		{
			var categoriesToInsert = new List<Category>();
			var cc = new CategoryController();

			foreach (var item in categories)
			{
				var selectedCategory = categoryRep.RetrieveCategoryById(item);
				if (selectedCategory.RetrieveCategoryId() != 0) categoriesToInsert.Add(selectedCategory);
			}

			cc.InsertCategoriesForProduct(categoriesToInsert, p);
		}


		/// <summary>
		///     Edit product
		/// </summary>
		/// <param name="p">ProductViewModel</param>
		/// <param name="productToEdit">Id of product</param>
		/// <param name="selectedCategory">List of IDs from categories</param>
		/// <param name="uploadedImage">Image to upload</param>
		/// <returns>Actionresult productoverview</returns>
		[HttpPost]
		public ActionResult Update(ProductViewModel p, int productToEdit, List<int> selectedCategory, string uploadedImage)
		{
			var soc = new SpecialOfferController();
			var selectedProduct = productRep.RetrieveProductById(productToEdit);
			if (selectedProduct.RetrieveProductId() != 0)
			{
				Product newProduct = new Product(selectedProduct.RetrieveProductId(), p.ProductName);
				newProduct.ProductAmount = p.ProductAmount;
				newProduct.ProductDesc = p.ProductDesc;
				newProduct.ProductCategories = new List<Category>();
				newProduct.ProductDiscount = p.ProductDiscount;
				newProduct.ImageUrl = uploadedImage;
				newProduct.BtwPercentage = p.BtwPercentage;

				//Add all selected categories in system to model via viewmodels
				foreach (CategoryOverviewViewModel c in p.ProductCategories)
				{
					Category newcategory = categoryRep.RetrieveCategoryById(c.CategoryId);
					newProduct.ProductCategories.Add(newcategory);
				}
				if (p.ProductInStock == "Yes"){newProduct.ProductInStock = 1;}
				else{newProduct.ProductInStock = 0;}
				if (newProduct.ProductDesc == null){newProduct.ProductDesc = "No description";}
				if (newProduct.ImageUrl == null){newProduct.ImageUrl = "NoImage.jpg";}

				newProduct.ProductPrice = p.ProductPrice;
				newProduct.ProductStatus = p.ProductStatus;
				productRep.UpdateProductById(newProduct);
				soc.Update(p, newProduct);
				InsertingCategoriesIntoProduct(selectedCategory, newProduct);
				revisionRep.CreateNewRevision(new Revision(-1, newProduct));
				return RedirectToAction("Overview", "Product", selectedProduct);
			}
			return RedirectToAction("Single", "Product");
		}


		public ProductViewModel AutoMappingProductViewModel(Product p)
		{
			ProductViewModel model = new ProductViewModel();
			var soc = new SpecialOfferController();
			var rec = new RevisionController();

			//Add all revisions to product via viewmodels
			model.RevisionsInProduct = new List<RevisionViewModel>();
			foreach (Revision r in rec.RetrieveAllRevisionsByProductId(p.RetrieveProductId()))
			{
				var mapperOne = mapextension.RevisionToRevisionViewModel();
				RevisionViewModel rmodel = mapperOne.Map<RevisionViewModel>(r);
				model.RevisionsInProduct.Add(rmodel);
			}

			//Add all categories from product to model via viewmodels
			model.ProductCategories = new List<CategoryOverviewViewModel>();
			foreach (Category c in categoryRep.RetrieveCategoriesFromProduct(p))
			{
				var mapperTwo = mapextension.CategoryToCategoryOverviewViewModel();
				CategoryOverviewViewModel cmodel = mapperTwo.Map<CategoryOverviewViewModel>(c);
				model.ProductCategories.Add(cmodel);
			}

			//Add all categories in system to model via viewmodels
			model.ListOfAllCategories = new List<CategoryOverviewViewModel>();
			foreach (Category c in categoryRep.RetrieveAllCategories())
			{
				var mapperThree = mapextension.CategoryToCategoryOverviewViewModel();
				CategoryOverviewViewModel cmodelsystem = mapperThree.Map<CategoryOverviewViewModel>(c);
				model.ListOfAllCategories.Add(cmodelsystem);
			}

			return model;
		}
	}
}
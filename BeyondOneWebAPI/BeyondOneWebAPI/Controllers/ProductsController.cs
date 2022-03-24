using BeyondOneWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeyondOneWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly BeyondOneTestDBContext _dbContext;
        public ProductsController(ILogger<ProductsController> logger, BeyondOneTestDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }


        /// <summary>
        /// returning all products
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAllProducts")]
        public ActionResult Get()
        {
            try
            {
                var response = new APIResponse()
                {
                    Data = _dbContext.Products.Select(x => new
                    {
                        x.Id,
                        x.ProductId,
                        x.ProductName,

                    }).ToList(),
                    Message = ApiMessages.SuccessMessage
                };

                _logger.LogInformation("Returning time in ISO8601 format");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());//Log error details
                //return error details
                return BadRequest(new APIResponse()
                {
                    Errors = new string[] { ex.Message },
                    Message = ex.Message
                });
            }
        }

        /// <summary>
        /// This method will be used to add new product
        /// </summary>
        /// <param name="model">contains productId, productName and stock availability paramaters</param>
        /// <returns></returns>
        [HttpPost(Name = "AddProduct")]
        public ActionResult Add(ProductModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.ProductId)) //checking product id provided
                {
                    return BadRequest(new APIResponse()
                    {
                        Errors = new string[] { "Product Id Required" }
                    });
                }
                if (string.IsNullOrEmpty(model.ProductName)) //checking product name provided
                {
                    return BadRequest(new APIResponse()
                    {
                        Errors = new string[] { "Product name Required" }
                    });
                }

                var isProductIdExist = _dbContext.Products.Any(x => x.ProductId.ToLower() == model.ProductId.ToLower());//Check if product id unique or not.
                if (isProductIdExist)
                    return BadRequest(new APIResponse()
                    {
                        Errors = new string[] { "Product Id already exists, kindly provide unique Id." }
                    });

                var product = new Product()
                {
                    CreatedDateTime = DateTime.Now,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    StockAvailable = model.StockAvailable,

                };
                _dbContext.Products.Add(product);//Added the object
                _dbContext.SaveChanges();//Adding new record

                return Ok(new APIResponse()
                {
                    Data = product.Id,
                    Message = ApiMessages.SuccessMessage
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());//Log error details
                //return error details
                return BadRequest(new APIResponse()
                {
                    Errors = new string[] { ex.Message },
                    Message = ex.Message
                });
            }
        }

        /// <summary>
        /// This method will be used to update the record
        /// </summary>
        /// <param name="id">Unique ID of the record</param>
        /// <param name="model">contains productId, productName and stock availability paramaters</param>
        /// <returns></returns>
        [AcceptVerbs("Put", "Patch")]
        [Route("{id}")]
        public ActionResult PutProduct(int id, ProductModel model)
        {

            try
            {
                if (string.IsNullOrEmpty(model.ProductId)) //checking product id provided
                {
                    return BadRequest(new APIResponse()
                    {
                        Errors = new string[] { "Product Id Required" }
                    });
                }
                if (string.IsNullOrEmpty(model.ProductName)) //checking product name provided
                {
                    return BadRequest(new APIResponse()
                    {
                        Errors = new string[] { "Product name Required" }
                    });
                }

                var isProductIdExist = _dbContext.Products.Any(x => x.ProductId.ToLower() == model.ProductId.ToLower() && x.Id != id);//Check if product id unique or not except updating record.
                if (isProductIdExist)
                    return BadRequest(new APIResponse()
                    {
                        Errors = new string[] { "Product Id already exists, kindly provide unique Id." }
                    });

                var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);//Getting the record to update information
                if (product == null)
                    return BadRequest(new APIResponse()
                    {
                        Errors = new string[] { "No product found!" }
                    });

                //Assigning values to the DB parameters
                product.StockAvailable = model.StockAvailable;
                product.ProductName = model.ProductName;
                product.ProductId = model.ProductId;
                product.UpdateDateTime = DateTime.Now;

                _dbContext.SaveChanges();//Update record

                return Ok(new APIResponse()
                {
                    Data = product.Id,
                    Message = ApiMessages.SuccessMessage
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());//Log error details
                //return error details
                return BadRequest(new APIResponse()
                {
                    Errors = new string[] { ex.Message },
                    Message = ex.Message
                });
            }
        }
        /// <summary>
        /// This method will delete the product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);//Getting the record
                if (product == null)
                    return BadRequest(new APIResponse()
                    {
                        Errors = new string[] { "No product found!" }
                    });

                _dbContext.Products.Remove(product);//removing the record from the DB
                _dbContext.SaveChanges();//Saving latest changes

                _logger.LogInformation("Deleted the product and the id is: " + id);
                return Ok(new APIResponse()
                {
                    Message = ApiMessages.SuccessMessage
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());//Log error details
                //return error details
                return BadRequest(new APIResponse()
                {
                    Errors = new string[] { ex.Message },
                    Message = ex.Message
                });
            }
        }

        /// <summary>
        /// This method will return true if stock quantity avaiable in other case false.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckStockAvailability/{productId}")]
        public ActionResult CheckStockAvailability(string productId, int quantity)
        {
            try
            {
                if (string.IsNullOrEmpty(productId)) //checking product id provided
                {
                    return BadRequest(new APIResponse()
                    {
                        Errors = new string[] { "Product Id Required" }
                    });
                }
                if (quantity < 1) //checking product name provided
                {
                    return BadRequest(new APIResponse()
                    {
                        Errors = new string[] { "Quantity must be greater than 0" }
                    });
                }

                var product = _dbContext.Products.FirstOrDefault(x => x.ProductId.ToLower() == productId.ToLower());//Check if product exists
                if (product == null)
                    return BadRequest(new APIResponse()// if no product found
                    {
                        Errors = new string[] { "No Product found." }
                    });

                if (product.StockAvailable <= 0)
                    return BadRequest(new APIResponse()//if the product stock is less than or equal to 0
                    {
                        Errors = new string[] { "Not possible to fullfill the requirement as product quantity is less than 1." }
                    });
                _logger.LogInformation("Product with quantity found: product ID" + productId);
                return Ok(new APIResponse()
                {
                    Data = quantity <= product.StockAvailable ? true : false,
                    Message = ApiMessages.SuccessMessage
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());//Log error details
                //return error details
                return BadRequest(new APIResponse()
                {
                    Errors = new string[] { ex.Message },
                    Message = ex.Message
                });
            }
        }
    }
}
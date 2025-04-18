﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstAPI.Models;

namespace MyFirstAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{


		private readonly ShopContext _context;

		public ProductsController(ShopContext context)
		{
			_context = context;

			_context.Database.EnsureCreated();
		}




		//[HttpGet]
		//public IEnumerable<Product> GetAllProducts()// assumes everything went ok and a status code 200 is returned always
		//{
		//	return _context.Products.ToArray();
		//}


		//[HttpGet]
		//public ActionResult GetAllProducts2()
		//{
		//	return Ok(_context.Products.ToArray());
		//}


		//[HttpGet("{id}")]
		//public ActionResult GetProduct(int id)
		//{
		//	var product = _context.Products.Find(id);
		//	if (product == null)
		//	{
		//		return NotFound();
		//	}
		//	return Ok(product);
		//}



		[HttpGet]
		public async Task<ActionResult> GetAllProducts()
		{
			return Ok(await _context.Products.ToArrayAsync());
		}

        //GET - http://localhost:5220/api/products/33
        [HttpGet("{id}")]
		public async Task<ActionResult> GetProduct(int id)
		{
			var product = await _context.Products.FindAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}


		[HttpPost]
		public async Task<ActionResult<Product>> PostProduct(Product product)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest();
			}


			_context.Products.Add(product);
			await _context.SaveChangesAsync();

			return CreatedAtAction(
				"GetProduct",
				new { id = product.Id },
				product);
		}



		[HttpPut("{id}")]
		public async Task<ActionResult> PutProduct(int id, Product product)
		{
			if (id != product.Id)
			{
				return BadRequest();
			}

			_context.Entry(product).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_context.Products.Any(p => p.Id == id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

        //DELETE - http://localhost:5220/api/products/34
        [HttpDelete("{id}")]
		public async Task<ActionResult<Product>> DeleteProduct(int id)
		{
			var product = await _context.Products.FindAsync(id);
			if (product == null)
			{
				return NotFound();
			}

			_context.Products.Remove(product);
			await _context.SaveChangesAsync();

			return product;
		}

        //http://localhost:5220/api/products/delete?ids=1&ids=2&ids=3
        [HttpPost]
		[Route("Delete")]
		public async Task<ActionResult> DeleteMultiple([FromQuery] int[] ids)
		{
			var products = new List<Product>();
			foreach (var id in ids)
			{
				var product = await _context.Products.FindAsync(id);

				if (product == null)
				{
					return NotFound();
				}

				products.Add(product);
			}

			_context.Products.RemoveRange(products);
			await _context.SaveChangesAsync();

			return Ok(products);
		}


	}




}

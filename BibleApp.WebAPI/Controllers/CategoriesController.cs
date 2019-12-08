using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BibleApp.Data;
using BibleApp.Models;
using BibleApp.Services;
using Microsoft.AspNet.Identity;

namespace BibleApp.WebAPI.Controllers
{
    public class CategoriesController : ApiController
    {
        private CategoriesService CreateCategoriesService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var categoriesService = new CategoriesService(userId);
            return categoriesService;
        }

        [HttpPost]
        public IHttpActionResult Post(CategoriesCreate categories)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCategoriesService();

            if (!service.CreateCategory(categories))
                return InternalServerError();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetCategories()
        {
            CategoriesService categoriesService = CreateCategoriesService();
            var categories = categoriesService.GetCategories();
            return Ok(categories);
        }

        [HttpGet]
        public IHttpActionResult GetCategoriesByID(int id)
        {
            CategoriesService categoriesService = CreateCategoriesService();
            var categories = categoriesService.GetCategoryByID(id);
            return Ok(categories);
        }

        [HttpPut]
        public IHttpActionResult Put(CategoriesEdit categories)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCategoriesService();

            if (!service.UpdateCategory(categories))
                return InternalServerError();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateCategoriesService();

            if (!service.DeleteCategory(id))
                return InternalServerError();

            return Ok();
        }
    }
}

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
    public class VerseController : ApiController
    {
        private VerseService CreateVerseService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var verseService = new VerseService(userId);
            return verseService;
        }

        [HttpPost]
        public IHttpActionResult Post(VerseCreate verse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateVerseService();

            if (!service.CreateVerse(verse))
                return InternalServerError();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetVerses()
        {
            VerseService verseService = CreateVerseService();
            var verse = verseService.GetVerses();
            return Ok(verse);
        }

        [HttpGet]
        public IHttpActionResult GetVerseByID(int id)
        {
            VerseService verseService = CreateVerseService();
            var verse = verseService.GetVerseByID(id);
            return Ok(verse);
        }

        [HttpGet]
        [Route("api/AllVerses")]
        public IHttpActionResult GetAllVerses()
        {
            VerseService verseService = CreateVerseService();
            var verse = verseService.GetAllVerses();
            return Ok(verse);
        }

        [HttpGet]
        [Route("api/RandomVerse")]
        public IHttpActionResult GetRandomVerse()
        {
            VerseService verseService = CreateVerseService();
            var verse = verseService.GetRandomVerse();
            return Ok(verse);
        }

        [HttpPut]
        public IHttpActionResult Put(VerseEdit verse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateVerseService();

            if (!service.UpdateVerse(verse))
                return InternalServerError();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateVerseService();

            if (!service.DeleteVerse(id))
                return InternalServerError();

            return Ok();
        }
    }
}

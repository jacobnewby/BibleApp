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
    public class VideoController : ApiController
    {
        private VideoService CreateVideoService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var videoService = new VideoService(userId);
            return videoService;
        }

        [HttpPost]
        public IHttpActionResult Post(VideoCreate video)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateVideoService();

            if (!service.CreateVideo(video))
                return InternalServerError();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetVideos()
        {
            VideoService videoService = CreateVideoService();
            var video = videoService.GetVideos();
            return Ok(video);
        }

        [HttpGet]
        public IHttpActionResult GetVideoByID(int id)
        {
            VideoService videoService = CreateVideoService();
            var video = videoService.GetVideoByID(id);
            return Ok(video);
        }

        [HttpGet]
        [Route("api/AllVideos")]
        public IHttpActionResult GetAllVideos()
        {
            VideoService videoService = CreateVideoService();
            var video = videoService.GetAllVideos();
            return Ok(video);
        }

        [HttpGet]
        [Route("api/RandomVideo")]
        public IHttpActionResult GetRandomVideo()
        {
            VideoService videoService = CreateVideoService();
            var video = videoService.GetRandomVideo();
            return Ok(video);
        }

        [HttpPut]
        public IHttpActionResult Put(VideoEdit video)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateVideoService();

            if (!service.UpdateVideo(video))
                return InternalServerError();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateVideoService();

            if (!service.DeleteVideo(id))
                return InternalServerError();

            return Ok();
        }
    }
}

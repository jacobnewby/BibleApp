using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibleApp.Data;
using BibleApp.Models;

namespace BibleApp.Services
{
    public class VideoService
    {
        private readonly Guid _userID;

        public VideoService(Guid userid)
        {
            _userID = userid;
        }

        public bool CreateVideo(VideoCreate model)
        {
            var entity =
                new Video()
                {
                    UserID = _userID,
                    CategoryID = model.CategoryID,
                    VideoLink = model.VideoLink,
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Video.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<VideoListItem> GetVideos()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Video
                    .Where(e => e.UserID == _userID)
                    .Select(
                        e =>
                            new VideoListItem
                            {
                                VideoID = e.VideoID,
                                CategoryID = e.CategoryID,
                                VideoLink = e.VideoLink,
                            }
                        );
                return query.ToArray();
            }
        }

        public VideoDetail GetVideoByID(int VideoID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Video
                    .Single(e => e.VideoID == VideoID && e.UserID == _userID);
                return
                    new VideoDetail
                    {
                        VideoID = entity.VideoID,
                        CategoryID = entity.CategoryID,
                        VideoLink = entity.VideoLink,
                    };
            }
        }

        public IEnumerable<VideoListItem> GetAllVideos()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Video
                    .Select(
                        e =>
                            new VideoListItem
                            {
                                VideoLink = e.VideoLink,
                            }
                        );
                return query.ToList();
            }
        }

        public IEnumerable<VideoListItem> GetRandomVideo()
        {
            var list = GetAllVideos().ToList();
            Random rnd = new Random();
            var verse = rnd.Next(list.Count);
            return new List<VideoListItem> { list[verse] };
        }

        public bool UpdateVideo(VideoEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Video
                    .Single(e => e.VideoID == model.VideoID && e.UserID == _userID);

                entity.VideoID = model.VideoID;
                entity.CategoryID = model.CategoryID;
                entity.VideoLink = model.VideoLink;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteVideo(int VideoID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Video
                    .Single(e => e.VideoID == VideoID && e.UserID == _userID);

                ctx.Video.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

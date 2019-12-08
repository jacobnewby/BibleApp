using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibleApp.Data;
using BibleApp.Models;

namespace BibleApp.Services
{
    public class VerseService
    {
        private readonly Guid _userID;

        public VerseService(Guid userId)
        {
            _userID = userId;
        }

        public bool CreateVerse(VerseCreate model)
        {
            var entity =
                new Verse()
                {
                    UserID = _userID,
                    CategoryID = model.CategoryID,
                    VerseReference = model.VerseReference,
                    VerseContent = model.VerseContent,
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Verse.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<VerseListItem> GetVerses()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Verse
                    .Where(e => e.UserID == _userID)
                    .Select(
                        e =>
                            new VerseListItem
                            {
                                VerseID = e.VerseID,
                                CategoryID = e.CategoryID,
                                VerseReference = e.VerseReference,
                                VerseContent = e.VerseContent,
                            }
                        );
                return query.ToArray();
            }
        }

        public VerseDetail GetVerseByID(int VerseID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Verse
                    .Single(e => e.VerseID == VerseID && e.UserID == _userID);
                return
                    new VerseDetail
                    {
                        VerseID = entity.VerseID,
                        CategoryID = entity.CategoryID,
                        VerseReference = entity.VerseReference,
                        VerseContent = entity.VerseContent,
                    };
            }
        }

        public IEnumerable<VerseListItem> GetAllVerses()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Verse
                    .Select(
                        e =>
                            new VerseListItem
                            {
                                VerseContent = e.VerseContent,
                                VerseReference = e.VerseReference,
                            }
                        );
                return query.ToList();
            }
        }

        public IEnumerable<VerseListItem> GetRandomVerse()
        {
            var list = GetAllVerses().ToList();
            Random rnd = new Random();
            var verse = rnd.Next(list.Count);
            return new List<VerseListItem> { list[verse] };
        }

        public bool UpdateVerse(VerseEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Verse
                    .Single(e => e.VerseID == model.VerseID && e.UserID == _userID);

                entity.VerseID = model.VerseID;
                entity.CategoryID = model.CategoryID;
                entity.VerseReference = model.VerseReference;
                entity.VerseContent = model.VerseContent;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteVerse(int VerseID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Verse
                    .Single(e => e.VerseID == VerseID && e.UserID == _userID);

                ctx.Verse.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

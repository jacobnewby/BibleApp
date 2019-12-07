using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibleApp.Data;
using BibleApp.Models;

namespace BibleApp.Services
{
    public class CategoriesService
    {
        private readonly Guid _userID;

        public CategoriesService(Guid userid)
        {
            _userID = userid;
        }

        public bool CreateCategory(CategoiesCreate model)
        {
            var entity =
                new Categories()
                {
                    UserID = _userID,
                    Name = model.Name,
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CategoriesListItem> GetCategories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Categories
                    .Where(e => e.UserID == _userID)
                    .Select(
                        e =>
                            new CategoriesListItem
                            {
                                CategoryID = e.CategoryID,
                                Name = e.Name,
                            }
                        );
                return query.ToArray();
            }
        }

        public CategoriesDetail GetCategoryByID(int CategoryID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Categories
                    .Single(e => e.CategoryID == CategoryID && e.UserID == _userID);
                return
                    new CategoriesDetail
                    {
                        CategoryID = entity.CategoryID,
                        Name = entity.Name,
                    };
            }
        }

        public bool UpdateCategory(CategoriesEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Categories
                    .Single(e => e.CategoryID == model.CategoryID && e.UserID == _userID);

                entity.CategoryID = model.CategoryID;
                entity.Name = model.Name;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCategory(int CategoryID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Categories
                    .Single(e => e.CategoryID == CategoryID && e.UserID == _userID);

                ctx.Categories.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

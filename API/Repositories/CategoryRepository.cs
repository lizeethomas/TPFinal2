using System.Linq;
using TPFinalStrasbourg.Models;
using TPFinalStrasbourg.Tools;

namespace TPFinalStrasbourg.Repositories;

public class CategoryRepository : BaseRepository<Category>
{
    public CategoryRepository(DataDbContext dataContext) : base(dataContext)
    {
    }

    public override List<Category> FindAll()
    {
        return _dataContext.Categories.ToList();
    }

    public override Category FindById(int id)
    {
        return _dataContext.Categories.FirstOrDefault(c => c.Id == id);
    }

    public override bool Save(Category element)
    {
        _dataContext.Categories.Add(element);
        return Update();
    }

    public override List<Category> SearchAll(Func<Category, bool> SearchMethod)
    {
        return _dataContext.Categories.Where(SearchMethod).ToList();
    }

    public override Category SearchOne(Func<Category, bool> SearchMethod)
    {
        return _dataContext.Categories.FirstOrDefault(SearchMethod);
    }
}

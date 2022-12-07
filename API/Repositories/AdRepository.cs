using TPFinalStrasbourg.Tools;
using TPFinalStrasbourg.Models;
using Microsoft.EntityFrameworkCore;

namespace TPFinalStrasbourg.Repositories;

public class AdRepository:BaseRepository<Ad>
{
    public AdRepository(DataDbContext dataContext) : base(dataContext)
    {
    }

    public bool Delete(Ad element)
    {
        _dataContext.Remove(element);
        return Update();
    }

    public override List<Ad> FindAll()
    {
        return _dataContext.Ads.Include(a => a.Images).Include(a => a.Comments).Include(a => a.Category).ToList();
    }

    public override Ad FindById(int id)
    {
        return _dataContext.Ads.Include(a => a.Images).Include(a => a.Comments).Include(a => a.Category).FirstOrDefault(i => i.Id == id);
    }

    public override bool Save(Ad element)
    {
        _dataContext.Ads.Add(element);
        return Update();
    }

    public override List<Ad> SearchAll(Func<Ad, bool> SearchMethod)
    {
        return _dataContext.Ads.Include(a => a.Images).Include(a => a.Comments).Include(a => a.Category).Where(SearchMethod).ToList();
    }

    public override Ad SearchOne(Func<Ad, bool> SearchMethod)
    {
        return _dataContext.Ads.Include(a => a.Images).Include(a => a.Comments).Include(a => a.Category).FirstOrDefault(SearchMethod);
    }
}
using TPFinalStrasbourg.Tools;
using TPFinalStrasbourg.Models;
using Microsoft.EntityFrameworkCore;

namespace TPFinalStrasbourg.Repositories;

public class UserRepository : BaseRepository<User>
{
    public UserRepository(DataDbContext dataContext) : base(dataContext)
    {
    }

    public override List<User> FindAll()
    {
        return _dataContext.Users.Include(u => u.Ads).ThenInclude(a => a.Images).Include(u => u.Ads).ThenInclude(a => a.Comments).Include(u => u.Role).ToList();
    }

    public override User FindById(int id)
    {
       return _dataContext.Users.Include(u => u.Ads).ThenInclude(a => a.Images).Include(u => u.Ads).ThenInclude(a => a.Comments).Include(u => u.Role).FirstOrDefault(u => u.Id == id);
    }

    public override bool Save(User element)
    {
        _dataContext.Users.Add(element);
        return Update();
    }

    public override List<User> SearchAll(Func<User, bool> SearchMethod)
    {
        return _dataContext.Users.Include(u => u.Ads).ThenInclude(a => a.Images).Include(u => u.Ads).ThenInclude(a => a.Comments).Include(u => u.Role).Where(SearchMethod).ToList();
    }

    public override User SearchOne(Func<User, bool> SearchMethod)
    {
        return _dataContext.Users.Include(u => u.Ads).ThenInclude(a => a.Images).Include(u => u.Ads).ThenInclude(a => a.Comments).Include(u => u.Role).FirstOrDefault(SearchMethod);
    }
}


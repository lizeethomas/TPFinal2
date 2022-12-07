using TPFinalStrasbourg.Repositories;
using TPFinalStrasbourg.Models;
using TPFinalStrasbourg.Tools;

namespace TPFinalStrasbourg.Repositories;

public class RoleRepository : BaseRepository<Role>
{
    public RoleRepository(DataDbContext dataContext) : base(dataContext)
    {
    }

    public override List<Role> FindAll()
    {
        throw new NotImplementedException();
    }

    public override Role FindById(int id)
    {
        return _dataContext.Roles.FirstOrDefault(r => r.Id == id);
    }

    public override bool Save(Role element)
    {
        throw new NotImplementedException();
    }

    public override List<Role> SearchAll(Func<Role, bool> SearchMethod)
    {
        throw new NotImplementedException();
    }

    public override Role SearchOne(Func<Role, bool> SearchMethod)
    {
        throw new NotImplementedException();
    }
}

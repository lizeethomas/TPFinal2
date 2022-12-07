using TPFinalStrasbourg.Models;
using TPFinalStrasbourg.Tools;

namespace TPFinalStrasbourg.Repositories;

public class CommentRepository : BaseRepository<Comment>
{
    public CommentRepository(DataDbContext dataContext):base(dataContext)
    {       
    }

    public override List<Comment> FindAll()
    {
        return _dataContext.Comments.ToList();
    }

    public override Comment FindById(int id)
    {
        return _dataContext.Comments.FirstOrDefault(c => c.Id == id);
    }

    public override bool Save(Comment element)
    {
        _dataContext.Comments.Add(element);
        return Update();
    }

    public override List<Comment> SearchAll(Func<Comment, bool> SearchMethod)
    {
        return _dataContext.Comments.Where(SearchMethod).ToList();
    }

    public override Comment SearchOne(Func<Comment, bool> SearchMethod)
    {
        return _dataContext.Comments.FirstOrDefault(SearchMethod);
    }
}

using TPFinalStrasbourg.Tools;

namespace TPFinalStrasbourg.Repositories
{
    public abstract class BaseRepository<T>
    {
        protected DataDbContext _dataContext { get; set; }

        public BaseRepository(DataDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public virtual bool Update()
        {
            return _dataContext.SaveChanges() > 0;
        }

        public abstract bool Save(T element);

        public abstract T FindById(int id);

        public abstract List<T> FindAll();

        public abstract T SearchOne(Func<T, bool> SearchMethod);

        public abstract List<T> SearchAll(Func<T, bool> SearchMethod);

    }
}
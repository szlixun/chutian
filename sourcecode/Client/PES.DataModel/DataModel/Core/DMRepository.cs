using PES.DataModel;

namespace PES.DataModel
{
    public static class DMRepository
    {
        #region Repository

        public static IBaseRepository<TEntity> Get<TEntity>()
        {
            return new BaseRepository<TEntity>();
        }

        public static IBaseRepository Get()
        {
            return new BaseRepository();
        }

        public static IBaseRepository Get(string connectionString)
        {
            return new BaseRepository(connectionString);
        }

        public static TService GetService<TService>() where TService : class
        {
            return DMContext.GetService<TService>();
        }

        public static void Register<TService, TImpl>()
            where TService : class
            where TImpl : class, TService
        {
            DMContext.Register<TService, TImpl>();
        }

        #endregion Repository
    }
}
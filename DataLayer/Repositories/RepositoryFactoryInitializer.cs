namespace DataLayer.Repositories
{
    public static class RepositoryFactoryInitializer
    {
        public static void Initialize() =>
            BusinessLayer.Factory.RepositoryFactory.SetRepositoryFactory(new RepositoryFactory());
    }
}
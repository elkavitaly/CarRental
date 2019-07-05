namespace DataLayer.Repositories
{
    /// <summary>
    /// Initializer for Repository Factory
    /// </summary>
    public static class RepositoryFactoryInitializer
    {
        public static void Initialize() =>
            BusinessLayer.Factory.RepositoryFactory.SetRepositoryFactory(new RepositoryFactory());
    }
}
namespace CommonDialogs.Maui
{
    public static class FileSystemOperationExtension
    {
        public static void AddFileSystemOperations(this IServiceCollection services)
        {
            services.AddSingleton<IFileSystemOperations, FileSystemOperations>();
        }
    }
}

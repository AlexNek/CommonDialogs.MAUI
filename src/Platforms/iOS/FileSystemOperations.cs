namespace CommonDialogs.Maui
{
    internal partial class FileSystemOperations
    {
        internal FileSystemPickResult? CreateFilePlatform(string platformFolderPath, string childPath)
        {
            throw new NotSupportedException();
        }

        internal FileSystemPickResult? CreateFolderPlatform(string platformFolderPath, string childPath)
        {
            throw new NotSupportedException();
        }

        internal Stream? OpenPickedFilePlatform(string platformPath, string fileOpenMode)
        {
            throw new NotImplementedException();
        }

        internal Task<FileSystemPickResult?> PickFilePlatformAsync(FilePickOptions? pickOptions)
        {
            throw new NotImplementedException();
        }

        internal Task<IEnumerable<FileSystemPickResult>> PickFilesPlatformAsync(FilePickOptions? pickOptions)
        {
            throw new NotImplementedException();
        }

        internal Task<FileSystemPickResult?> PickFolderPlatformAsync(FilePickOptions? pickOptions)
        {
            throw new NotImplementedException();
        }
    }
}

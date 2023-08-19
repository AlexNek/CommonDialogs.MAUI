namespace CommonDialogs.Maui
{
    public partial class FilePicker
    {
        internal FilePickResult? CreateFilePlatform(string platformFolderPath, string childPath)
        {
            throw new NotSupportedException();
        }

        internal FilePickResult? CreateFolderPlatform(string platformFolderPath, string childPath)
        {
            throw new NotSupportedException();
        }

        internal Stream? OpenPickedFilePlatform(string platformPath, string fileOpenMode)
        {
            throw new NotImplementedException();
        }

        internal Task<FilePickResult?> PickFilePlatformAsync(FilePickOptions? pickOptions)
        {
            throw new NotImplementedException();
        }

        internal Task<IEnumerable<FilePickResult>> PickFilesPlatformAsync(FilePickOptions? pickOptions)
        {
            throw new NotImplementedException();
        }

        internal Task<FilePickResult?> PickFolderPlatformAsync(FilePickOptions? pickOptions)
        {
            throw new NotImplementedException();
        }
    }
}

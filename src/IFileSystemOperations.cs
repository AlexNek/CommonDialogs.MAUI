namespace CommonDialogs.Maui
{
    /// <summary>
    /// Interface for file pickers.
    /// </summary>
    public interface IFileSystemOperations
    {
        /// <summary>
        /// Create file
        /// </summary>
        /// <param name="platformFolderPath">the path from picked result or created from CreateFolder</param>
        /// <param name="childPath">"etc,folder/file.txt or file.txt"</param>
        /// <returns></returns>
        FileSystemPickResult? CreateFile(string platformFolderPath, string childPath);

        /// <summary>
        /// Create folder
        /// </summary>
        /// <param name="platformFolderPath">the path from picked result or created from CreateFolder</param>
        /// <param name="childPath">"etc,folder/file.txt or file.txt"</param>
        /// <returns></returns>
        FileSystemPickResult? CreateFolder(string platformFolderPath, string childPath);

        /// <summary>
        /// Open picked file
        /// </summary>
        /// <param name="platformPath">the path from picked result</param>
        /// <param name="fileOpenMode">"r","w","rw"</param>
        /// <returns></returns>
        Stream? OpenFile(string platformPath, string fileOpenMode);

        /// <summary>
        /// Pick single file
        /// </summary>
        /// <param name="pickOptions">can be null</param>
        /// <returns></returns>
        Task<FileSystemPickResult?> PickFileAsync(FilePickOptions? pickOptions);

        /// <summary>
        /// Pck multiple files
        /// </summary>
        /// <param name="pickOptions">can be null</param>
        /// <returns></returns>
        Task<IEnumerable<FileSystemPickResult>> PickFilesAsync(FilePickOptions? pickOptions);

        /// <summary>
        /// Pick a folder
        /// </summary>
        /// <param name="pickOptions">set display title</param>
        /// <returns></returns>
        Task<FileSystemPickResult?> PickFolderAsync(FilePickOptions? pickOptions);
    }
}

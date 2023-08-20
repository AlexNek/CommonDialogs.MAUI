namespace CommonDialogs.Maui
{
    /// <summary>
    /// Class FilePicker. Represents a file picker object for selecting files.
    /// Implements the <see cref="CommonOperations.Maui.IFilePicker" />
    /// </summary>
    /// <seealso cref="CommonOperations.Maui.IFilePicker" />
    internal partial class FileSystemOperations : IFileSystemOperations
    {
        /// <summary>
        /// create file
        /// </summary>
        /// <param name="platformFolderPath">the path from picked result or created from CreateFolder</param>
        /// <param name="childPath">"etc,folder/file.txt or file.txt"</param>
        /// <returns>System.Nullable&lt;FilePickResult&gt;.</returns>
        public FilePickResult? CreateFile(string platformFolderPath, string childPath)
        {
            return CreateFilePlatform(platformFolderPath, childPath);
        }

        /// <summary>
        /// create folder
        /// </summary>
        /// <param name="platformFolderPath">the path from picked result or created from CreateFolder</param>
        /// <param name="childPath">"etc,folder/file.txt or file.txt"</param>
        /// <returns>System.Nullable&lt;FilePickResult&gt;.</returns>
        public FilePickResult? CreateFolder(string platformFolderPath, string childPath)
        {
            return CreateFolderPlatform(platformFolderPath, childPath);
        }

        /// <summary>
        /// open picked file
        /// </summary>
        /// <param name="platformPath">the path from picked result</param>
        /// <param name="fileOpenMode">"r","w","rw"</param>
        /// <returns>System.Nullable&lt;Stream&gt;.</returns>
        public Stream? OpenFile(string platformPath, string fileOpenMode)
        {
            return OpenPickedFilePlatform(platformPath, fileOpenMode);
        }

        /// <summary>
        /// pick single file
        /// </summary>
        /// <param name="pickOptions">can be null</param>
        /// <returns>Task&lt;System.Nullable&lt;FilePickResult&gt;&gt;.</returns>
        public Task<FilePickResult?> PickFileAsync(FilePickOptions? pickOptions)
        {
            return PickFilePlatformAsync(pickOptions);
        }

        /// <summary>
        /// pick mutilple files
        /// </summary>
        /// <param name="pickOptions">can be null</param>
        /// <returns>Task&lt;IEnumerable&lt;FilePickResult&gt;&gt;.</returns>
        public Task<IEnumerable<FilePickResult>> PickFilesAsync(FilePickOptions? pickOptions)
        {
            return PickFilesPlatformAsync(pickOptions);
        }

        /// <summary>
        /// pick a folder to create file in it
        /// </summary>
        /// <param name="pickOptions">set display title</param>
        /// <returns>Task&lt;System.Nullable&lt;FilePickResult&gt;&gt;.</returns>
        public Task<FilePickResult?> PickFolderAsync(FilePickOptions? pickOptions)
        {
            return PickFolderPlatformAsync(pickOptions);
        }
    }
}

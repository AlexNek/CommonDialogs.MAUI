namespace CommonDialogs.Maui
{
    /// <summary>
    /// Represents the result of a file pick operation.
    /// </summary>
    public class FileSystemPickResult
    {
        /// <summary>
        /// Initializes a new instance of the FilePickResult class.
        /// </summary>
        /// <param name="name">The name of the file.</param>
        /// <param name="fullPath">The full path of the file.</param>
        /// <param name="platformPath">The platform-specific path of the file.</param>
        public FileSystemPickResult(string? name, string? fullPath, string? platformPath)
        {
            Name = name;
            FullPath = fullPath;
            PlatformPath = platformPath;
        }

        public string? Name { get; }

        public string? FullPath { get; }

        /// <summary>
        /// Platform path for read or write
        /// </summary>
        public string? PlatformPath { get; }
    }
}

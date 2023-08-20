namespace CommonDialogs.Maui
{
    /// <summary>
    /// Represents the result of a file pick operation.
    /// </summary>
    public class FileSystemPickResult
    {
        /// <summary>
        /// Represents the result of picking a file system item.
        /// </summary>
        /// <param name="name">The name of the file system item.</param>
        /// <param name="fullPath">The full path of the file system item.</param>
        /// <param name="platformPath">The platform specific path of the file system item.</param>
        public FileSystemPickResult(string? name, string? fullPath, string? platformPath)
        {
            Name = name;
            FullPath = fullPath;
            PlatformPath = platformPath;
        }

        /// <summary>
        /// Gets the name of a file system item.
        /// </summary>
        public string? Name { get; }

        /// <summary>
        /// Gets the full path of the file system item.
        /// </summary>
        public string? FullPath { get; }

        /// <summary>
        /// Gets the platform specific path of the file system item
        /// </summary>
        public string? PlatformPath { get; }
    }
}

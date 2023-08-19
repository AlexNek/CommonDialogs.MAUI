namespace CommonDialogs.Maui
{
    /// <summary>
    /// Represents the result of a file pick operation.
    /// </summary>
    public class FilePickResult
    {
        /// <summary>
        /// Initializes a new instance of the FilePickResult class.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="fullPath">The full path of the file.</param>
        /// <param name="platformPath">The platform-specific path of the file.</param>
        public FilePickResult(string? fileName, string? fullPath, string? platformPath)
        {
            FileName = fileName;
            FullPath = fullPath;
            PlatformPath = platformPath;
        }

        public string? FileName { get; }

        public string? FullPath { get; }

        /// <summary>
        /// Platform path for read or write
        /// </summary>
        public string? PlatformPath { get; }
    }
}

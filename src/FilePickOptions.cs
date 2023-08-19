namespace CommonDialogs.Maui
{
    /// <summary>
    /// Class FilePickOptions. Represents the options for picking a file.
    /// Implements the <see cref="Microsoft.Maui.Storage.PickOptions" />
    /// </summary>
    /// <seealso cref="Microsoft.Maui.Storage.PickOptions" />
    public class FilePickOptions : PickOptions
    {
        private static readonly FilePickerFileType[] FilePickerFileTypes =
            {
                new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                        {
                            { DevicePlatform.Android, new[] { "image/*" } },
                            { DevicePlatform.WinUI, new[] { "*.png", "*.jpg", "*.jpeg", "*.webp", "*.gif", "*.bmp", "" } }
                        }),
                new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                        {
                            { DevicePlatform.Android, new[] { "audio/*" } },
                            { DevicePlatform.WinUI, new[] { "*.mp3", "*.wav", "*.flac", "*.m4a", "*.midi", "*.ogg", "*.ape", "*.alac", "*.aac", } }
                        }),
                new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                        {
                            { DevicePlatform.Android, new[] { "video/*" } },
                            { DevicePlatform.WinUI, new[] { "*.mp4", "*.rmvb", "*.mkv", "*.3gp", "*.wmv", "*.mov", "*.rm", "*.flv" } }
                        }),
                new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                        {
                            { DevicePlatform.Android, new[] { "*/*" } }, { DevicePlatform.WinUI, new[] { "" } }
                        }),
                new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                        {
                            { DevicePlatform.Android, new[] { "text/*", "application/*" } },
                            { DevicePlatform.WinUI, new[] { "*.txt", "*.csv", "*.lrc", "*.srt", "*.ass", } }
                        }),
            };

        public static readonly FilePickOptions Audios = new FilePickOptions { FileTypes = FilePickerFileTypes[1] };

        public new static readonly FilePickOptions Images = new FilePickOptions { FileTypes = FilePickerFileTypes[0] };

        public static readonly FilePickOptions Texts = new FilePickOptions { FileTypes = FilePickerFileTypes[4] };

        public static readonly FilePickOptions Videos = new FilePickOptions { FileTypes = FilePickerFileTypes[2] };

        /// <summary>
        /// take persistable permission after application restart or not
        /// </summary>
        public bool HoldPermission { get; set; } = true;
    }
}

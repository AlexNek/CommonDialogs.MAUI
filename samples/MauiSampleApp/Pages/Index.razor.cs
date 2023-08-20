using CommonDialogs.Maui;

namespace MauiSampleApp.Pages
{
    public partial class Index
    {
        private FilePickResult? _filePickResult;

        private IEnumerable<FilePickResult>? _filesPickResult;

        private FilePickResult? _folderPickResult;

        private async Task OnSelectFile()
        {
            _filePickResult = await CommonOperations.PickFileAsync(FilePickOptions.All);
        }

        private async Task OnSelectFiles()
        {
            _filesPickResult = await CommonOperations.PickFilesAsync(FilePickOptions.All);
        }

        private async Task OnSelectFolder()
        {
            _folderPickResult = await CommonOperations.PickFolderAsync(new FilePickOptions { PickerTitle = "Select Test Folder" });
        }
    }
}

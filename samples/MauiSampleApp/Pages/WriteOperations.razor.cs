using CommonDialogs.Maui;

using Microsoft.AspNetCore.Components;

namespace MauiSampleApp.Pages
{
    public partial class WriteOperations : ComponentBase
    {
        private const string TestFileName = "test.txt";

        private string? _errorText1;

        private string? _errorText2;

        private string? _fileContent1;

        private string? _fileContent2;

        private FilePickResult? _folderPickResult;

        private string? _resultText1;

        private string? _resultText2;

        private async Task OnCreateFile()
        {
            try
            {
                await Task.Delay(0);
                _errorText1 = string.Empty;
                _resultText1 = String.Empty;
                if (_folderPickResult != null)
                {
                    var res = CommonDialogs.Maui.CommonOperations.CreateFile(_folderPickResult.PlatformPath, TestFileName);
                    using (var stream = CommonDialogs.Maui.CommonOperations.OpenFile(res.PlatformPath, "w"))
                    {
                        using var sw = new StreamWriter(stream);
                        sw.Write("Some text");
                        _resultText1 = "File created";
                    }

                    using (var streamRead = CommonDialogs.Maui.CommonOperations.OpenFile(res.PlatformPath, "r"))
                    {
                        using var textReader = new StreamReader(streamRead);
                        _fileContent1 = textReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _errorText1 = ex.Message;
                _errorText1 = "Error";
            }
        }

        private async Task OnCreateFileAndFolders()
        {
            try
            {
                await Task.Delay(0);
                _errorText2 = string.Empty;
                _resultText2 = String.Empty;
                if (_folderPickResult != null)
                {
                    string newPath = Path.Combine("SubfolerTest01", "subfolderTest02", TestFileName);
                    var res = CommonDialogs.Maui.CommonOperations.CreateFile(_folderPickResult.PlatformPath, newPath);
                    using (var stream = CommonDialogs.Maui.CommonOperations.OpenFile(res.PlatformPath, "w"))
                    {
                        using (var sw = new StreamWriter(stream))
                        {
                            sw.Write("Some text");
                            _resultText2 = "File created";
                        }
                    }

                    using var streamRead = CommonDialogs.Maui.CommonOperations.OpenFile(res.PlatformPath, "r");

                    using var textReader = new StreamReader(streamRead);
                    _fileContent2 = textReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _errorText2 = ex.Message;
                _errorText2 = "Error";
            }
        }

        private async Task OnSelectFolder()
        {
            var folderPickResult = await CommonDialogs.Maui.CommonOperations.PickFolderAsync(new FilePickOptions { PickerTitle = "Select Test Folder" });
            if (folderPickResult != null)
            {
                _folderPickResult = folderPickResult;
            }
        }
    }
}

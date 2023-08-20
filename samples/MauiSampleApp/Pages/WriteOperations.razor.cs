using CommonDialogs.Maui;

using Microsoft.AspNetCore.Components;

namespace MauiSampleApp.Pages
{
    public partial class WriteOperations : ComponentBase
    {
        private const string TestFileName = "test.txt";

        private string? _errorText1;

        private string? _errorText2;

        private string? _errorText3;

        private string? _fileContent1;

        private string? _fileContent2;

        private FileSystemPickResult? _folderCreateTest;

        private FileSystemPickResult? _folderPickResult;

        private string? _resultText1;

        private string? _resultText2;

        private string? _resultText3;

        private void CheckPreconditions()
        {
            if (_folderPickResult == null)
            {
                _errorText1 = "Select folder first";
                //StateHasChanged();
            }
        }

        private void OnCreateFile()
        {
            try
            {
                _errorText1 = string.Empty;
                _resultText1 = String.Empty;

                CheckPreconditions();
                
                if (_folderPickResult != null)
                {
                    var res = CommonOperations.CreateFile(_folderPickResult.PlatformPath, TestFileName);
                    using (var stream = CommonOperations.OpenFile(res.PlatformPath, FileOperations.Write))
                    {
                        using var sw = new StreamWriter(stream);
                        sw.Write("Some text");
                        _resultText1 = "File created";
                    }

                    using (var streamRead = CommonOperations.OpenFile(res.PlatformPath, FileOperations.Read))
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
                _resultText1 = "Error";
            }
        }

        private void OnCreateFileAndFolders()
        {
            try
            {
                _errorText2 = string.Empty;
                _resultText2 = String.Empty;

                CheckPreconditions();
               
                if (_folderPickResult != null)
                {
                    string newPath = Path.Combine("SubfolerTest01", "SubfolderTest02", TestFileName);
                    var res = CommonOperations.CreateFile(_folderPickResult.PlatformPath, newPath);
                    using (var stream = CommonOperations.OpenFile(res.PlatformPath, FileOperations.Write))
                    {
                        using (var sw = new StreamWriter(stream))
                        {
                            sw.Write("Some text");
                            _resultText2 = "File created";
                        }
                    }

                    using var streamRead = CommonOperations.OpenFile(res.PlatformPath, FileOperations.Read);

                    using var textReader = new StreamReader(streamRead);
                    _fileContent2 = textReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _errorText2 = ex.Message;
                _resultText2 = "Error";
            }
        }

        private void OnCreateFolder()
        {
            try
            {
                _errorText3 = string.Empty;
                _resultText3 = String.Empty;
                CheckPreconditions();
               
                if (_folderPickResult != null)
                {
                    string newPath = Path.Combine("SubfolerTest02", "SubfolderTest");
                    _folderCreateTest = CommonOperations.CreateFolder(_folderPickResult.PlatformPath, newPath);
                    _resultText3 = _folderCreateTest != null ? "Folder created" : "Error";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _errorText3 = ex.Message;
                _resultText3 = "Error";
            }
        }

        private async Task OnSelectFolder()
        {
            var folderPickResult = await CommonOperations.PickFolderAsync(new FilePickOptions { PickerTitle = "Select Test Folder" });
            if (folderPickResult != null)
            {
                _folderPickResult = folderPickResult;
                _errorText1 = String.Empty;
            }
        }
    }
}

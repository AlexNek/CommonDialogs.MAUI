using Android.Content;

using AndroidX.DocumentFile.Provider;

using CommonDialogs.Maui;

namespace CommonDialogs.Maui
{
    internal partial class FileSystemOperations
    {
        internal FilePickResult? CreateFilePlatform(string platformFolderPath, string childPath)
        {
            var paths = childPath.Split(new[] { '/', Path.PathSeparator }, StringSplitOptions.RemoveEmptyEntries);
            var folderUri = Android.Net.Uri.Parse(platformFolderPath);
            if (folderUri != null && paths.Length > 0)
            {
                DocumentFile? file = DocumentFile.FromTreeUri(Context, folderUri);
                if (file != null)
                {
                    if (paths.Length > 1)
                    {
                        file = CreateStructureFoldersInDocument(paths.Take(paths.Length - 1), file);
                        return CreateFileInDocument(paths[paths.Length - 1], file);
                    }

                    if (paths.Length == 1)
                    {
                        return CreateFileInDocument(paths[0], file);
                    }
                }
            }

            return null;
        }

        internal FilePickResult? CreateFolderPlatform(string platformFolderPath, string childPath)
        {
            var paths = childPath.Split(new[] { '/', Path.PathSeparator }, StringSplitOptions.RemoveEmptyEntries);
            var folderUri = Android.Net.Uri.Parse(platformFolderPath);
            if (folderUri != null && paths.Length > 0)
            {
                DocumentFile? file;
                file = DocumentFile.FromTreeUri(Context, folderUri);
                if (file != null)
                {
                    if (paths.Length > 1)
                    {
                        file = CreateStructureFoldersInDocument(paths.Take(paths.Length - 1), file);
                        return CreateFolderInDocument(paths[paths.Length - 1], file);
                    }

                    if (paths.Length == 1)
                    {
                        return CreateFolderInDocument(paths[0], file);
                    }
                }
            }

            return null;
        }

        internal Stream? OpenPickedFilePlatform(string platformPath, string fileOpenMode)
        {
            if (platformPath.StartsWith("content"))
            {
                if (fileOpenMode == FileOperations.Read)
                {
                    return Context.ContentResolver?.OpenInputStream(Android.Net.Uri.Parse(platformPath)!);
                }

                if (fileOpenMode == FileOperations.Write)
                {
                    return Context.ContentResolver?.OpenOutputStream(Android.Net.Uri.Parse(platformPath)!, "w");
                }

                if (fileOpenMode == FileOperations.ReadWrite)
                {
                    var descriptor = Context.ContentResolver?.OpenFileDescriptor(Android.Net.Uri.Parse(platformPath)!, "rw");
                    return new JavaStreamWrapper(descriptor!);
                }
                else
                {
                    var descriptor = Context.ContentResolver?.OpenFileDescriptor(Android.Net.Uri.Parse(platformPath)!, "rw");
                    return new JavaStreamWrapper(descriptor!);
                }
            }

            try
            {
                if (fileOpenMode == FileOperations.Read)
                {
                    return File.OpenRead(platformPath);
                }

                if (fileOpenMode == FileOperations.Write)
                {
                    return File.OpenWrite(platformPath);
                }

                return File.Open(platformPath, FileMode.OpenOrCreate);
            }
            catch
            {
            }

            try
            {
                var status = Permissions.CheckStatusAsync<Permissions.StorageRead>().Result;
                if (status != PermissionStatus.Granted)
                {
                    Permissions.RequestAsync<Permissions.StorageRead>().Wait();
                    Permissions.RequestAsync<Permissions.StorageWrite>().Wait();
                }

                var file = new Java.IO.File(platformPath);
                if (file.Exists())
                {
                    return new JavaStreamWrapper(file);
                }
            }
            catch
            {
            }

            return null;
        }

        internal Task<FilePickResult?> PickFilePlatformAsync(FilePickOptions? pickOptions)
        {
            PickFileActivity.PickFileTaskCompletionSource = new TaskCompletionSource<FilePickResult?>();
            PickFileActivity.HoldPermisson = pickOptions?.HoldPermission ?? true;
            PickFileActivity.DisplayTitle = pickOptions?.PickerTitle;
            if (pickOptions != null)
            {
                var types = pickOptions?.FileTypes?.Value;
                if (types != null && types.Any())
                {
                    PickFileActivity.MimeType = types.First();
                    PickFileActivity.ExtraMimeTypes = types.ToArray();
                }
                else
                {
                    PickFileActivity.MimeType = "*/*";
                    PickFileActivity.ExtraMimeTypes = null;
                }
            }
            else
            {
                PickFileActivity.MimeType = "*/*";
                PickFileActivity.ExtraMimeTypes = null;
            }

            var intent = new Intent(Context, typeof(PickFileActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            Context.StartActivity(intent);
            return PickFileActivity.PickFileTaskCompletionSource.Task;
        }

        internal Task<IEnumerable<FilePickResult>> PickFilesPlatformAsync(FilePickOptions? pickOptions)
        {
            PickFileActivity.PickFilesTaskCompletionSource = new TaskCompletionSource<IEnumerable<FilePickResult>>();
            PickFileActivity.HoldPermisson = pickOptions?.HoldPermission ?? true;
            PickFileActivity.DisplayTitle = pickOptions?.PickerTitle;
            if (pickOptions != null)
            {
                PickFileActivity.MimeType = string.Join(";", pickOptions?.FileTypes?.Value ?? new[] { "*/*" });
            }
            else
            {
                PickFileActivity.MimeType = "*/*";
            }

            var intent = new Intent(Context, typeof(PickFileActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            Context.StartActivity(intent);

            return PickFileActivity.PickFilesTaskCompletionSource.Task;
        }

        internal Task<FilePickResult?> PickFolderPlatformAsync(FilePickOptions? pickOptions)
        {
            PickFileActivity.PickFolderTaskCompletionSource = new TaskCompletionSource<FilePickResult?>();
            PickFileActivity.HoldPermisson = pickOptions?.HoldPermission ?? true;
            PickFileActivity.DisplayTitle = pickOptions?.PickerTitle ?? "选择文件夹";
            var intent = new Intent(Context, typeof(PickFileActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            Context.StartActivity(intent);
            return PickFileActivity.PickFolderTaskCompletionSource.Task;
        }

        private FilePickResult? CreateFileInDocument(string fileName, DocumentFile? file)
        {
            var target = file?.FindFile(fileName);
            if (target == null || target.IsDirectory)
            {
                file = file?.CreateFile("*/*", fileName)!;
            }
            else
            {
                file = target;
            }

            if (file != null)
            {
                //Context?.ContentResolver?.TakePersistableUriPermission(file.Uri, ActivityFlags.GrantReadUriPermission | ActivityFlags.GrantWriteUriPermission );
                return new FilePickResult(
                    file.Name,
                    PickFileActivity.GetAbsoluteFolderPath(file.Uri),
                    file.Uri.ToString());
            }

            return null;
        }

        private FilePickResult? CreateFolderInDocument(string folderName, DocumentFile? file)
        {
            var target = file?.FindFile(folderName);
            if (target == null || target.IsFile)
            {
                file = file?.CreateDirectory(folderName);
            }
            else
            {
                file = target;
            }

            if (file != null)
            {
                return new FilePickResult(
                    file.Name,
                    PickFileActivity.GetAbsoluteFolderPath(file.Uri),
                    file.Uri.ToString());
            }

            return null;
        }

        private DocumentFile? CreateStructureFoldersInDocument(IEnumerable<string> folders, DocumentFile? file)
        {
            foreach (var folder in folders)
            {
                var target = file?.FindFile(folder);
                if (target == null || target.IsFile)
                {
                    file = file?.CreateDirectory(folder);
                }
                else
                {
                    file = target;
                }
            }

            return file;
        }

        private Context Context => Android.App.Application.Context;
    }
}

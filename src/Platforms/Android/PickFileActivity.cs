using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

using AndroidX.DocumentFile.Provider;

namespace CommonDialogs.Maui
{
    [Activity(
        MainLauncher = false,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout
                               | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    internal class PickFileActivity : Activity
    {
        public const int PickFileId = 1;

        public const int PickFilesId = 2;

        public const int PickFolderId = 3;

        private static TaskCompletionSource<IEnumerable<FilePickResult>>? _PickFilesTaskCompletionSource;

        private static TaskCompletionSource<FilePickResult?>? _PickFileTaskCompletionSource;

        private static TaskCompletionSource<FilePickResult?>? _PickFolderTaskCompletionSource;

        public static string? GetAbsoluteFolderPath(Android.Net.Uri uri)
        {
            var path = uri?.Path?.Split(':').Last();
            if (path != null)
            {
                return Path.Combine(Android.OS.Environment.ExternalStorageDirectory?.AbsolutePath ?? string.Empty, path);
            }

            return uri?.Path;
        }

        public static string? GetAbsolutePath(Android.Net.Uri uri)
        {
            using var cusor = Android.App.Application.Context.ContentResolver?.Query(
                uri,
                new[] { "_data" },
                null,
                null,
                null);
            if (cusor != null && cusor.MoveToNext())
            {
                var dataCol = cusor.GetColumnIndex("_data");
                return cusor.GetString(dataCol);
            }

            return uri.Path;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent? data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == PickFileId)
            {
                try
                {
                    if (resultCode == Result.Ok && data != null)
                    {
                        var uri = data.Data;
                        var takeFlags = ActivityFlags.GrantReadUriPermission | ActivityFlags.GrantWriteUriPermission;
                        if (uri != null)
                        {
                            if (HoldPermisson)
                            {
                                ContentResolver?.TakePersistableUriPermission(uri, takeFlags);
                            }

                            PickFileTaskCompletionSource?.TrySetResult(ReadFile(uri));
                        }
                    }
                }
                catch
                {
                }

                PickFileTaskCompletionSource?.TrySetResult(null);
            }
            else if (requestCode == PickFilesId)
            {
                try
                {
                    if (resultCode == Result.Ok && data?.ClipData != null)
                    {
                        var results = new List<FilePickResult>();
                        for (int i = 0; i < data.ClipData.ItemCount; i++)
                        {
                            var uri = data.ClipData.GetItemAt(i);
                            var takeFlags = ActivityFlags.GrantReadUriPermission | ActivityFlags.GrantWriteUriPermission;
                            if (uri?.Uri != null)
                            {
                                if (HoldPermisson)
                                {
                                    ContentResolver?.TakePersistableUriPermission(uri.Uri, takeFlags);
                                }

                                try
                                {
                                    results.Add(ReadFile(uri.Uri));
                                }
                                catch
                                {
                                }
                            }
                        }

                        PickFilesTaskCompletionSource?.TrySetResult(results);
                    }
                }
                catch
                {
                }

                PickFilesTaskCompletionSource?.TrySetResult(new List<FilePickResult>());
            }
            else if (requestCode == PickFolderId)
            {
                if (resultCode == Result.Ok && data != null)
                {
                    Android.Net.Uri? uri = data.Data;
                    var takeFlags = ActivityFlags.GrantReadUriPermission | ActivityFlags.GrantWriteUriPermission;
                    if (uri != null)
                    {
                        if (HoldPermisson)
                        {
                            ContentResolver?.TakePersistableUriPermission(uri, takeFlags);
                        }

                        var folderName = uri.Path?.Split(":").Last();
                        PickFolderTaskCompletionSource?.TrySetResult(
                            new FilePickResult(folderName, GetAbsoluteFolderPath(uri), uri.ToString()));
                    }
                }
                else
                {
                    PickFolderTaskCompletionSource?.TrySetResult(null);
                }
            }

            Finish();
        }

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Intent intent;
            MimeType ??= "*/*";
            switch (ActionId)
            {
                case PickFileId:
                    intent = new Intent(Intent.ActionOpenDocument);
                    DisplayTitle ??= "Select the file";
                    SetMimeType(intent);
                    intent.PutExtra(Intent.ExtraTitle, DisplayTitle);
                    StartActivityForResult(intent, PickFileId);
                    break;
                case PickFilesId:
                    intent = new Intent(Intent.ActionOpenDocument);
                    SetMimeType(intent);
                    DisplayTitle ??= "Select multiple files";
                    intent.PutExtra(Intent.ExtraTitle, DisplayTitle);
                    intent.PutExtra(Intent.ExtraAllowMultiple, true);
                    StartActivityForResult(intent, PickFilesId);
                    break;
                case PickFolderId:
                    intent = new Intent(Intent.ActionOpenDocumentTree);
                    DisplayTitle ??= "Select a folder";
                    intent.PutExtra(Intent.ExtraTitle, DisplayTitle);
                    StartActivityForResult(intent, PickFolderId);
                    break;
            }

            SetContentView(new Android.Widget.LinearLayout(this));
        }

        private FilePickResult ReadFile(Android.Net.Uri uri)
        {
            var documentFile = DocumentFile.FromSingleUri(this, uri);
            return new FilePickResult(documentFile?.Name, GetAbsoluteFolderPath(uri), uri.ToString());
        }

        private static void SetMimeType(Intent intent)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                intent.SetType(MimeType);
                if (ExtraMimeTypes != null && ExtraMimeTypes.Length > 1)
                {
                    intent.SetType("*/*");
                    intent.PutExtra(Intent.ExtraMimeTypes, ExtraMimeTypes);
                }
            }
            else
            {
                intent.SetType(MimeType);
                if (ExtraMimeTypes != null && ExtraMimeTypes.Length > 1)
                {
                    intent.SetType(string.Join("|", ExtraMimeTypes));
                }
            }
        }

        public static string? DisplayTitle { get; set; }

        public static string[]? ExtraMimeTypes { get; set; }

        public static bool HoldPermisson { get; set; }

        public static string? MimeType { get; set; }

        public static TaskCompletionSource<IEnumerable<FilePickResult>>? PickFilesTaskCompletionSource
        {
            get
            {
                return _PickFilesTaskCompletionSource;
            }
            set
            {
                _PickFilesTaskCompletionSource = value;
                ActionId = PickFilesId;
            }
        }

        public static TaskCompletionSource<FilePickResult?>? PickFileTaskCompletionSource
        {
            get => _PickFileTaskCompletionSource;
            set
            {
                _PickFileTaskCompletionSource = value;
                ActionId = PickFileId;
            }
        }

        public static TaskCompletionSource<FilePickResult?>? PickFolderTaskCompletionSource
        {
            get => _PickFolderTaskCompletionSource;
            set
            {
                _PickFolderTaskCompletionSource = value;
                ActionId = PickFolderId;
            }
        }

        private static int ActionId { get; set; }
    }
}

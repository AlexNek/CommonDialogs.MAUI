# CommonDialogs.MAUI
## Overview
I cannot find possibility to select file or folder for MAUI application. 
I can only found [library](https://github.com/mingkly/MKFilePicker) which support file and folder selection for windows and android. So I based this package on that library.
> **Note**: At this time supported file and folder operations under windows and android only. I don't have any apple devices so I cannot test possible additions now

## Supported operations
- Create file
- Select single file
- Select many files
- Open file
- Create folder
- Select folder

## Android permissions
Don't forget to change android manifest nd set permissions
- READ_EXTERNAL_STORAGE
- WRITE_EXTERNAL_STORAGE

for android api bigger as 29 you need to add
- MANAGE_EXTERNAL_STORAGE
> **Note 1**: After file selection absolute path under android could be not correct so it is better to use platform path instead
> **Note 2**: Write to external storage from api 30 (Andorid 11) or for some android devices is not possible in easy way.

## Samples

1. Select a video file and read it:
```csharp
FilePickResult res = await Picker.PickFileAsync(FilePickOptions.Videos);
using var stream = Picker.OpenPickedFile(res.PlatformPath, "r");
...
```

2. Select many video files:
```csharp
var results = await Picker.PickFilesAsync(FilePickOptions.Videos);
```
3. Select special types of file:
```csharp
var fileOptions = new FilePickOptions()
{
    FileTypes = new FilePickerFileType(new Dictionary>
    {
          {DevicePlatform.Android, new string[]{"image/*"} },
          {DevicePlatform.WinUI,   new string[]{"*.png", "*.jpg", "*.jpeg", "*.webp","*.gif","*.bmp"} }
    }),
};
FilePickResult res = await Picker.PickFileAsync(fileOptions);
```
4. Select a folder and create file under it:
```csharp
var folder = await Picker.PickFolderAsync(null);
var res = Picker.CreateFile(folder.PlatformPath, "test.txt");
using var stream = Picker.OpenPickedFile(res.PlatformPath, "w");
using var sw = new StreamWriter(stream );
sw.Write("Some text");
```
5. Select a folder and create subfolder with file:
```csharp
var res3 = Picker.CreateFolder(folder.PlatformPath, "testFolder");
var res4 = Picker.CreateFile(res3.PlatformPath, "TestInnerFolder/test.txt");
using var stream = Picker.OpenPickedFile(res4.PlatformPath, "w");
using var sw = new StreamWriter(stream);
sw.Write("Some text");
```
In repository you can find sample project too

## Android troubleshooting
If you have problem for writing to SD card try this solution:
Open system settings and go to Apps. Find application named Files (on some devices the app may have name Documents or other name). You may need to enable to show system apps from menu. You may also filter to show only disabled apps, then you may find it easier. Then enable the app, force-close X-plore and retry.

This is reported to fix the problem, if Documents (Files) app exists on device. If the app is not included, there may be no fix.
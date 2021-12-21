using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ElectronNET.API;
using ElectronNET.API.Entities;
using ElectronNET.API.Interfaces;
using Microsoft.JSInterop;

namespace Electronize.Data;

public class ElectronFileSavingService : IFileSavingService
{
    private IWindowManager _windowManager;
    private IDialog _dialog;
    private INotification _notification;
    public ElectronFileSavingService(IWindowManager windowManager, IDialog dialog, INotification notification)
    {
        _windowManager = windowManager;
        _dialog = dialog;
        _notification = notification;
    }
    public async Task SaveDataAsync(string data, IJSRuntime jsRuntime)
    {
        //get the first browser window, presumably the current one
        var mainWindow = _windowManager.BrowserWindows.First();
        //set save dialog options
        var options = new SaveDialogOptions
        {
            Title = "Save file...",
            Filters = new FileFilter[]
            {
                new() { Name = "Text file", Extensions = new string[] { "txt" } }
            },
            DefaultPath = "test.txt"
        };
        
        var filePath = await _dialog.ShowSaveDialogAsync(mainWindow, options);
        if (filePath == "")
            return;
        await File.WriteAllLinesAsync(filePath, new string[]{data}, CancellationToken.None);
        _notification.Show(new NotificationOptions("File saved", $"Saved file at {filePath}"));
    }
}
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;

namespace Electronize.Data;

public class ElectronFileSavingService : IFileSavingService
{
    public async Task SaveData(string data, IJSRuntime jsRuntime)
    {
        //get the first browser window, presumably the current one
        var mainWindow = Electron.WindowManager.BrowserWindows.First();
        //set save dialog options
        var options = new SaveDialogOptions
        {
            Title = "Save file...",
            Filters = new FileFilter[]
            {
                new() { Name = "Text file", Extensions = new string[] { "txt" } }
            },
            DefaultPath = "test"
        };
        
        var filePath = await Electron.Dialog.ShowSaveDialogAsync(mainWindow, options);
        await File.WriteAllLinesAsync(filePath, new string[]{data}, CancellationToken.None);
        Electron.Notification.Show(new NotificationOptions("File saved", $"Saved file at {filePath}"));
        
    }
}
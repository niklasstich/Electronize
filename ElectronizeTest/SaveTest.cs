using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Electronize.Data;
using ElectronNET.API;
using ElectronNET.API.Entities;
using ElectronNET.API.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace ElectronizeTest;

[TestFixture]
public class SaveTest
{
    [OneTimeSetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task ElectronFileSavingServiceCallsElectron()
    {
        var windowManager = Substitute.For<IWindowManager>();
        var window = Substitute.For<IBrowserWindow>();
        windowManager.BrowserWindows.Returns(new []{window});
        
        var dialog = Substitute.For<IDialog>();
        var path = Directory.GetCurrentDirectory() + "test.txt";
        dialog.ShowSaveDialogAsync(window, Arg.Any<SaveDialogOptions>()).Returns(path);
        
        var notification = Substitute.For<INotification>();
        
        var fileSavingService = new ElectronFileSavingService(windowManager, dialog, notification);
        await fileSavingService.SaveDataAsync("testdata123", null);
        
        Assert.IsTrue(File.Exists(path));
        Assert.AreEqual("testdata123\n", await File.ReadAllTextAsync(path));

        dialog.Received().ShowSaveDialogAsync(window, Arg.Any<SaveDialogOptions>());
        notification.Received().Show(Arg.Any<NotificationOptions>());
    }
}
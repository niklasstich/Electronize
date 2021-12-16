using System;
using System.IO;
using System.Threading.Tasks;
using Bunit;
using Electronize;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Electronize.Data;
using Electronize.Pages;
using ElectronNET.API;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ElectronizeTest;

[TestFixture]
public class SaveTest
{
    [OneTimeSetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task TestSavePageWithMockService()
    {
        using var ctx = new Bunit.TestContext();
        ctx.Services.AddSingleton<IFileSavingService>(new MockFileSavingService());
        
        var page = ctx.RenderComponent<Save>();
        var input = page.Find("input");
        const string target = "written from inside the test";
        await input.ChangeAsync(new ChangeEventArgs
        {
            Value = target
        });
        await page.Find("button").ClickAsync(new MouseEventArgs());
        page.Find("p").MarkupMatches("<p>written from inside the test</p>");
        var path = MockFileSavingService.Path;
        Assert.IsTrue(File.Exists(path));
        var fileContent = await File.ReadAllTextAsync(path);
        Assert.AreEqual(target, fileContent);
    }
}
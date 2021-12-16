using System.IO;
using System.Threading.Tasks;
using Electronize.Data;
using Microsoft.JSInterop;

namespace ElectronizeTest;

public class MockFileSavingService : IFileSavingService
{
    public static string Path => Directory.GetCurrentDirectory() + "test.txt";

    public Task SaveData(string data, IJSRuntime jsRuntime)
    {
        File.WriteAllText(Path, data);
        return Task.CompletedTask;
    }
}
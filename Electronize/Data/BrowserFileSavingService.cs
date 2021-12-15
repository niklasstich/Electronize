using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Electronize.Data;

public class BrowserFileSavingService : IFileSavingService
{
    public async Task SaveData(string data, IJSRuntime jsRuntime)
    {
        var fileStream = new MemoryStream(Encoding.ASCII.GetBytes(data));
        var fileName = "download.txt";

        using var streamRef = new DotNetStreamReference(stream: fileStream);
        await jsRuntime.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
    }
}
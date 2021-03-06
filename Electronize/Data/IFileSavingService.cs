using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Electronize.Data;

public interface IFileSavingService
{
    public Task SaveDataAsync(string data, IJSRuntime jsRuntime);
}
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Electronize.Data;

public interface IFileSavingService
{
    public Task SaveData(string data, IJSRuntime jsRuntime);
}
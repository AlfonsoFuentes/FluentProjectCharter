using Microsoft.JSInterop;
using System.Text.Json;

namespace FluentWeb.Services
{
    public class ModelLocalStorage : IModelLocalStorage
    {
        IJSRuntime _jsRuntime;
        public ModelLocalStorage(IJSRuntime _jsRuntime)
        {
            this._jsRuntime = _jsRuntime;
        }

        public async Task<T?> LoadFromLocalStorage<T>(T model)
        {
            var jsonModel = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", model!.GetType().Name);
            if (!string.IsNullOrEmpty(jsonModel))
            {
                await RemoveFromLocalStorage(model!.GetType().Name);
                return JsonSerializer.Deserialize<T>(jsonModel);
            }
            return default;
        }

        public async Task SaveToLocalStorage<T>(T model)
        {
            string key = model!.GetType().Name;
            var jsonModel = JsonSerializer.Serialize(model);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, jsonModel);
        }
        public async Task RemoveFromLocalStorage(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}

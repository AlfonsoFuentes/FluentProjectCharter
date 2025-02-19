
namespace FluentWeb.Services
{
    public interface IModelLocalStorage
    {
        Task<T?> LoadFromLocalStorage<T>(T model);
        Task<T?> LoadFromLocalStorage<T>(string type);
        Task RemoveFromLocalStorage(string key);
        Task SaveToLocalStorage<T>(T model);
    }
}
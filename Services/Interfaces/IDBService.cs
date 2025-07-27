using DemoApp.Models;

namespace DemoApp.Services.Interfaces;

public interface IDBService
{
    Task<int> Create(DemoModel model);
    Task<List<DemoModel>> Read(int limit, int offset);
    Task<int> Delete(DemoModel model);
    Task ResetDB();
}
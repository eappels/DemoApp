using DemoApp.Helpers;
using DemoApp.Models;
using DemoApp.Services.Interfaces;
using SQLite;
using System.Diagnostics;

namespace DemoApp.Services;

public class DBService : IDBService
{

    private SQLiteAsyncConnection database;

    async Task Init()
    {
        if (database is not null)
            return;

        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await database.CreateTableAsync<DemoModel>();
        Debug.WriteLine($"Database location: {Constants.DatabasePath}");
    }

    public async Task<int> Create(DemoModel track)
    {
        await Init();
        return await database.InsertAsync(track);
    }

    public async Task<List<DemoModel>> Read(int limit, int offset)
    {
        await Init();
        return await database.Table<DemoModel>()
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<int> Delete(DemoModel track)
    {
        await Init();
        return await database.DeleteAsync(track);
    }

    public async Task ResetDB()
    {
        await Init();
        await database.DropTableAsync<DemoModel>();
        await database.CreateTableAsync<DemoModel>();
    }
}
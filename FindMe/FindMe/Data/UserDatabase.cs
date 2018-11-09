using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FindMe.Data
{
    public class UserDatabase
    {
        private readonly SQLiteAsyncConnection database;

        public UserDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<User>().Wait();
        }

        public Task<List<User>> GetItemsAsync()
        {
            return database.Table<User>().ToListAsync();
        }
        public Task<List<User>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<User>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }
        public Task<User> GetItemAsync(int id)
        {
            return database.Table<User>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveItemAsync(User item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        public Task<int> DeleteItemAsync(User item)
        {
            return database.DeleteAsync(item);
        }
        public Task<int> TruncateDbAsync()
        {
            return database.Table<User>().DeleteAsync(a => a.Id > 0);
        }
    }

}

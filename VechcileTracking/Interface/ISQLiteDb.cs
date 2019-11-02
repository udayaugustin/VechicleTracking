using SQLite;

namespace VechcileTracking
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}


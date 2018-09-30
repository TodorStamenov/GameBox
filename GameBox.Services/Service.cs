using GameBox.Data;

namespace GameBox.Services
{
    public abstract class Service
    {
        protected Service(GameBoxDbContext database)
        {
            this.Database = database;
        }

        protected GameBoxDbContext Database { get; }
    }
}
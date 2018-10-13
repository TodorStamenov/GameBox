using GameBox.Core;
using GameBox.Core.Enums;
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

        protected ServiceResult GetServiceResult(string message, ServiceResultType resultType)
        {
            return new ServiceResult
            {
                Message = message,
                ResultType = resultType
            };
        }

        protected ServiceResult GetServiceResult(string username, string token, bool isAdmin, string message, ServiceResultType resultType)
        {
            return new ServiceAuthResult
            {
                Username = username,
                Token = token,
                IsAdmin = isAdmin,
                Message = message,
                ResultType = resultType
            };
        }
    }
}
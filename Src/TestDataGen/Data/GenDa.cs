using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace TestDataGen.Data
{
    class GenDa
    {
        private readonly MyConnectionFactory connectionFactory;
        public GenDa(MyConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public int GetMaxId()
        {
            var sqlText = "Select max(Id) Max from ASource";
            using (var connection = connectionFactory.Create())
            {
                var r=connection.Query<int>(sqlText, null);
                return r.FirstOrDefault();
            }
        }

        public async Task Insert(ASourceRecord r)
        {
            var sqlText = @"INSERT INTO ASource(Id,Title,Description) VALUES(@Id,@Title,@Description)";
            using (var connection = connectionFactory.Create())
            {
                await connection.ExecuteAsync(sqlText,new {r.Id,r.Title,r.Description});
            }
        }
    }
}

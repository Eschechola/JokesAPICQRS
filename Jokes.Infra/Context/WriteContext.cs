using Jokes.Infra.Interfaces;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Jokes.Infra.Context
{
    public class WriteContext : IContext
    {
        private readonly IConfiguration _configuration;

        public WriteContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration["ConnectionStrings:WriteDatabase"]);
        }
    }
}

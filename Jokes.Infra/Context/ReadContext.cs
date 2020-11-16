using Jokes.Infra.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Jokes.Infra.Context
{
    public class ReadContext : IContext
    {
        private readonly IConfiguration _configuration;

        public ReadContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration["ConnectionStrings:ReadDatabase"]);
        }
    }
}

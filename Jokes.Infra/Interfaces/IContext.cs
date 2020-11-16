using System.Data.SqlClient;

namespace Jokes.Infra.Interfaces
{
    public interface IContext
    {
        SqlConnection GetConnection();
    }
}

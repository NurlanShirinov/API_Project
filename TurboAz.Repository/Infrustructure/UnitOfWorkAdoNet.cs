using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboAz.Repository.Infrustructure
{

    public interface IUnitOfWorkAdoNet
    {
        SqlConnection GetConnection();
        SqlConnection OpenConnection();
        void CloseConnection(SqlConnection connection);
    }

    public class UnitOfWorkAdoNet:IUnitOfWorkAdoNet
    {
        private readonly SqlConnection _connection = null;

        public UnitOfWorkAdoNet( IConfiguration configuration)
        {
            _connection = new SqlConnection( configuration.GetConnectionString("DefaultConnection"));
        }

        public void CloseConnection(SqlConnection connection)
        {
            if (connection.State== System.Data.ConnectionState.Open && connection is not null)
            {
                connection.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }

        public SqlConnection OpenConnection()
        {
            _connection.Open();
            return _connection;
        }

    }
}

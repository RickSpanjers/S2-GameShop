using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RicksWebWorld.Context
{
    public abstract class ConnectionHelper
    {
		public SqlConnection ReturnSQLConnection()
		{
			SqlConnection cnn = new SqlConnection(Startup.Connectionstring);
			
			if(cnn.State != ConnectionState.Open)
			{
				cnn.Open();
			}

			return cnn;
		}

		public SqlCommand CreateSQLCommandText(string query,SqlConnection connection)
		{
			SqlConnection cnn = connection;
			var cmd = cnn.CreateCommand();
			cmd.Connection = cnn;
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = query;
			return cmd;
		}

		public SqlCommand CreateSQLCommandStoredProcedure(string procedure, SqlConnection connection)
		{
			SqlConnection cnn = connection;
			SqlCommand cmd = new SqlCommand(procedure, cnn);
			cmd.CommandType = CommandType.StoredProcedure;
			return cmd;
		}
	}
}

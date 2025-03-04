using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Data.SqlClient;
using System.Data;
using CraftMan_WebApi.Models;
namespace CraftMan_WebApi.DataAccessLayer
{
    public class DBAccess
    {
        private String strConnection;
        private SqlConnection strConnectionString;
        private SqlCommand sqlCmd;
        private SqlTransaction sqlTransaction;

        public DBAccess()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            strConnection = configuration.GetConnectionString("dbconn");


            strConnectionString = new SqlConnection(strConnection);
            sqlCmd = new SqlCommand();
            sqlCmd.Connection = strConnectionString;
            sqlCmd.CommandType = CommandType.Text;
        }

        public SqlDataReader ReadDB(string SQLstr)
        {
            if (strConnectionString.State == ConnectionState.Closed)
                strConnectionString.Open();
            sqlCmd = strConnectionString.CreateCommand();
            sqlCmd.CommandTimeout = 6000;
            sqlCmd.CommandText = SQLstr;
            // ''---------------------
            if (!(this.sqlTransaction == null))
               
                this.sqlCmd.Transaction = this.sqlTransaction;
            // ''---------------------
            return sqlCmd.ExecuteReader();
        }
        public Response validate(string qstr)
        {
            Response strReturn = new Response();
            SqlConnection con = new SqlConnection(strConnection);
            SqlDataAdapter sda = new SqlDataAdapter(qstr, con);// "select JobType from [dbo].[tblJobTypeMaster]where upper(JobType)=upper('" + _Job.JobType + "') ", con);

            DataTable da = new DataTable();
            sda.Fill(da);

            if (da.Rows.Count > 0)
            {
                strReturn.StatusMessage = "Already exists...";
                strReturn.StatusCode = 1;

            }

            return strReturn;

        }


        public int ExecuteNonQuery(string qstr)
        {

            if (strConnectionString.State == ConnectionState.Closed)
            {
                strConnectionString.Open();
            }
            sqlCmd = strConnectionString.CreateCommand();
            sqlCmd = new SqlCommand(qstr, strConnectionString);
            sqlCmd.CommandType = CommandType.Text;
            return sqlCmd.ExecuteNonQuery();
        }


        public int ExecuteScalar(string qstr)
        {
            int insertedId = 0;

            if (strConnectionString.State == ConnectionState.Closed)
            {
                strConnectionString.Open();
            }

            qstr += "; SELECT SCOPE_IDENTITY();";


            sqlCmd = strConnectionString.CreateCommand();
            sqlCmd = new SqlCommand(qstr, strConnectionString);
            sqlCmd.CommandType = CommandType.Text;

            object result = sqlCmd.ExecuteScalar();

            if (result != null)
            {
                insertedId = Convert.ToInt32(result);
            }

            return insertedId;
        }
    }
}

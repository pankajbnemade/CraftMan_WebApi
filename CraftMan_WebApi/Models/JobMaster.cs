using CraftMan_WebApi.DataAccessLayer;
using Microsoft.Data.SqlClient;
using System.Collections;

namespace CraftMan_WebApi.Models
{
    public class JobMaster
    {
        public int JobTypeId { get; set; }
        public string JobType { get; set; }
        public string JobTypeDescription { get; set; }
        public Response validatejob(JobMaster _Job)
        {
            string qstr = "select JobType from [dbo].[tblJobTypeMaster]where upper(JobType)=upper('" + _Job.JobType + "') ";
            DBAccess db = new DBAccess();
           return db.validate(qstr);

        }
        public static int InsertJob(JobMaster _Job)
        {
            string qstr = " INSERT into [dbo].[tblJobTypeMaster](JobType,JobTypeDescription)     VALUES('" + _Job.JobType + "','" + _Job.JobTypeDescription + "')";
            DBAccess db = new DBAccess();
            return db.ExecuteNonQuery(qstr);

        }
        public static ArrayList JobList()
        {
            ArrayList jobTypes = new ArrayList();
            DBAccess db = new DBAccess();

            string query = "SELECT JobTypeId, JobType, JobTypeDescription FROM [Craftman].[dbo].[tblJobTypeMaster]";
            SqlDataReader reader = db.ReadDB(query);
            
                while (reader.Read())
                {
                    var jobType = new
                    {
                        JobTypeId = reader.GetInt32(0),
                        JobType = reader.GetString(1),
                        JobTypeDescription = reader.GetString(2)
                    };
                    jobTypes.Add(jobType);
                }

            reader.Close();
            reader.Dispose();

            return jobTypes;
        }
    }
}

using DATA.Models;
using System.Data.SqlClient;
using System.Data;

namespace DATA.DAL
{
    public class DataDAL
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration Configuration { get; set; }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");
        }
        public List<Data> GetAll()
        {
            List<Data> datalist = new List<Data>();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "DATAVIEW_SP";
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();
                while(dr.Read())
                {
                    Data data = new Data();
                    data.Id = Convert.ToInt32(dr["Id"]);
                    data.Name = dr["Name"].ToString();
                    data.Age = Convert.ToInt32(dr["Age"]);
                    datalist.Add(data);
                }
            }
            return datalist;
        }
        public bool Insert(Data data)
        {
            using(_connection=new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "DATAINSERT_SP";
                _command.Parameters.AddWithValue("@Name", data.Name);
                _command.Parameters.AddWithValue("@Age", data.Age);
                _connection.Open();
                _command.ExecuteNonQuery();
            }
            return true;
        }
        public Data GetById(int id)
        {
            Data data = new Data();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddWithValue("@id", id);
                _command.CommandText = "DATAVIEWBYID_SP";
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();
                while (dr.Read())
                {
                    data.Id = Convert.ToInt32(dr["Id"]);
                    data.Name = dr["Name"].ToString();
                    data.Age = Convert.ToInt32(dr["Age"]);
                }
            }
            return data;
        }
        public bool Update(Data data)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "DATAUPDATE_SP";
                _command.Parameters.AddWithValue("@id", data.Id);
                _command.Parameters.AddWithValue("@Name", data.Name);
                _command.Parameters.AddWithValue("@Age", data.Age);
                _connection.Open();
                id = _command.ExecuteNonQuery();
            }
            return true;
        }
        public bool Delete(Data data)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "DATAUPDATE1_SP";
                _command.Parameters.AddWithValue("@id", data.Id);
                _connection.Open();
                id = _command.ExecuteNonQuery();
            }
            return true;
        }
    }
}

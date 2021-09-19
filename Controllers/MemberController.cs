using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebApp.Models;


namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        

        public MemberController(IConfiguration configuration)
        {
            _configuration = configuration;
           
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"
                    select MemberId,MemberName, MemberSurname, MemberMail, MemberPassword from dbo.Member";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MemberConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);

        }


        [HttpPost]
        public JsonResult Post(Member mem)
        {
            string query = @"
                insert into dbo.Member values
                (@MemberName,@MemberSurname,@MemberMail,@MemberPassword)
                ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MemberConn");
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query,myCon))
                {
      
                    myCommand.Parameters.AddWithValue("@MemberName", mem.MemberName);
                    myCommand.Parameters.AddWithValue("@MemberSurname", mem.MemberSurname);
                    myCommand.Parameters.AddWithValue("@MemberMail", mem.MemberMail);
                    myCommand.Parameters.AddWithValue("@MemberPassword", mem.MemberPassword);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");

        }

        [HttpPut]
        public JsonResult Put(Member mem)
        {
            string query = @"
                           update dbo.Member
                           set MemberName= @MemberName,
                             MemberSurname = @MemberSurname,
                             MemberMail = @MemberMail,
                             MemberPassword = @MemberPassword
                            where MemberId=@MemberId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MemberConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@MemberId", mem.MemberId);
                    myCommand.Parameters.AddWithValue("@MemberName", mem.MemberName);
                    myCommand.Parameters.AddWithValue("@MemberSurname", mem.MemberSurname);
                    myCommand.Parameters.AddWithValue("@MemberMail", mem.MemberMail);
                    myCommand.Parameters.AddWithValue("@MemberPassword", mem.MemberPassword);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Member
                            where MemberId=@MemberId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MemberConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@MemberId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

    }
}

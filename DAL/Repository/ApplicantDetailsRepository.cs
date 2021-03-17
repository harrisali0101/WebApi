using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using FF.Data.Repository;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace DAL.Repository
{
    public class ApplicantDetailsRepository: IApplicantDetailsRepository
    {
        private readonly ApplicantDetailsContext _context;
        readonly string ConnectionString = "server=localhost;port=3306;user=root;password=root;database=applicants";
        public ApplicantDetailsRepository(ApplicantDetailsContext context)
        {
            this._context = context;
            
            
            
        }
        /// <summary>
        /// This method is called to get all the applicants data.
        /// </summary>
        /// <returns>List of Applicant data.</returns>
        public  IEnumerable<ApplicantDetails> GetApplicantDetails()
        {
            var applist = _context.Database.ExecuteSqlRaw("exec ApplicantViewAll _ID");
            
            using (MySqlConnection sqlCon = new MySqlConnection(ConnectionString))
            {
                sqlCon.Open();
                MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter("ApplicantViewAll", sqlCon);
                sqlDataAdapter.SelectCommand.CommandType= CommandType.StoredProcedure;
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                List<ApplicantDetails> applicantsList = new List<ApplicantDetails>();
                for(int i = 0; i < dataTable.Rows.Count; i++)
                {
                    ApplicantDetails applicantDetails = new ApplicantDetails();
                    applicantDetails.ID = Convert.ToInt32(dataTable.Rows[i]["ID"]);
                    applicantDetails.Name = dataTable.Rows[i]["Name"].ToString();
                    applicantDetails.FamilyName = dataTable.Rows[i]["FamilyName"].ToString();
                    applicantDetails.Address = dataTable.Rows[i]["Address"].ToString();
                    applicantDetails.EmailAddress = dataTable.Rows[i]["EmailAddress"].ToString();
                    applicantDetails.CountryOfOrigin = dataTable.Rows[i]["CountryOfOrigin"].ToString();
                    applicantDetails.Age = Convert.ToInt32( dataTable.Rows[i]["Age"].ToString());
                    applicantDetails.Hired = Convert.ToBoolean( dataTable.Rows[i]["Hired"]);
                    applicantsList.Add(applicantDetails);
                }
                return applicantsList.ToList();
            }

            
                //return _context.Applicant.ToList();
        }

        /// <summary>
        /// This method is called to get the data of a specific record.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="applicantDetails"></param>
        /// <returns>Return applicant data according to the id received.</returns>
        public bool TryGetApplicant(int id, out ApplicantDetails applicantDetails)
        {
            using (MySqlConnection sqlCon = new MySqlConnection(ConnectionString))
            {
                sqlCon.Open();
                MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter("ApplicantViewByID", sqlCon);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("_ID",id);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                
                    ApplicantDetails applicant = new ApplicantDetails();
                    applicant.ID = Convert.ToInt32(dataTable.Rows[0]["ID"]);
                    applicant.Name = dataTable.Rows[0]["Name"].ToString();
                    applicant.FamilyName = dataTable.Rows[0]["FamilyName"].ToString();
                    applicant.Address = dataTable.Rows[0]["Address"].ToString();
                    applicant.EmailAddress = dataTable.Rows[0]["EmailAddress"].ToString();
                    applicant.CountryOfOrigin = dataTable.Rows[0]["CountryOfOrigin"].ToString();
                    applicant.Age = Convert.ToInt32(dataTable.Rows[0]["Age"].ToString());
                    applicant.Hired = Convert.ToBoolean(dataTable.Rows[0]["Hired"]);


                applicantDetails = applicant;
                return (applicantDetails!=null);
            }
            

            //applicantDetails = _context.Applicant.Find(id);
            //return (applicantDetails != null);
        }
        /// <summary>
        /// This method is called whenever there is a post request to add data.
        /// </summary>
        /// <param name="applicantDetails"></param>
        public async Task<int> AddApplicantDetailsAsync(ApplicantDetails applicantDetails)
        {
            using (MySqlConnection sqlCon = new MySqlConnection(ConnectionString))
            {
                sqlCon.Open();
                MySqlCommand SqlCommand = new MySqlCommand("ApplicantAddorEdit", sqlCon);
                SqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                SqlCommand.Parameters.AddWithValue("_ID",applicantDetails.ID);
                SqlCommand.Parameters.AddWithValue("_Name",applicantDetails.Name);
                SqlCommand.Parameters.AddWithValue("_FamilyName",applicantDetails.FamilyName);
                SqlCommand.Parameters.AddWithValue("_Address",applicantDetails.Address);
                SqlCommand.Parameters.AddWithValue("_CountryOfOrigin",applicantDetails.CountryOfOrigin);
                SqlCommand.Parameters.AddWithValue("_EmailAddress",applicantDetails.EmailAddress);
                SqlCommand.Parameters.AddWithValue("_Age",applicantDetails.Age);
                SqlCommand.Parameters.AddWithValue("_Hired",Convert.ToBoolean(applicantDetails.Hired));
                return await SqlCommand.ExecuteNonQueryAsync();
            }



            //    int rowsAffected = 0;

            //_context.Applicant.Add(applicantDetails);
            //rowsAffected = await _context.SaveChangesAsync();
            //return rowsAffected;
        }
        /// <summary>
        /// This method is called whenever there is a delete request.
        /// </summary>
        /// <param name="id"></param>
        public async Task<int> DelApplicantDetailsAsync(int id)
        {

            using (MySqlConnection sqlCon = new MySqlConnection(ConnectionString))
            {
                sqlCon.Open();
                MySqlCommand SqlCommand = new MySqlCommand("ApplicantDeleteByID", sqlCon);
                SqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                SqlCommand.Parameters.AddWithValue("_ID", id);
                
                return await SqlCommand.ExecuteNonQueryAsync();
            }


            //ApplicantDetails ad;
            //int rowsAffected = 0;
            //ad = _context.Applicant.Find(id);
            //_context.Applicant.Remove(ad);
            //rowsAffected = await _context.SaveChangesAsync();
            //return rowsAffected;

        }
        /// <summary>
        /// This method will check whether the sent id exists in the database or not. If it does it will
        /// update the record in database according to the given id sent along with the updated data to the api.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="applicantDetails"></param>
        public async Task<int> PutApplicantDetails(int id, ApplicantDetails applicantDetails)
        {
            using (MySqlConnection sqlCon = new MySqlConnection(ConnectionString))
            {
                sqlCon.Open();
                MySqlCommand SqlCommand = new MySqlCommand("ApplicantAddorEdit", sqlCon);
                SqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                SqlCommand.Parameters.AddWithValue("_ID", id);
                SqlCommand.Parameters.AddWithValue("_Name", applicantDetails.Name);
                SqlCommand.Parameters.AddWithValue("_FamilyName", applicantDetails.FamilyName);
                SqlCommand.Parameters.AddWithValue("_Address", applicantDetails.Address);
                SqlCommand.Parameters.AddWithValue("_CountryOfOrigin", applicantDetails.CountryOfOrigin);
                SqlCommand.Parameters.AddWithValue("_EmailAddress", applicantDetails.EmailAddress);
                SqlCommand.Parameters.AddWithValue("_Age", applicantDetails.Age);
                SqlCommand.Parameters.AddWithValue("_Hired", Convert.ToBoolean(applicantDetails.Hired));
                return  await SqlCommand.ExecuteNonQueryAsync();
            }

            //var existAD = _context.Applicant.Find(id);
            //int rowsAffected = 0;
            //if (existAD.ID != 0)
            //{
            //    existAD.Name = applicantDetails.Name;
            //    existAD.FamilyName = applicantDetails.FamilyName;
            //    existAD.Address = applicantDetails.Address;
            //    existAD.CountryOfOrigin = applicantDetails.CountryOfOrigin;
            //    existAD.EmailAddress = applicantDetails.EmailAddress;
            //    existAD.Age = applicantDetails.Age;
            //    existAD.Hired = applicantDetails.Hired;
            //    rowsAffected = await _context.SaveChangesAsync();
            //}
            //return rowsAffected;
        }



    }
}

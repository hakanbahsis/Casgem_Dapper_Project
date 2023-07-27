using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebUI.DAL.Entities;

namespace WebUI.Controllers
{
    public class HeadingController : Controller
    {
        private readonly string _connectionString="Server=DESKTOP-PK98KLS;initial catalog=CasgemDapperDb;integrated security=true;";

        public async Task<IActionResult> Index()
        {
            await using var con = new SqlConnection(_connectionString);
            var values = await con.QueryAsync<Headings>("select * from TblHeading");
            return View(values);
        }

        [HttpGet]
        public IActionResult AddHeading()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddHeading(Headings heading)
        {
            await using var connection = new SqlConnection(_connectionString);
            var query = $"INSERT INTO dbo.TblHeading VALUES ( '{heading.HeadingName}', '1')";
            await connection.ExecuteAsync(query);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteHeading(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            var query = $"Delete from TblHeading where HeadingId='{id}'";
            await connection.ExecuteAsync(query);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateHeading(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            var query = $"select *  from TblHeading where HeadingId='{id}'";
           var values= await connection.QueryFirstAsync<Headings>(query);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHeading(Headings heading)
        {
            
            await using var connection = new SqlConnection(_connectionString);
            var query = $"UPDATE TblHeading SET {nameof(heading.HeadingName)} = '{heading.HeadingName}', {nameof(heading.HeadingStatus)} = '{heading.HeadingStatus}' WHERE HeadingId = '{heading.HeadingId}'";
            await connection.ExecuteAsync(query);
            return RedirectToAction("Index");
        }
    }
}

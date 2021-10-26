using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Dapper;
using DapperDemo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DapperDemo.Repository
{
    public class CompanyRepositoryDapper : ICompanyRepository
    {
        private IDbConnection _db;

        public CompanyRepositoryDapper(IConfiguration configuration)
        {
            this._db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<Company> Add(Company company)
        {
            var sql = "INSERT INTO Companies (Name, Adress, City, State, PostalCode) VALUES(@Name, @Adress, @City, @State, @PostalCode);"
                        + "SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = await this._db.QueryAsync<int>(sql, company);
            company.CompanyId = id.SingleOrDefault();
            return company;
        }

        public async Task<Company> Find(int id)
        {
            var sql = "SELECT * FROM Companies WHERE CompanyId = @Id";
            var result = await this._db.QueryAsync<Company>(sql, new { @Id = id });

            return result.SingleOrDefault();
        }

        public async Task<List<Company>> GetAll()
        {
            var sql = "SELECT * FROM Companies";
            var result = await this._db.QueryAsync<Company>(sql);
            return result.ToList();
        }

        public async Task Delete(int id)
        {
            var sql = "DELETE FROM Companies WHERE CompanyId = @id";
            var result = await this._db.ExecuteAsync(sql, new { id });
        }

        public async Task<Company> Update(Company company)
        {
            var sql = "UPDATE Companies SET Name = @Name, Adress = @Adress, City = @City, State = @State, PostalCode = @PostalCode WHERE CompanyId = @CompanyId";
            await this._db.ExecuteAsync(sql, company);
            return company;
        }
    }
}

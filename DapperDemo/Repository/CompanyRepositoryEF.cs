using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperDemo.Data;
using DapperDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DapperDemo.Repository
{
    public class CompanyRepositoryEF : ICompanyRepository
    {
        private readonly AppDBContext _db;

        public CompanyRepositoryEF(AppDBContext db)
        {
            this._db = db;
        }

        public async Task<Company> Add(Company company)
        {
            await this._db.Companies.AddAsync(company);
            await this._db.SaveChangesAsync();

            return company;
        }

        public async Task<Company> Find(int id)
        {
            var company = await this._db.Companies.FirstOrDefaultAsync(s => s.CompanyId == id);
            return company;
        }

        public async Task<List<Company>> GetAll()
        {
            var companies = await this._db.Companies.ToListAsync();
            return companies;
        }

        public async Task Delete(int id)
        {
            var company = await this._db.Companies.FirstOrDefaultAsync(s => s.CompanyId == id);
            this._db.Companies.Remove(company);
            await this._db.SaveChangesAsync();
        }

        public async Task<Company> Update(Company company)
        {
            this._db.Companies.Update(company);
            await this._db.SaveChangesAsync();
            return company;
        }
    }
}

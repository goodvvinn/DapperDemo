using System.Threading.Tasks;
using DapperDemo.Models;
using DapperDemo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DapperDemo.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompanyRepository _companyRepository;

        public CompaniesController(ICompanyRepository companyRepository)
        {
            this._companyRepository = companyRepository;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return this.View(await this._companyRepository.GetAll());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var company = await this._companyRepository.Find(id.GetValueOrDefault());
            if (company == null)
            {
                return this.NotFound();
            }

            return this.View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,Name,Adress,City,State,PostalCode")] Company company)
        {
            if (this.ModelState.IsValid)
            {
                await this._companyRepository.Add(company);
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var company = await this._companyRepository.Find(id.GetValueOrDefault());
            if (company == null)
            {
                return this.NotFound();
            }

            return this.View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyId,Name,Adress,City,State,PostalCode")] Company company)
        {
            if (id != company.CompanyId)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                    await this._companyRepository.Update(company);
                    return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            await this._companyRepository.Delete(id.GetValueOrDefault());
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using RealtimeData.Data;

namespace RealtimeData.Controllers
{
    public partial class ExportCompanyDBController : ExportController
    {
        private readonly CompanyDBContext context;
        private readonly CompanyDBService service;

        public ExportCompanyDBController(CompanyDBContext context, CompanyDBService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/CompanyDB/employees/csv")]
        [HttpGet("/export/CompanyDB/employees/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmployeesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEmployees(), Request.Query), fileName);
        }

        [HttpGet("/export/CompanyDB/employees/excel")]
        [HttpGet("/export/CompanyDB/employees/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmployeesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEmployees(), Request.Query), fileName);
        }
    }
}

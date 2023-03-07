using RealtimeData.Models.CompanyDB;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using RealtimeData.Hubs;
using RealtimeData.Data;
using Microsoft.EntityFrameworkCore;
#if !RADZEN
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;
#endif

namespace RealtimeData.Services {


    public class RealtimeEmployeeDataService {

        private readonly IHubContext<EmployeeHub> _hubContext;
        #if !RADZEN
        private readonly SqlTableDependency<Employee> _dependency;
        #endif
        private readonly string _connectionString;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RealtimeEmployeeDataService(IHubContext<EmployeeHub> hubContext, IServiceScopeFactory serviceScopeFactory){
            _serviceScopeFactory = serviceScopeFactory;
            _hubContext = hubContext;
            _connectionString = "Server=DESKTOP-UL9R65A\\SQLEXPRESS;Connection Timeout=30;Persist Security Info=False;TrustServerCertificate=True;Integrated Security=True;Initial Catalog=CompanyDB";
            #if !RADZEN
            _dependency = new SqlTableDependency<Employee>(_connectionString, "Employee");
            _dependency.OnChanged += Changed;
            _dependency.Start();
            #endif
        }

        #if !RADZEN     
        public async void Changed(object sender, RecordChangedEventArgs<Employee> e)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CompanyDBContext>();
            var employees = await context.Employees.AsNoTracking().ToListAsync();
            await _hubContext.Clients.All.SendAsync("RefreshEmployees", employees);
        }
        #endif

    }

}
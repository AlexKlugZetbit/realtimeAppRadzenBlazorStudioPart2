using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace RealtimeData.Pages
{
    public partial class Employees
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        public CompanyDBService CompanyDBService { get; set; }

        protected IEnumerable<RealtimeData.Models.CompanyDB.Employee> employees;

        protected RadzenDataGrid<RealtimeData.Models.CompanyDB.Employee> grid0;
        protected override async Task OnInitializedAsync()
        {
            employees = await CompanyDBService.GetEmployees();
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddEmployee>("Add Employee", null);
            await grid0.Reload();
        }

        protected async Task EditRow(RealtimeData.Models.CompanyDB.Employee args)
        {
            await DialogService.OpenAsync<EditEmployee>("Edit Employee", new Dictionary<string, object> { {"Id", args.Id} });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, RealtimeData.Models.CompanyDB.Employee employee)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await CompanyDBService.DeleteEmployee(employee.Id);

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                { 
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error", 
                    Detail = $"Unable to delete Employee" 
                });
            }
        }
    }
}
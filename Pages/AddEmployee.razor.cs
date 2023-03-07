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
    public partial class AddEmployee
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

        protected override async Task OnInitializedAsync()
        {
            employee = new RealtimeData.Models.CompanyDB.Employee();
        }
        protected bool errorVisible;
        protected RealtimeData.Models.CompanyDB.Employee employee;

        protected async Task FormSubmit()
        {
            try
            {
                await CompanyDBService.CreateEmployee(employee);
                DialogService.Close(employee);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
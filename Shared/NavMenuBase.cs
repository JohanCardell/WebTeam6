using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WebTeam6.Data;
using WebTeam6.Services;
using WebTeam6.Pages.MenuComponents;

namespace WebTeam6.Shared
{
    public class NavMenuBase: ComponentBase
    {
        [Inject]
        public IGroupService GroupService { get; set; }

        [Inject]
        public AppData AppData { get; set; }

        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }

        public Group GroupObject { get; set; } = new Group();

        protected bool collapseNavMenu = true;

        protected string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        protected void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        protected override async Task OnInitializedAsync()
        {
            AppData.Groups = await GroupService.GetAuthorizedUserGroups(AuthenticationStateTask);
        }

        //protected async void Update()
        //{
        //    AppData.Groups = await GroupService.GetAuthorizedUserGroups(authenticationStateTask);
        //    Console.WriteLine("Updating sidebar");
        //    UserGroupList.ForEach(g => Console.WriteLine(g.Name));
        //    StateHasChanged();
        //}

        protected void InitializeGroupObject()
        {
            GroupObject = new Group();
        }
    }
}

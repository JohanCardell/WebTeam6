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
        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }
        public List<Group> UserGroupList { get; set; }
        public Group GroupObject { get; set; } = new Group();

        protected bool collapseNavMenu = true;

        protected string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        protected void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        protected override async Task OnInitializedAsync()
        {
            UserGroupList = await GroupService.GetGetAuthorizedUserGroups(authenticationStateTask);
        }

        protected async void DataChanged()
        {
            UserGroupList = await GroupService.GetGetAuthorizedUserGroups(authenticationStateTask);
            StateHasChanged();
        }
        protected void InitializeGroupObject()
        {
            GroupObject = new Group();
        }


    }
}

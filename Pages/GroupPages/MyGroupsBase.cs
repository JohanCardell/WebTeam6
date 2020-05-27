using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTeam6.Data;
using WebTeam6.Services;

namespace WebTeam6.Pages.GroupPages
{
    public class MyGroupsBase : ComponentBase

    {
        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        public IGroupService GroupService { get; set; }

        protected List<Group> MyGroupsList { get; set; }
       
        protected Group GroupObject { get; set; }

        protected override async Task OnInitializedAsync()
        {
            MyGroupsList = await GroupService.GetGetAuthorizedUserGroups(authenticationStateTask);
        }
        protected void InitializeGroupObject()
        {
            GroupObject = new Group();
        }

        protected async void DataChanged()
        {
            MyGroupsList = await GroupService.GetGetAuthorizedUserGroups(authenticationStateTask);
            StateHasChanged();
        }
    }
}

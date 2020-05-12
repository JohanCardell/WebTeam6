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

        protected List<Group> UserGroupList { get; set; }
        [Inject]
        public IGroupService GroupService { get; set; }
        //protected int counter;

        protected override async Task OnInitializedAsync()
        {
            UserGroupList = await GroupService.GetGetAuthorizedUserGroups(authenticationStateTask);
            //counter = UserGroupList.Count();
        }
    }
}

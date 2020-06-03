using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTeam6.Data;
using WebTeam6.Services;

namespace WebTeam6.Pages.MessagePages
{
    public class MessageListBase : ComponentBase
    {
        [Inject]
        private IMessageService Service { get; set; }
        
        [Inject]
        private IUserService UserService { get; set; }

        [Parameter]
        public Group Group { get; set; }

        [Parameter]
        public IEnumerable<Message> Messages { get; set; }
        public Message Message { get; set; } = new Message { Time = DateTime.Now };
        public User User { get; set; }

        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }

        protected async void DataChanged()
        {
            var res = await Service.Get(Group);
            if (res != null) Messages = res.OrderBy(m => m.Time).Take(10);
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Messages = Group.Messages;
            User = await UserService.GetAuthorizedUser(authenticationStateTask);
        }
        public async void Add()
        {
            Message.Group = Group;
            Message.Creator = User;
            var res = await Service.Add(Message);
            if (res != null)
            {
                Message = new Message { Time = DateTime.Now };
                DataChanged();
                base.StateHasChanged();
            }
        }
        protected async void Delete(Message Message)
        {
            if (Message != null)
            {
                await Service.Delete(Message.Id);
                DataChanged();
                base.StateHasChanged();
            }
        }
    }
}

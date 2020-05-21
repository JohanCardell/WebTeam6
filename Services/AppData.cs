using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTeam6.Data;

namespace WebTeam6.Services
{
    public class AppData
    {
        private ICollection<Group> _groups;
        private Group _selected;
        public Group SelectedGroup {
            get
            {
                return _selected;
            }
            set {
                _selected = value;
                NotifyDataChanged();
            } 
        }
        public ICollection<Group> Groups {
            get
            {
                return _groups;
            }
            set {
                _groups = value;
                NotifyDataChanged();
            } 
        }
        public event Action OnChange;
        private void NotifyDataChanged() => OnChange?.Invoke();
    }
}

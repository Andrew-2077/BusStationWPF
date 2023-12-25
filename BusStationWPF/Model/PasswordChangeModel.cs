using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model
{
    public class PasswordChangeModel
    {
        private BehaviourPasswordBox newPassword;
        private BehaviourPasswordBox oldPassword;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        public string NewPassword
        {
            get => newPassword.Password;
        }
        public string OldPassword
        {
            get => oldPassword.Password;
        }
        public PasswordChangeModel(BehaviourPasswordBox newPassword, BehaviourPasswordBox oldPassword)
        {
            this.newPassword = newPassword;
            this.oldPassword = oldPassword;
        }
    }
}

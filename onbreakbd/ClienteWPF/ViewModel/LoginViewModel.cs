using ClienteWPF.Model;
using ClienteWPF.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClienteWPF.ViewModel
{
    internal class LoginViewModel : ObservableCollection<User>, INotifyPropertyChanged
    {
        #region Atributos
        private string user;
        private string password;
        private ICommand getInCommand;
        #endregion

        #region Propiedades
        public string User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public ICommand GetInCommand
        {
            get
            {
                return getInCommand;
            }
            set
            {
                getInCommand = value;
            }
        }
        #endregion

        #region Constructores
        public LoginViewModel()
        {
            GetInCommand = new CommandBase(param => this.GetInSesion());
        }
        #endregion

        #region Interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Metodos/Funciones
        private void GetInSesion()
        {
            User vlClient = new User();
            vlClient.usuario = user;
            vlClient.contrasena = password;
            this.Add(vlClient);
        }
        #endregion
    }
}

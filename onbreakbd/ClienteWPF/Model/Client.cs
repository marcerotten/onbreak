using ClienteWPF.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteWPF.Model
{
    class Client : NotifyBase
    {

        #region Atributos
        private int id;
        private string name;
        private string lastname;
        #endregion

        #region Propiedades
        public int Id
        {
            get
            {
                return id;
            }//Fin de get.
            set
            {
                id = value;
                OnPropertyChanged("Id");
                OnPropertyChanged("DisplayName");
            }//Fin de set.
        }//Fin de propiedad Id.

        public string Name
        {
            get
            {
                return name;
            }//Fin de get.
            set
            {
                name = value;
                OnPropertyChanged("Name");
                OnPropertyChanged("DisplayName");
            }//Fin de set.
        }//Fin de propiedad Name.

        public string LastName
        {
            get
            {
                return lastname;
            }//Fin de get.
            set
            {
                lastname = value;
                OnPropertyChanged("LastName");
                OnPropertyChanged("DisplayName");
            }//Fin de set.
        }//Fin de propiedad LastName.

        public string DisplayName
        {
            get
            {
                return Id + "-" + Name + " " + LastName;
            }//Fin de get.
        }//Fin de la propiedad readonly DisplayName.
        #endregion
    }
}

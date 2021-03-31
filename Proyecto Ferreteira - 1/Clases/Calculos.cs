using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Proyecto_Ferreteira___1.Clases
{
   public class Calculos : INotifyPropertyChanged
    {
        private string precio;
        private string cantidad;
        private string subtotal;

        public Calculos()
        { }

        public string Precio
        {
            get { return precio; }
            set
            {
                int numero;
                bool operacion = int.TryParse(value, out numero);
                if (operacion) precio = value;

                // La propiedad cambia, avisar a la interfaz
                OnPropertyChanged("Precio");
                OnPropertyChanged("Subtotal");
            }
        }

        public string Cantidad
        {
            get { return cantidad; }
            set
            {
                int numero;
                if (int.TryParse(value, out numero)) cantidad = numero.ToString();

                // La propiedad cambia, avisar a la interfaz
                OnPropertyChanged("Cantidad");
                OnPropertyChanged("Subtotal");
            }
        }

        public string Subtotal
        {
            get
            {
                int respuesta = int.Parse(Precio) * int.Parse(Cantidad);
                return respuesta.ToString();
            }
            set
            {
                int respuesta = int.Parse(Precio) * int.Parse(Cantidad);
                subtotal = respuesta.ToString();

                // La propiedad cambia, avisar a la interfaz
                OnPropertyChanged("Subtotal");
            }
        }


        // Requerido por la interfaz INotifiyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }


    
    }
}

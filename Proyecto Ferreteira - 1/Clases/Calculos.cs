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
        private string descuento;
        private string isv;
        private string total;
        private string flete;


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
                OnPropertyChanged("Descuento");
                OnPropertyChanged("ISV");
                OnPropertyChanged("Total");
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
                OnPropertyChanged("Descuento");
                OnPropertyChanged("ISV");
                OnPropertyChanged("Total");

            }
        }

        public string Flete
        {
            get { return flete; }
            set
            {
                int numero;
                if (int.TryParse(value, out numero)) flete = numero.ToString();

                // La propiedad cambia, avisar a la interfaz
                OnPropertyChanged("Total");

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

        public string Descuento
        {
            get
            {
                Double respuesta = (Double.Parse(Precio) * Double.Parse(Cantidad)) * 0.10 ;
                return respuesta.ToString();
            }
            set
            {
                Double respuesta = (Double.Parse(Precio) * Double.Parse(Cantidad)) * 0.10;
                descuento = respuesta.ToString();
                // La propiedad cambia, avisar a la interfaz
                OnPropertyChanged("Descuento");
            }

        }

        public string ISV
        {
            get
            {
                Double respuesta = ((Double.Parse(Precio) * Double.Parse(Cantidad)) - ((Double.Parse(Precio) * Double.Parse(Cantidad))*0.10)) * 0.12;
                return respuesta.ToString();
            }
            set
            {
                Double respuesta = ((Double.Parse(Precio) * Double.Parse(Cantidad)) - ((Double.Parse(Precio) * Double.Parse(Cantidad)) * 0.10)) * 0.12;
                isv = respuesta.ToString();
                // La propiedad cambia, avisar a la interfaz
                OnPropertyChanged("ISV");
            }

        }

        public string Total
        {
            get
            {
                Double respuesta = ((Double.Parse(Precio) * Double.Parse(Cantidad)) - ((Double.Parse(Precio) * Double.Parse(Cantidad)) * 0.10) + 
                    ((Double.Parse(Precio) * Double.Parse(Cantidad)) - ((Double.Parse(Precio) * Double.Parse(Cantidad)) * 0.10)) * 0.12) + Double.Parse(Flete);

                return respuesta.ToString();
            }
            set
            {
                Double respuesta = ((Double.Parse(Precio) * Double.Parse(Cantidad)) - ((Double.Parse(Precio) * Double.Parse(Cantidad)) * 0.10) +
                    ((Double.Parse(Precio) * Double.Parse(Cantidad)) - ((Double.Parse(Precio) * Double.Parse(Cantidad)) * 0.10)) * 0.12) + Double.Parse(Flete);

                total = respuesta.ToString();
                // La propiedad cambia, avisar a la interfaz
                OnPropertyChanged("Total");
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

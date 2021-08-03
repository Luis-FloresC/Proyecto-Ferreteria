using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proyecto_Ferreteira___1
{
    /// <summary>
    /// Lógica de interacción para FormEditarPerfil.xaml
    /// </summary>
    public partial class FormEditarPerfil : UserControl
    {

       


        public FormEditarPerfil()
        {
            InitializeComponent();
            LoadEditarPerfil();
        }

        /// <summary>
        /// Metodo para validar los campos
        /// </summary>
        /// <returns></returns>
        private bool VerificarDatos()
        {
            bool VerificacionCorrecta = true;


            if (CadenaSoloEspacios(txtDNI.Text) || CadenaSoloEspacios(txtNombre.Text) ||
               CadenaSoloEspacios(txtApellido.Text) || CadenaSoloEspacios(txtEmail.Text) ||
               CadenaSoloEspacios(txtUsuario.Text) || CadenaSoloEspacios(txtConfirmarPassword.Password) ||
               CadenaSoloEspacios(txtPassword.Password))
            {

                MessageBox.Show("Todos los Campos son Obligatorio", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                VerificacionCorrecta = false;

            }

            if (txtPassword.Password.Length < 8)
            {
                MessageBox.Show("La contraseña debe tener minimo 8 caracteres,Vuelva a intentar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
                
            }

            if(txtPassword.Password != txtConfirmarPassword.Password)
            {
                MessageBox.Show("La contraseña no coincide,Vuelva a intentar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if(txtPassWordActual.Password != Clases.CacheUsuario.Contraseña)
            {
                MessageBox.Show("La contraseña es incorrecta,Vuelva a intentar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
               
                return false;
            }

            if (!ValidarEmail(txtEmail.Text))
            {
                MessageBox.Show("Ingrese un Correo Electronico Valido", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!VerificarIdentidad(txtDNI.Text))
            {
                MessageBox.Show("Verificar su Identidad", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (CadenaSoloEspacios(txtNombre.Text) || CadenaSoloEspacios(txtApellido.Text))
            {
                MessageBox.Show("El Nombre o Apellido deben tener al menos 2 letras", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (txtDNI.Text.Length <= 12)
            {
                MessageBox.Show("La Identidad tiene que tener entre 13 caracteres", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return VerificacionCorrecta;
        }



        /// <summary>
        /// Metodo para permitir solo Letras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidacionLetras(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = true;
                MessageBox.Show("Solo se permiten letras", "Advertencia",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            else
            {
                e.Handled = false;
            }
        }

        /// <summary>
        /// Metodo para permitir solo Numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidacionNumeros(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Solo se permiten números", "Advertencia",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Validar Cadenas vacias
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        private bool CadenaSoloEspacios(string cadena)
        {
            String source = cadena; //Original text

            if (source.Trim().Length <= 1)
            {
                return true;
            }
            else
            {

                return false;
            }

        }

        /// <summary>
        /// Validar Direccion de Correo Electronico
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private Boolean ValidarEmail(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public Dictionary<int, string> Departamentos = new Dictionary<int, string>();

        private void AggDatosDiccionario()
        {
            Departamentos.Clear();
            Departamentos.Add(1, "1-8");
            Departamentos.Add(2, "1-10");
            Departamentos.Add(3, "1-21");
            Departamentos.Add(4, "1-23");
            Departamentos.Add(5, "1-12");
            Departamentos.Add(6, "1-16");
            Departamentos.Add(7, "1-19");
            Departamentos.Add(8, "1-28");
            Departamentos.Add(9, "1-6");
            Departamentos.Add(10, "1-17");
            Departamentos.Add(11, "1-4");
            Departamentos.Add(12, "1-19");
            Departamentos.Add(13, "1-28");
            Departamentos.Add(14, "1-16");
            Departamentos.Add(15, "1-23");
            Departamentos.Add(16, "1-28");
            Departamentos.Add(17, "1-9");
            Departamentos.Add(18, "1-11");

        }

        private int Li;
        private int Ls;

        /// <summary>
        /// Encontrar si El departamento existe
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private bool BuscarDiccionario(int x)
        {
            if (Departamentos.ContainsKey(x))
            {
                String source = Departamentos[x]; //Original text
                String[] result = source.Split(new char[] { '-', '-' });
                Li = int.Parse(result[0]);
                Ls = int.Parse(result[1]);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool VerificarIdentidad(string cadena)
        {
            bool resultado = false;
            int depto = int.Parse(cadena.Substring(0, 2));
            int muni = int.Parse(cadena.Substring(2, 2));
            int año = int.Parse(cadena.Substring(4, 4));
            int folio = int.Parse(cadena.Substring(8, 5));

            AggDatosDiccionario();


            if (NumerosEnteros(depto, 1, 18))
            {

                bool Est = BuscarDiccionario(depto);
                if (NumerosEnteros2(muni, Li, Ls))
                {
                    if (NumerosEnteros(año, 1900, 2100))
                    {
                        if (NumerosEnteros(folio, 1, 99999))
                        {
                            resultado = true;
                        }
                        else
                        {
                            MessageBox.Show("El folio debe tener un rango del 00001- 99999", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }


                    }
                    else
                    {
                        MessageBox.Show("El año debe estar en un rango del 1900-2100", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {

                    return false;
                }
            }
            else
            {
                MessageBox.Show("Los primeros dos número de la identidad \ndeben estar en un rango de 1-18.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return resultado;
        }

        private bool NumerosEnteros(int valor, int li, int ls)
        {
            if (valor < li || valor > ls)
            {
                return false;
            }
            else
                return true;
        }


        private bool NumerosEnteros2(int valor, int li, int ls)
        {
            if (valor < li || valor > ls)
            {
                MessageBox.Show(string.Concat("Los siguientes dos dígitos: ", valor, " del municipio", "\nsolo se permiten número del rango: ", li, " y ", ls, "."), "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// Metodo para Editar los datos personales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if(VerificarDatos())
            {
                var Resultado = usuarios.EditarDatos(txtNombre.Text, txtApellido.Text, txtUsuario.Text, txtPassword.Password, txtEmail.Text,txtDNI.Text);
                MessageBox.Show(Resultado,"Aviso",MessageBoxButton.OK,MessageBoxImage.Information);
                LoadEditarPerfil();
            }
        }


        /// <summary>
        /// Evento Click para cancelar la operacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Color.Text = "#242222";
            Panel2.Children.Clear();
            Panel2.Visibility = Visibility.Hidden;
            
        }

        /// <summary>
        /// Funcion para habilitar los campos de Contraseña y Confrimar Constraseña
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chEditar_Checked(object sender, RoutedEventArgs e)
        {
            if (chEditar.IsChecked != true)
            {

                txtPassword.IsEnabled = false;
                txtConfirmarPassword.IsEnabled = false;
                txtPassword.Password = Clases.CacheUsuario.Contraseña;
                txtConfirmarPassword.Password = Clases.CacheUsuario.Contraseña;
            }
            else
            {
                txtPassword.Password = string.Empty;
                txtConfirmarPassword.Password = string.Empty;
                txtPassword.IsEnabled = true; 
                txtConfirmarPassword.IsEnabled = true;
            }
        }


       /// <summary>
       /// Instancia para llamar al Formulario de Usuarios
       /// </summary>
        FormUsuarios formUsuario = new FormUsuarios();

        /// <summary>
        /// Instancia para llamar a la clase de Usuarios
        /// </summary>
        public Clases.Usuarios usuarios { get; set; }
        private void LoadEditarPerfil()
        {
            usuarios = new Clases.Usuarios
            {
                NombreUsuario = Clases.CacheUsuario.Usuario,
                Nombre = Clases.CacheUsuario.NombreCompleto,
                Apellido = Clases.CacheUsuario.ApellidoCompleto,
                Email = Clases.CacheUsuario.Email,
                Cargo = Clases.CacheUsuario.Cargo,
                DNI = Clases.CacheUsuario.DNI
            };
            this.DataContext = usuarios;


            txtPassword.Password = Clases.CacheUsuario.Contraseña;
            txtConfirmarPassword.Password = Clases.CacheUsuario.Contraseña;
           
         

        }


    }
}

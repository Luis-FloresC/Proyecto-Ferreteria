using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Proyecto_Ferreteira___1
{
    /// <summary>
    /// Lógica de interacción para FormEmpleados.xaml
    /// </summary>
    public partial class FormEmpleados : UserControl
    {

        /// <summary>
        /// Instancia para llamar a la clase de Empleados
        /// </summary>
        Clases.Empleados Empleados = new Clases.Empleados();
        Clases.Usuarios usuario = new Clases.Usuarios();

        /// <summary>
        /// Lista para alamcear los datos del empleado
        /// </summary>
        private List<Clases.UserData> ListaCargos;

        /// <summary>
        /// Datatable para llenar el DatagridView
        /// </summary>
        private DataTable DataTable;

        public FormEmpleados()
        {
            InitializeComponent();
            CargarDatosComboBoxCargo();
            CargarDatosDataGrid();
            if (!usuario.Permisos())
            {
                BtnModificar.Visibility = Visibility.Hidden;
                btnEliminar.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Metodo para Visualizar los datos en el DatagridView
        /// </summary>
        private void CargarDatosDataGrid()
        {
            
            DataTable = Empleados.MostarDataTableEmpleado();
            DataGridEmpleados.ItemsSource = DataTable.DefaultView;
            var TotalRows = DataGridEmpleados.Items.Count;
            txtTotal.Text = "Total: " + TotalRows;
        }
        /// <summary>
        /// Obtiene el los valores de los cargos Dsiponibles en nuestra base de datos
        /// </summary>
        private void CargarDatosComboBoxCargo()
        {
            ListaCargos = Empleados.Cargos();
            cmbCargo.SelectedValuePath = "Id";
            cmbCargo.DisplayMemberPath = "Cargo";
            cmbCargo.ItemsSource = ListaCargos;
        }

        /// <summary>
        /// Metodo para cancelar la operacion a realizar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("¿Esta Seguro que desea cancelar la operacion?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes))
            {
                LimpiarCampos();
            }
          
        }

        /// <summary>
        /// Metodo para hacer una insercion en la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if(Validaciones())
            {

                int codigo = (int)cmbCargo.SelectedValue;
                var Resultado = Empleados.AñadirNuevoEmpleado(
                                                               txtNombreEmpleado.Text,
                                                               txtApellidoEmpleado.Text,
                                                               codigo,
                                                               txtTelefono.Text,
                                                               txtEmail.Text,
                                                               txtDireccion.Text,
                                                               true,
                                                               FechaNac.SelectedDate.ToString(),
                                                               txtDNI.Text);

                MessageBox.Show(Resultado, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                CargarDatosDataGrid();
                LimpiarCampos();
            }
           


        }

        /// <summary>
        /// Metodo para llenar los Campos cuando seleccionamos una fila del DataGridView
        /// </summary>
        private void LlenarCampos()
        {
            txtDNI.Text = Clases.CacheEmpleado.DNI;
            txtNombreEmpleado.Text = Clases.CacheEmpleado.NombreEmpleado;
            txtApellidoEmpleado.Text = Clases.CacheEmpleado.ApellidoEmpleado;
            txtEmail.Text = Clases.CacheEmpleado.Email;
            txtTelefono.Text = Clases.CacheEmpleado.Telefono;
            txtDireccion.Text = Clases.CacheEmpleado.Direccion;
            cmbCargo.Text = Clases.CacheEmpleado.Cargo;
            FechaNac.SelectedDate = Clases.CacheEmpleado.FechaNacimiento;
        }

        /// <summary>
        /// Metodo para seleccionar una fila del DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridEmpleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridEmpleados.SelectedIndex != -1)
            {
                int id = Convert.ToInt32((DataGridEmpleados.CurrentItem as DataRowView).Row.ItemArray[0].ToString());
                var BuscarEmpleado = Empleados.BuscarEmpleado(id);

                if(!BuscarEmpleado)
                {
                    MessageBox.Show("Error");
                }
                else
                {
                    LlenarCampos();
                }
            }
          
        }

       
        /// <summary>
        /// Metodo para Modificar Datos del Empleado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridEmpleados.SelectedIndex != -1 && Validaciones())
            {

                int codigo = (int)cmbCargo.SelectedValue;
                var Resultado = Empleados.ModificarDatosPersonales(
                                                               txtNombreEmpleado.Text,
                                                               txtApellidoEmpleado.Text,
                                                               txtDNI.Text,
                                                               txtEmail.Text,
                                                               txtDireccion.Text,
                                                               FechaNac.SelectedDate.Value,
                                                               codigo,
                                                               txtTelefono.Text
                                                               );

                MessageBox.Show(Resultado, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                CargarDatosDataGrid();
                LimpiarCampos();
            }
            

        }

        /// <summary>
        /// Metodo para Desabilitar un Empleado de la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridEmpleados.SelectedIndex != -1 )
            {

                if((MessageBox.Show("¿Esta Seguro que desea Eliminar el Empleado?","Advertencia",MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes))
                {
                    var Resultado = Empleados.EliminarUsuario(Clases.CacheEmpleado.IdEmpleado);
                    MessageBox.Show(Resultado, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarDatosDataGrid();
                    LimpiarCampos();
                }

               
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
                MessageBox.Show(string.Concat("Los siguientes dos digitos: ", valor, " del municipio.", "\nSolo se permiten numero del rango: ", li, " y ", ls, "."), "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else
                return true;
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
                        if(NumerosEnteros(folio,1,99999))
                        {
                            resultado = true;
                        }
                        else
                        {
                            MessageBox.Show("el folio debe tener un rango del 00001- 99999", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        

                    }
                    else
                    {
                        MessageBox.Show("el año debe estar en un rango del 1900-2100", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {

                    return false;
                }
            }
            else
            {
                MessageBox.Show("los primeros dos numero de la identidad. \ndeben estar en un rango de 1-18.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return resultado;
        }

        /// <summary>
        /// Funcion para tener una diferencia de edad
        /// </summary>
        /// <returns></returns>
        private bool DiferenciaEdad()
        {

            var timeSpan = DateTime.Now - FechaNac.DisplayDate;
            int Edad = new DateTime(timeSpan.Ticks).Year - 1;

            if (Edad < 18)
            {
                return true;
            }
            else
                return false;

        }

        /// <summary>
        /// Metodo para vaidar los campos del formulario
        /// </summary>
        /// <returns></returns>
        private bool Validaciones()
        {
            bool datosCorrectos = true;
           
            if( CadenaSoloEspacios(txtDNI.Text) || CadenaSoloEspacios(txtNombreEmpleado.Text) ||
                CadenaSoloEspacios(txtApellidoEmpleado.Text) || CadenaSoloEspacios(txtEmail.Text) ||
                CadenaSoloEspacios(txtTelefono.Text) || CadenaSoloEspacios(txtDireccion.Text) ||
                (cmbCargo.SelectedValue == null) || (FechaNac.SelectedDate == null) )
            {
               
                MessageBox.Show("Todos los Campos son Obligatorio","Advertencia",MessageBoxButton.OK,MessageBoxImage.Warning);
                datosCorrectos = false;

            }
            
            if(!VerificarIdentidad(txtDNI.Text))
            {
                MessageBox.Show("Verificar su Identidad", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if(CadenaSoloEspacios(txtNombreEmpleado.Text) || CadenaSoloEspacios(txtApellidoEmpleado.Text))
            {
                MessageBox.Show("El Nombre o Apellido deben tener al menos 2 letras", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (txtDNI.Text.Length <=  12 )
            {
                MessageBox.Show("La Identidad tiene que tener entre 13 caracteres", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;   
            }

            if(DiferenciaEdad())
            {
                MessageBox.Show("1. Debe ser mayor de Edad\n" +
                                "2. Ingrese una Fecha valida", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (txtTelefono.Text.Length <= 7)
            {
                MessageBox.Show("El telefono debe tener al menos 8 numeros", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;

            }

            if(!ValidarEmail(txtEmail.Text))
            {
               
                MessageBox.Show("Ingrese un Correo Electronico Valido", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return datosCorrectos;

        }

        /// <summary>
        /// Boton para limpiar los valores de los campos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        /// <summary>
        /// Metodo para limpiar todos los campos
        /// </summary>
        private void LimpiarCampos()
        {
            txtDNI.Text = string.Empty;
            txtNombreEmpleado.Text = string.Empty;
            txtApellidoEmpleado.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            cmbCargo.Text = null;
            FechaNac.SelectedDate = null;
        }

        /// <summary>
        /// Metodo para bsucar de una forma dinamicia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable = Empleados.BuscarDataTableEmpleado(txtBuscar.Text);
            DataGridEmpleados.ItemsSource = DataTable.DefaultView;
            var TotalRows = DataGridEmpleados.Items.Count;
            txtTotal.Text = "Total: " + TotalRows;
        }

        

        /// <summary>
        /// Metodo para permitir solo Letras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidacionLetras(object sender,KeyEventArgs e )
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = true;
                MessageBox.Show("Solo se Permiten Letras", "Aviso");
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
                MessageBox.Show("Solo se Permiten Numeros", "Aviso");
            }
        }

        private void txtApellidoEmpleado_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void FechaNac_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}

﻿using System;
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
        /// Verficacion de Numero de telefono
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public bool VerificarNumeroTelefono(string numero)
        {
            try
            {

                if (numero.Length > 8 || numero.Length < 8)
                {
                    return false;
                }

                if (int.Parse(numero.Substring(0, 1)) == 9 || int.Parse(numero.Substring(0, 1)) == 8 || int.Parse(numero.Substring(0, 1)) == 3 || int.Parse(numero.Substring(0, 1)) == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Metodo para Visualizar los datos en el DatagridView
        /// </summary>
        private void CargarDatosDataGrid()
        {
            try
            {
                DataTable = Empleados.MostarDataTableEmpleado();
                DataGridEmpleados.ItemsSource = DataTable.DefaultView;
                var TotalRows = DataGridEmpleados.Items.Count;
                txtTotal.Text = "Total: " + TotalRows;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
         
        }
        /// <summary>
        /// Obtiene el los valores de los cargos Dsiponibles en nuestra base de datos
        /// </summary>
        private void CargarDatosComboBoxCargo()
        {
            try
            {
                ListaCargos = Empleados.Cargos();
                cmbCargo.SelectedValuePath = "Id";
                cmbCargo.DisplayMemberPath = "Cargo";
                cmbCargo.ItemsSource = ListaCargos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// Metodo para cancelar la operacion a realizar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("¿Está seguro que desea cancelar la operación?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes))
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
            try
            {
                if (Validaciones())
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
                                                                   FechaNac.SelectedDate.Value,
                                                                   txtDNI.Text);

                    MessageBox.Show(Resultado, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarDatosDataGrid();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Metodo para llenar los Campos cuando seleccionamos una fila del DataGridView
        /// </summary>
        private void LlenarCampos()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Metodo para seleccionar una fila del DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridEmpleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (DataGridEmpleados.SelectedIndex != -1)
                {
                    int id = Convert.ToInt32((DataGridEmpleados.CurrentItem as DataRowView).Row.ItemArray[0].ToString());
                    var BuscarEmpleado = Empleados.BuscarEmpleado(id);

                    if(!BuscarEmpleado)
                    {
                        MessageBox.Show("Error","Error",MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        LlenarCampos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

       
        /// <summary>
        /// Metodo para Modificar Datos del Empleado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridEmpleados.SelectedIndex != -1 && Validaciones())
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Metodo para Desabilitar un Empleado de la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridEmpleados.SelectedIndex != -1 )
                {
                    if((MessageBox.Show("¿Está seguro que desea eliminar el empleado?","Advertencia",MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes))
                    {
                        var Resultado = Empleados.EliminarUsuario(Clases.CacheEmpleado.IdEmpleado);
                        MessageBox.Show(Resultado, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                        CargarDatosDataGrid();
                        LimpiarCampos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Validar Cadenas vacias
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        private bool CadenaSoloEspacios(string cadena)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
        }

        /// <summary>
        /// Validar Direccion de Correo Electronico
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private Boolean ValidarEmail(String email)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }


        /// <summary>
        /// Metodo para validar solo numeros enteros en un rango determinado
        /// </summary>
        /// <param name="valor">numero a verificar</param>
        /// <param name="li">limite inferior</param>
        /// <param name="ls">limite superior</param>
        /// <returns></returns>
        private bool NumerosEnteros(int valor, int li, int ls)
        {
            try
            {
                if (valor < li || valor > ls)
                {
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }

        /// <summary>
        /// Metodo para validar solo numeros enteros en un rango determinado
        /// </summary>
        /// <param name="valor">numero a verificar</param>
        /// <param name="li">limite inferior</param>
        /// <param name="ls">limite superior</param>
        /// <returns></returns>
        private bool NumerosEnteros2(int valor, int li, int ls)
        {
            try
            {
                if (valor < li || valor > ls)
                {
                    MessageBox.Show(string.Concat("Los siguientes dos digitos: ", valor, " del municipio.", "\nSolo se permiten números del rango: ", li, " y ", ls, "."), "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }

        /// <summary>
        /// Diccionario para los municipios de cada departamento
        /// </summary>
        public Dictionary<int, string> Departamentos = new Dictionary<int, string>();

        private void AggDatosDiccionario()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// Variable de limite inferior y limite superior
        /// </summary>
        private int Li;
        private int Ls;

        /// <summary>
        /// Encontrar si El departamento existe
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private bool BuscarDiccionario(int x)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private bool VerificarIdentidad(string cadena)
        {

            try
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
                                MessageBox.Show("El folio debe tener un rango del 00001- 99999", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                                txtDNI.Focus();
                            }
                        

                        }
                        else
                        {
                            MessageBox.Show("El año debe estar en un rango del 1900-2100", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                            txtDNI.Focus();
                        }
                    }
                    else
                    {

                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Los primeros dos numero de la identidad \ndeben estar en un rango de 1-18.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtDNI.Focus();
                    return false;
                }

                return resultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

          
        }

        /// <summary>
        /// Funcion para tener una diferencia de edad
        /// </summary>
        /// <returns></returns>
        private bool DiferenciaEdad()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// Metodo para vaidar los campos del formulario
        /// </summary>
        /// <returns></returns>
        private bool Validaciones()
        {
            try
            {
                bool datosCorrectos = true;
                bool telefonoCheck = int.TryParse(txtTelefono.Text, out int Numero);
                if( CadenaSoloEspacios(txtDNI.Text) || CadenaSoloEspacios(txtNombreEmpleado.Text) ||
                    CadenaSoloEspacios(txtApellidoEmpleado.Text) || CadenaSoloEspacios(txtEmail.Text) ||
                    CadenaSoloEspacios(txtTelefono.Text) || CadenaSoloEspacios(txtDireccion.Text) ||
                    (cmbCargo.SelectedValue == null) || (FechaNac.SelectedDate == null) )
                {
               
                    MessageBox.Show("Todos los campos son obligatorio","Advertencia",MessageBoxButton.OK,MessageBoxImage.Warning);
                    datosCorrectos = false;

                }
            
                if(!VerificarIdentidad(txtDNI.Text))
                {
                    MessageBox.Show("Verificar su identidad", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtDNI.Focus();
                    return false;
                }

                if (!VerificarNumeroTelefono(txtTelefono.Text))
                {
                    MessageBox.Show("Ingrese un número de teléfono válido!", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtTelefono.Focus();
                    return false;
                }

                if (CadenaSoloEspacios(txtNombreEmpleado.Text) || CadenaSoloEspacios(txtApellidoEmpleado.Text))
                {
                    MessageBox.Show("El nombre o apellido deben tener al menos 2 letras", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    if(CadenaSoloEspacios(txtNombreEmpleado.Text))
                    {
                        txtNombreEmpleado.Focus();
                    }
                    else
                    {
                        txtApellidoEmpleado.Focus();
                    }
                    return false;
                }

                if (txtDNI.Text.Length <=  12 )
                {
                    MessageBox.Show("La Identidad debe tener 13 caracteres", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtDNI.Focus();
                    return false;   
                }

                if(DiferenciaEdad())
                {
                    MessageBox.Show("1. Debe ser mayor de edad.\n" +
                                    "2. Ingrese una fecha válida", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    FechaNac.Focus();
                    return false;
                }

                if (txtTelefono.Text.Length <= 7)
                {
                    MessageBox.Show("El teléfono debe tener al menos 8 némeros", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtTelefono.Focus();
                    return false;

                }

                if (!telefonoCheck)
                {
                    MessageBox.Show("Ingrese un número de teléfono válido", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtTelefono.Focus();
                    return false;

                }

                if (!ValidarEmail(txtEmail.Text))
                {
                    MessageBox.Show("Ingrese un correo electrónico válido", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtEmail.Focus();
                    return false;
                }
                return datosCorrectos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
          

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
            try
            {
                DataTable = Empleados.BuscarDataTableEmpleado(txtBuscar.Text);
                DataGridEmpleados.ItemsSource = DataTable.DefaultView;
                var TotalRows = DataGridEmpleados.Items.Count;
                txtTotal.Text = "Total: " + TotalRows;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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
                MessageBox.Show("Sólo se permiten letras", "Avertencia",MessageBoxButton.OK, MessageBoxImage.Warning);
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
                MessageBox.Show("Sólo se permiten números", "Advertencia",MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    

        private void FechaNac_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}

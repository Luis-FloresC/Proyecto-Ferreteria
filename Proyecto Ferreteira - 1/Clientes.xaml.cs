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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace Proyecto_Ferreteira___1
{
    /// <summary>
    /// Lógica de interacción para Clientes.xaml
    /// </summary>
    public partial class Clientes : UserControl
    {
        //Objetos
        Clases.ClsCliente cliente = new Clases.ClsCliente();

        //Variable miembro
        private SqlConnection sqlConnection;

        public Clientes()
        {
            InitializeComponent();
            // Realizar la conexión con el servidor de base de datos
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Ferreteira___1.Properties.Settings.FerreteriaDb"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
            cargarTabla();
        }

        /// <summary>
        /// Guarda los datos del formulario en la clase ClsCliente
        /// </summary>
        private void guardarDatos()
        {
            cliente.Nombres = txtNombre.Text;
            cliente.Apellidos = txtApellido.Text;
            cliente.Identidad = txtIdentidad.Text;
            cliente.FechaNacimiento = tpFechaNacimiento.SelectedDate.Value;
            cliente.Telefono = txtTelefono.Text;
            cliente.Rtn = txtRtn.Text;
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
        /// Limpia los texBox y el time picker del formulario
        /// </summary>
        private void limpiar()
        {
            txtIdentidad.Text = null;
            txtNombre.Text = null;
            txtApellido.Text = null;
            txtRtn.Text = null;
            txtTelefono.Text = null;
            tpFechaNacimiento.SelectedDate = null;
        }

        /// <summary>
        /// Pone visible o ocultos los botones del formulario al momento de editar
        /// oculta los botones al momento de editar para mostrar los botones de aceptar y cancelar
        /// vuelve a mostrar los botones al momento de aceptar o cancelar la edición
        /// </summary>
        /// <param name="estado"></param>
        private void estadoBotones(Visibility estado)
        {
            btnAgregar.Visibility = estado;
            btnEditar.Visibility = estado;
            btnEliminar.Visibility = estado;
            btnLimpiar.Visibility = estado;
        }

        /// <summary>
        /// Valida el estado de los textBox y del time picker
        /// retorna true si todos los campos estan llenos y false si hay alguno sin llenar
        /// </summary>
        /// <returns></returns>
        private bool validaciones()
        {
            try
            {
                bool estado = false;

          
                if (CadenaSoloEspacios(txtNombre.Text) || CadenaSoloEspacios(txtApellido.Text)
                    || CadenaSoloEspacios(txtIdentidad.Text) || CadenaSoloEspacios(txtRtn.Text)
                    || CadenaSoloEspacios(txtTelefono.Text) || tpFechaNacimiento.SelectedDate == null)
                {
                    estado = false;
                    if ((CadenaSoloEspacios(txtNombre.Text) || CadenaSoloEspacios(txtApellido.Text)))
                    {
                        MessageBox.Show("El nombre o apellido debe tener al menos 2 caracteres y es un campo obligatorio", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        if((CadenaSoloEspacios(txtNombre.Text)))
                        {
                            txtNombre.Focus();
                        }
                        else
                        {
                            txtApellido.Focus();
                        }
                    }
                    else if (CadenaSoloEspacios(txtIdentidad.Text))
                    {
                        MessageBox.Show("La identidad es obligatoria", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);

                        txtIdentidad.Focus();
                    }
                    else if (CadenaSoloEspacios(txtRtn.Text))
                    {
                        MessageBox.Show("El RTN es obligatorio", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        txtRtn.Focus();
                    }
                    else
                    {
                        MessageBox.Show("El teléfono es obligatorio", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        txtTelefono.Focus();
                    }
                }
                else
                {
                    estado = true;
                }
                return estado;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(),"Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
          
        }



        /// <summary>
        /// Metodo para obtener la edad
        /// </summary>
        /// <returns></returns>
        private bool DiferenciaEdad()
        {

            try
            {
                    var timeSpan = DateTime.Now - tpFechaNacimiento.DisplayDate;
                    int Edad = new DateTime(timeSpan.Ticks).Year - 1;

                    if(Edad < 18)
                    {
                        return  true;
                    }
                     return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
           
        }

        /// <summary>
        /// Metodo para validar la entrada de los campos
        /// </summary>
        /// <returns></returns>
        private bool validaciones2()
        {
            try
            {
               bool estado = false;

                    if (txtNombre.Text.Length <= 1 || txtApellido.Text.Length <= 1
                        || txtIdentidad.Text.Length <= 12 || txtRtn.Text.Length <= 13
                        || txtTelefono.Text.Length <= 7 )
                    {
                        if(txtNombre.Text.Length <= 1)
                        {
                            MessageBox.Show("El nombre o apellido debe tener al menos 2 caracteres", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                            txtNombre.Focus();
                        }
                        else if(txtApellido.Text.Length <= 1)
                        {
                            MessageBox.Show("El apellido debe tener al menos 2 caracteres", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                            txtApellido.Focus();

                        }
                        else if (txtIdentidad.Text.Length <= 12)
                        {
                            MessageBox.Show("La identidad debe tener 13 caracteres", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                            txtIdentidad.Focus();
                        }
                        else if(txtRtn.Text.Length <= 13)
                        {
                            MessageBox.Show("El RTN debe tener 14 caracteres", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                            txtRtn.Focus();
                        }
                         else if (!VerificarNumeroTelefono(txtTelefono.Text))
                        {
                              MessageBox.Show("Ingrese un número de teléfono válido!", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                               txtTelefono.Focus();
                        }
                    else
                        {
                            MessageBox.Show("El teléfono debe tener 8 caracteres", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                            txtTelefono.Focus();
                        }

                    }  
                    else
                    {
            
                        if (DiferenciaEdad())
                        {
                            estado = false;
                            MessageBox.Show("Tiene que ser mayor de edad o ingresar una fecha válida", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                            estado = true;
                    }





                    return estado;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(),"Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }

          
        }
        /// <summary>
        /// Carga el listBox de empleados
        /// </summary>
        private void cargarTabla()
        {
            try
            {
                //Query de consulta
                string query = "Select * from [Ventas].[Cliente] Where cod_estado = 1";

                // SqlDataAdapter es una interfaz entre las tablas de la base de datos
                // y los objetos utilizables en C#
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);

                //Utiliza el sqlDataAdapter
                using (sqlDataAdapter)
                {
                    // Objeto de C# que refleje la estructura de una tabla
                    DataTable tablaCliente = new DataTable();

                    // LLenar el objeto de tipo DataTable con los valores proveniente del SqlDataAdapter
                    sqlDataAdapter.Fill(tablaCliente);

                    // ¿Cuál es la información de la tabla (DataTable) que debería desplegarse al usuario?
                    lbClientes.DisplayMemberPath = "nombres";

                    // ¿Qué valor debe ser entregado cuando un elemento del ListBox es seleccionado?
                    lbClientes.SelectedValuePath = "codigo_cliente";

                    // ¿Quién es la referencia para los datos del ListBox? (populate)
                    lbClientes.ItemsSource = tablaCliente.DefaultView;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error en la base de datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Manda la tarea de ingresar un cliente a la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                    if (validaciones())
                    {
                        if(validaciones2())
                        {
                            if (VerificarIdentidad(txtIdentidad.Text))
                            {
                                if(VerificarRTN(txtRtn.Text))
                                {
                                    guardarDatos();
                                    cliente.AgregarCliente();
                                    cargarTabla();
                                    limpiar();
                                }
                                else
                                {
                                    MessageBox.Show("Por favor, verifique los datos.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    txtRtn.Focus();
                                }
                           

                            }
                            else
                            {
                       
                                    MessageBox.Show("Por favor, verifique los datos.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    txtIdentidad.Focus();
                        
                            }
                        }
                       else
                        {
                            MessageBox.Show("Por favor, verifique los datos.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Por favor, llene todos los campos.","Advertencia",MessageBoxButton.OK,MessageBoxImage.Warning);
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(),"Error",MessageBoxButton.OK,MessageBoxImage.Error);
               
            }

        }


        /// <summary>
        /// Metodo para validar si la cadena de texto solo contiene espacios
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
                return false;
            }
         
        }

        /// <summary>
        /// Metodo para validar numeros enteros
        /// </summary>
        /// <param name="valor">valor que enviamos a verificar</param>
        /// <param name="li">limite inferior</param>
        /// <param name="ls">limite superior</param>
        private bool NumerosEnteros(int valor,int li,int ls)
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
    /// Metodo para validar numeros enteros
    /// </summary>
    /// <param name="valor">valor que enviamos a verificar</param>
    /// <param name="li">limite inferior</param>
    /// <param name="ls">limite superior</param>
    /// <returns></returns>
    private bool NumerosEnteros2(int valor, int li, int ls)
        {

            try
            {
             if (valor < li || valor > ls)
              {
                MessageBox.Show(string.Concat("Los siguientes dos dígitos: ", valor, " del municipio.", "\nSolo se permiten números dentro del rango: ", li, " y ", ls, "."), "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
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
        /// Declaracion de Diccionario
        /// </summary>
        public Dictionary<int, string> Departamentos = new Dictionary<int, string>(); 

        /// <summary>
        /// Metodo para agregar los municipios al diccionario por cada departamento
        /// </summary>
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
        /// Metodo para buscar el total de municipios por departamento
        /// </summary>
        /// <param name="x">numero de Departamento</param>
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


        private int Ls;
        private int Li;
        /// <summary>
        /// Metodo para verificar la identidad
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
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

                            if (NumerosEnteros(folio, 1, 99999))
                            {
                                resultado = true;
                            }
                            else
                            {
                                MessageBox.Show("El folio debe estar en un rango del 00001-99999", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                                txtIdentidad.Focus();
                            }

                        }
                        else
                        {
                            MessageBox.Show("El año debe estar en un rango del 1900-2100", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                            txtIdentidad.Focus();
                        }
                    }
                    else
                    {

                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Los primeros dos números de la identidad \ndeben estar en un rango del 1-18.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtIdentidad.Focus();
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
        /// Metodo para verificar el Rtn
        /// </summary>
        /// <param name="cadena">el valor de Rtn</param>
        /// <returns></returns>
        private bool VerificarRTN(string cadena)
        {
            try
            {
                bool resultado = false;
                int depto = int.Parse(cadena.Substring(0, 2));
                int muni = int.Parse(cadena.Substring(2, 2));
                int año = int.Parse(cadena.Substring(4, 4));
                int folio = int.Parse(cadena.Substring(8, 6));

                AggDatosDiccionario();


                if (NumerosEnteros(depto, 1, 18))
                {

                    bool Est = BuscarDiccionario(depto);
                    if (NumerosEnteros2(muni, Li, Ls))
                    {
                        if (NumerosEnteros(año, 1900, 2100))
                        {

                            if (NumerosEnteros(folio, 1, 999999))
                            {
                                resultado = true;
                            }
                            else
                            {
                                MessageBox.Show("El folio debe estar en un rango del 00001-99999", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                                txtRtn.Focus();
                            }

                        }
                        else
                        {
                            MessageBox.Show("El año debe estar en un rango del 1900-2100", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                            txtRtn.Focus();
                        }
                    }
                    else
                    {

                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("1. Los primeros dos números del RTN. \n2.Deben estar en un rango de 1-18.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtRtn.Focus();
                    return false;
                }

                return resultado;
            }
            catch ( Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            } 
        }

        /// <summary>
        /// Consulta el cliente seleccionado para su edicion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if(lbClientes.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un cliente","Advertencia",MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                cliente.consultarCliente(Convert.ToInt32(lbClientes.SelectedValue));

                txtIdentidad.Text = cliente.Identidad;
                txtNombre.Text = cliente.Nombres;
                txtApellido.Text = cliente.Apellidos;
                txtTelefono.Text = cliente.Telefono;
                txtRtn.Text = cliente.Rtn;
                tpFechaNacimiento.SelectedDate = cliente.FechaNacimiento;

                estadoBotones(Visibility.Hidden);
            }
        }



        /// <summary>
        /// Solo Permite el ingreso de Letras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidacionLetras(object sender, KeyEventArgs e)
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


        /// <summary>
        /// Cancela la edicion y devuelve el formulario a su estado original
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            estadoBotones(Visibility.Visible);
        }

        /// <summary>
        /// Limpia los campos del formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        /// <summary>
        /// Ejecuta la edicion del cliente en la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                        if (validaciones())
                        {
                            if(validaciones2())
                            {
                                if(VerificarIdentidad(txtIdentidad.Text))
                                {
                                   if(VerificarRTN(txtRtn.Text))
                                    {
                                        guardarDatos();
                                        cliente.editarCliente();
                                        estadoBotones(Visibility.Visible);
                                        limpiar();
                                        cargarTabla();
                                    }
                                   else
                                        MessageBox.Show("Por favor! Verificar su RTN.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                                else
                                    MessageBox.Show("Por favor! Verificar su numero de identidad.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else
                                MessageBox.Show("Por favor! Verificar todos los campos.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);

                        }
                        else
                        {
                            MessageBox.Show("Por favor! Llene todos los campos.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message.ToString());
            }
          
        }

        /// <summary>
        /// Elimina (desactiva) un cliente en la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                 if (lbClientes.SelectedValue == null)
                 {
                     MessageBox.Show("Seleccione un cliente a eliminar", "Aviso",MessageBoxButton.OK, MessageBoxImage.Warning);
                 }
                 else
                 {
                     if(MessageBox.Show("¿Seguro que quieres eliminar el cliente?","Eliminar cliente",MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                     {
                         cliente.eliminarCliente(Convert.ToInt32(lbClientes.SelectedValue));
                         cargarTabla();
                     }
                 }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message.ToString());
            }
           
        }

    /// <summary>
    /// Se activa la fecha de nacimiento
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

        private void tpFechaNacimiento_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}

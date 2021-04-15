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
            bool estado = false;

            if (txtNombre.Text.Equals("") || txtApellido.Text.Equals("")
                || txtIdentidad.Text.Equals("") || txtRtn.Text.Equals("")
                || txtTelefono.Text.Equals("") || tpFechaNacimiento.SelectedDate == null)
                estado = false;
            else
                estado = true;

            return estado;
        }

        /// <summary>
        /// Carga el listBox de empleados
        /// </summary>
        private void cargarTabla()
        {
            try
            {
                //Query de consulta
                string query = "Select * from [Ventas].[Cliente] Where estado = 1";

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
            catch (Exception e)
            {
                MessageBox.Show("Error en la base de datos");
            }
        }

        /// <summary>
        /// Manda la tarea de ingresar un cliente a la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (validaciones())
            {
                guardarDatos();
                cliente.AgregarCliente();
                cargarTabla();
                limpiar();
            }
            else
            {
                MessageBox.Show("Por favor, llene todos los campos.");
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
                MessageBox.Show("Seleccione un cliente");
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
            if (validaciones())
            {
                guardarDatos();
                cliente.editarCliente();
                estadoBotones(Visibility.Visible);
                limpiar();
                cargarTabla();
            }
            else
            {
                MessageBox.Show("Por favor! Llene todos los campos.");
            }
        }

        /// <summary>
        /// Elimina (desactiva) un cliente en la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (lbClientes.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un cliente a eliminar");
            }
            else
            {
                if(MessageBox.Show("¿Seguro que quieres eliminar el cliente?","Eliminar cliente",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    cliente.eliminarCliente(Convert.ToInt32(lbClientes.SelectedValue));
                    cargarTabla();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

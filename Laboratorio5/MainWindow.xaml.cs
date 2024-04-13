using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Laboratorio5
{
    public partial class MainWindow : Window
    {
        public string connectionString = "Data Source=LAB1504-25\\SQLEXPRESS;Initial Catalog=LAB05;User ID=UserTec;Password=123456";
        public List<Clientes> ListaClientes { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ListaClientes = new List<Clientes>(); 
            dataClientes.ItemsSource = ListaClientes;
        }

        private void Button_Insertar(object sender, RoutedEventArgs e)
        {
            string idCliente = txtIdCliente.Text;
            string nombreCompañia = txtNombreCompania.Text;
            string nombreContacto = txtNombreContacto.Text;
            string cargoContacto = txtCargoContacto.Text;
            string direccion = txtDireccion.Text;
            string ciudad = txtCiudad.Text;
            string pais = txtPais.Text;
            string telefono = txtTelefono.Text;

            Clientes nuevoCliente = new Clientes(idCliente, nombreCompañia, nombreContacto, cargoContacto, direccion, ciudad, pais, telefono);
            ListaClientes.Add(nuevoCliente);
            dataClientes.Items.Refresh(); 

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("USP_InsertarClientes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@idCliente", idCliente);
                        command.Parameters.AddWithValue("@NombreCompañia", nombreCompañia);
                        command.Parameters.AddWithValue("@NombreContacto", nombreContacto);
                        command.Parameters.AddWithValue("@CargoContacto", cargoContacto);
                        command.Parameters.AddWithValue("@Direccion", direccion);
                        command.Parameters.AddWithValue("@Ciudad", ciudad);
                        command.Parameters.AddWithValue("@Pais", pais);
                        command.Parameters.AddWithValue("@Telefono", telefono);

                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show("Cliente registrado correctamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar cliente: " + ex.Message);
            }
        }
    }

    public class Clientes
    {
        public string idCliente { get; set; }
        public string NombreCompañia { get; set; }
        public string NombreContacto { get; set; }
        public string CargoContacto { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public string Telefono { get; set; }

        public Clientes(string idCliente, string nombreCompañia, string nombreContacto, string cargoContacto, string direccion, string ciudad, string pais, string telefono)
        {
            this.idCliente = idCliente;
            NombreCompañia = nombreCompañia;
            NombreContacto = nombreContacto;
            CargoContacto = cargoContacto;
            Direccion = direccion;
            Ciudad = ciudad;
            Pais = pais;
            Telefono = telefono;
        }
    }
}

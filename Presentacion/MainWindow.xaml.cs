using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Entidades;
using Negocio;

namespace Presentacion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ProductoService _service = new ProductoService();

        public MainWindow()
        {
            InitializeComponent();
            CargarProductos();
        }

        private void CargarProductos()
        {
            try
            {
                var lista = _service.Listar();
                dgProductos.ItemsSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtId.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtBuscar.Text = string.Empty;
            dgProductos.SelectedItem = null;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var p = new Producto
                {
                    NombreProducto = txtNombre.Text.Trim(),
                    PrecioUnidad = decimal.TryParse(txtPrecio.Text, out var pr) ? pr : 0m,
                    UnidadesEnExistencia = int.TryParse(txtStock.Text, out var st) ? st : 0
                };
                var newId = _service.Insertar(p);
                MessageBox.Show($"Producto insertado con Id {newId}", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                CargarProductos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DgProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProductos.SelectedItem is Producto p)
            {
                txtId.Text = p.IdProducto.ToString();
                txtNombre.Text = p.NombreProducto;
                txtPrecio.Text = p.PrecioUnidad.ToString();
                txtStock.Text = p.UnidadesEnExistencia.ToString();
            }
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(txtId.Text, out var id) || id <= 0)
                {
                    MessageBox.Show("Seleccione un producto válido para actualizar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var p = new Producto
                {
                    IdProducto = id,
                    NombreProducto = txtNombre.Text.Trim(),
                    PrecioUnidad = decimal.TryParse(txtPrecio.Text, out var pr) ? pr : 0m,
                    UnidadesEnExistencia = int.TryParse(txtStock.Text, out var st) ? st : 0
                };
                _service.Actualizar(p);
                MessageBox.Show("Producto actualizado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                CargarProductos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(txtId.Text, out var id) || id <= 0)
                {
                    MessageBox.Show("Seleccione un producto válido para eliminar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var resp = MessageBox.Show("¿Confirma la eliminación?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resp != MessageBoxResult.Yes) return;
                _service.Eliminar(id);
                MessageBox.Show("Producto eliminado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                CargarProductos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var nombre = txtBuscar.Text.Trim();
                var lista = _service.Buscar(nombre);
                dgProductos.ItemsSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            CargarProductos();
        }
    }
}

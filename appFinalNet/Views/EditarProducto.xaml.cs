using appFinalNet.Modelos;
using appFinalNet.Repositories;

namespace appFinalNet.Views;

public partial class EditarProducto : ContentPage
{
    RepositoryProducto repositoryProducto = new RepositoryProducto();
    public Producto ProductoSeleccionado { get; }

	public EditarProducto()
	{
		InitializeComponent();
	}

    public EditarProducto(Producto productoSeleccionado)
    {
        InitializeComponent();
        ProductoSeleccionado = productoSeleccionado;
        CargarDatosEnPantalla();
    }

    private void CargarDatosEnPantalla()
    {
        txtNombre.Text = ProductoSeleccionado.nombre;
        txtDescripcion.Text = ProductoSeleccionado.descripcion;
        txtPrecio.Text = ProductoSeleccionado.precio.ToString();
        txtImagenUrl.Text = ProductoSeleccionado.imagen_url;
    }

    private async void GuardarBtn_Clicked(object sender, EventArgs e)
    {
        Producto nuevoProducto = new Producto()
        {
            _id = ProductoSeleccionado._id,
            nombre = txtNombre.Text,
            descripcion = txtDescripcion.Text,
            precio = Convert.ToInt32(txtPrecio.Text),
            imagen_url = txtImagenUrl.Text,
        };

        //enviamos por Post el objeto que creamos a la URL de la API
        var appFinal = await repositoryProducto.UpdateAsync(nuevoProducto);

        if (appFinal)
        {
            await DisplayAlert("Notificación", "Producto Guardado", "OK");
            await Navigation.PopAsync();
        }

    }

    private async void CancelarBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
using appFinalNet.Modelos;
using appFinalNet.Repositories;

namespace appFinalNet.Views;

public partial class AgregarProducto : ContentPage
{
    RepositoryProducto repositoryProducto = new RepositoryProducto();
	public AgregarProducto()
	{
		InitializeComponent();
	}

    private async void GuardarBtn_Clicked(object sender, EventArgs e)
    {
        Producto nuevoProducto = new Producto()
        {
            nombre = txtNombre.Text,
            descripcion = txtDescripcion.Text,
            precio = Convert.ToInt32(txtPrecio.Text),
            imagen_url = txtImagenUrl.Text,
        };

        //Enviamos por POST el objeto que creamos a la Url de la API
        var appfinal = await repositoryProducto.AddAsync(nuevoProducto);

        if(appfinal)
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
using appFinalNet.Modelos;
using appFinalNet.Repositories;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace appFinalNet.Views;

public partial class InicioApp : ContentPage
{
    public ObservableCollection<Producto> Productos { get; set; }
    public Producto ProductoSeleccionado { get; set; }
    RepositoryProducto repositoryProducto = new RepositoryProducto();

	public InicioApp()
	{
		InitializeComponent();
        Productos = new ObservableCollection<Producto>();
        ProductosCollectionView.SelectionChanged += SeleccionarProducto;
	}

    private void SeleccionarProducto(object sender, SelectionChangedEventArgs e)
    {
        if (ProductosCollectionView.SelectedItems != null)
        {
            ProductoSeleccionado = (Producto)ProductosCollectionView.SelectedItem;
        }
    }
     
    public async void GetAllProductos(object sender, EventArgs e)
    {
        Productos = await repositoryProducto.GetAllAsync();
        ProductosCollectionView.ItemsSource = Productos;
    }

    private void SeleccionarProductoEnCollectionView()
    {
        foreach (var producto in Productos)
        {
            if (producto._id == ProductoSeleccionado._id)
            {
                ProductosCollectionView.SelectedItem = producto;
                break;
            }
        }
    }

    protected override void OnAppearing()
    {
        NetworkAccess conexionInternet = Connectivity.Current.NetworkAccess;
        if (conexionInternet == NetworkAccess.Internet)
        {
            GetAllProductos(this, EventArgs.Empty);
        }
    }

    protected override bool OnBackButtonPressed()
    {
        Debug.Print(">>>>>>>>>>>>>>>>>>>>>> se ha pulsado el botón atras");
        return false;
    }

    protected override void OnDisappearing()
    {
        Debug.Print(">>>>>>>>>>>>>>>>>>>>>> se ha cerrado la ventana de la lista");
    }
    private async void AgregarProducto_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AgregarProducto());
    }

    private async void EdigarProducto_Clicked(object sender, EventArgs e)
    {
        if (ProductoSeleccionado != null)
        {
            await Navigation.PushAsync(new EditarProducto(ProductoSeleccionado));
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Editar", "Error: Debe seleccionar un Producto a editar", "ok");
        }
    }

    private async void EliminarProducto_Clicked(object sender, EventArgs e)
    {
        if (ProductoSeleccionado != null)
        {
            ProductoSeleccionado = (Producto)ProductosCollectionView.SelectedItem;
            bool respuesta = await Application.Current.MainPage.DisplayAlert("Eliminar", $"¿Está seguro que desea Eliminar el producto: {ProductoSeleccionado.nombre}?", "Si", "No");
            if(respuesta)
            {
                var eliminado = await repositoryProducto.RemoveAsync(ProductoSeleccionado._id);
                if(eliminado)
                {
                    await Application.Current.MainPage.DisplayAlert("Eliminar", $"Se ha eliminado el producto: {ProductoSeleccionado.nombre} correctamente", "Ok");
                    GetAllProductos(this, EventArgs.Empty);
                }
            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Eliminar", "Error: Debe seleccionar el producto que desea Eliminar", "Ok");
        }
    }
}
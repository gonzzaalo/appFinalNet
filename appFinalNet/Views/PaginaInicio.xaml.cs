namespace appFinalNet.Views;

public partial class PaginaInicio : ContentPage
{
	public PaginaInicio()
	{
		InitializeComponent();
	}

    private async void Productos_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new InicioApp());
    }

}
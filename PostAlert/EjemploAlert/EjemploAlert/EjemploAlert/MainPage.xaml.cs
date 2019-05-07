using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EjemploAlert
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            bool termresult = false;
            String sexresult = null;
            this.register.Clicked += async (sender, args) =>
            {
                this.result.Text = "";
                if(this.name.Text != null && this.surname.Text != null && this.age.Text != null && sexresult != null)
                {
                    if (termresult)
                    {
                        this.result.Text = "Registrado correctamente.";
                    }
                    else
                    {
                        await DisplayAlert("Advertencia", "No ha aceptado los terminos.", "Cerrar");
                    }
                   
                }
                else
                {
                    await DisplayAlert("Advertencia", "Hay campos si rellenar.", "Cerrar");
                }
               
            };
            this.sex.Clicked += async (sender, args) =>
            {
                sexresult = await DisplayActionSheet("Selección de sexo", "Cancelar", null, "Hombre", "Mujer", "Prefiero no contestar");
            };
            this.terms.Clicked += async (sender, args) =>
            {
                termresult = await DisplayAlert("Términos de Uso", "¿Acepta nustra política de uso de datos?", "Si", "No");
            };
            this.info.Clicked += async (sender, args) =>
            {
                Info modal = new Info();
                await Navigation.PushModalAsync(modal);
            };
        }
    }
}

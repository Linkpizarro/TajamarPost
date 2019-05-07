using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EjemploAlert
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Info : ContentPage
	{
		public Info()
		{
			InitializeComponent ();
            this.close.Clicked += async (sender, args) =>
            {
                await Navigation.PopModalAsync();
            };
        }
	}
}
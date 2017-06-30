
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ShoppingCart
{
    [Activity(Label = "PaginaPrincipal", MainLauncher = true)]
    public class PaginaPrincipal : Activity
    {

        Button btnRegistrarProductos;
        Button btnRegistrarComercios;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PaginaPrincipal);
            // Create your application here
            btnRegistrarProductos = FindViewById<Button>(Resource.Id.btnRegistrarProductos);
            btnRegistrarComercios = FindViewById<Button>(Resource.Id.btnRegistrarComercios);

            btnRegistrarProductos.Click += (sender, e) =>
			{

                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
			};

            btnRegistrarComercios.Click += (sender, e) =>
			{

                StartActivity(new Intent(Application.Context, typeof(RegistroComercios)));
			};

		}


		
       
    }
}

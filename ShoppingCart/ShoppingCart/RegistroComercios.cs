
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace ShoppingCart
{
    [Activity(Label = "@string/ApplicationName")]
    public class RegistroComercios : Activity
    {
        HttpClient client = new HttpClient();
        string URLComercio ="http://10.215.152.20/api/Productos";
        string ProductoCompleto;
        static int IDProducto;
        private List<string> datos;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegistroComercios);
            // Create your application here
            BotonesYEventos();
        }

		public void BotonesYEventos()
		{
            var btnGuardarComercio=FindViewById<Button>(Resource.Id.btnGuardarComercio);

            var btnConsultarComercio = FindViewById<Button>(Resource.Id.btnConsultarComercio);
            btnConsultarComercio.Click += btnConsultarComercio_Click;

            var btnLimpiarComercio = FindViewById<Button>(Resource.Id.btnLimpiarComercio);
			btnLimpiarComercio.Click += btnLimpiarComercio_Click;

            var btnEliminarComercio = FindViewById<Button>(Resource.Id.btnEliminarComercio);
			btnLimpiarComercio.Click += btnEliminarComercio_Click;

			datos = new List<string>();
		}

		private async void btnConsultarComercio_Click(object sender, EventArgs e)
		{
			await ObtenerComercios();
		}


		// Get ycrearLista en listview
		public async Task<IEnumerable<Productos>> ObtenerComercios()
		{

            string result = await client.GetStringAsync(URLComercio);
			var Resultado = JsonConvert.DeserializeObject<IEnumerable<Productos>>(result);



			foreach (var Comercio in Resultado)
			{
				ProductoCompleto = "ID:" + Comercio.ID + "Producto: " + Comercio.Nombre;

				ListView();
			}
			return Resultado;
		}

		private void btnLimpiarComercio_Click(object sender, EventArgs e)
		{
            var txtNombre = FindViewById<EditText>(Resource.Id.EditTextNombreComercio);
            var txtNLatitud = FindViewById<EditText>(Resource.Id.EditTextLatitud);
            var txtLongitud = FindViewById<EditText>(Resource.Id.EditTextLongitud);

            txtNombre.Text = "";
            txtLongitud.Text = "";
            txtNLatitud.Text = "";
        }

		private async void btnEliminarComercio_Click(object sender, EventArgs e)
		{
			await Delete();
			var txtNombre = FindViewById<EditText>(Resource.Id.EditTextNombreComercio);
			var txtNLatitud = FindViewById<EditText>(Resource.Id.EditTextLatitud);
			var txtLongitud = FindViewById<EditText>(Resource.Id.EditTextLongitud);


			txtNombre.Text = "";
			txtNLatitud.Text = "";
			txtLongitud.Text = "0";
		}

		public static async Task Delete()
		{

			HttpClient client = new HttpClient();
			var response = await client.DeleteAsync("http://10.215.152.20/api/Productos/" + IDProducto);

			//return JsonConvert.DeserializeObject<Productos>(await response.Content.ReadAsStringAsync());
		}
		public void ListView()
		{
            var lista = FindViewById<ListView>(Resource.Id.LVComercios);

			datos.Add(ProductoCompleto);

			//lo que va a mostrar la vista listview
			lista.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, datos);

			lista.ItemClick += Lista_ItemClick;
		}


		private async void Lista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
            //var lista = FindViewById<ListView>(Resource.Id.LVComercios);
			//string str = lista.GetItemAtPosition(e.Position).ToString();

			//await MostrarProductoEnCampos(str);




		}
    }
}

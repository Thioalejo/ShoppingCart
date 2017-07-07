using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using ShoppingCart.Service;

using System;
using Android.Content.PM;
using System.Net.Http;
using Newtonsoft.Json;
using Android.Content;

using System.Threading.Tasks;
using System.Text;

namespace ShoppingCart
{
    [Activity(Label = "@string/ApplicationName", Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        private List<string> datos;
        //TextView TxtID;
        EditText TxtName;
        EditText TxtCdBarras;
        EditText TxtPrice;
		string URLProductos = "http://10.215.152.29/Compras2/api/Productos";
        static int IDProducto;
		string NOMBRE;
		int CDBARRAS;
		double PRECIO;

		//esto podria ser un servicio en azure o para consumir servicio rest, en este caso seria local
		//ProductService servicio = new ProductService();
		//recordar que no se debe conectar directamente con el model, para eso se usa el view model que hace este proceso
		// por tanto yo desde la vista me comunico con el viewmodel no el modelo y el view model hace el mapeo de datos

		HttpClient client = new HttpClient();
		string ProductoCompleto;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            //ConsumirServicio();
            BotonesYEventos();

        }

        public void BotonesYEventos()
        {
            var btnGuardar = FindViewById<Button>(Resource.Id.btnGuardar);
            btnGuardar.Click += BtnGuardar_Click;

            var btnLimpiar = FindViewById<Button>(Resource.Id.btnLimpiar);
            btnLimpiar.Click += BtnLimpiar_Click;

            var btnEliminar = FindViewById<Button>(Resource.Id.btnEliminar);
            btnEliminar.Click += BtnEliminar_Click;

            var btnModificar = FindViewById<Button>(Resource.Id.btnModificar);
            btnModificar.Click += BtnModificar_Click;

            var btnConsultar = FindViewById<Button>(Resource.Id.btnConsultar);
            btnConsultar.Click += btnConsultar_Click;        
            datos = new List<string>();
        }

        private async void  btnConsultar_Click(object sender, EventArgs e)
        {
            await ObtenerProductos();
        }

		

		// Get ycrearLista en listview
		public async Task<IEnumerable<Productos>> ObtenerProductos()
		{
			
            string result = await client.GetStringAsync("http://10.215.152.29/Compras2/api/Productos");
			var Resultado = JsonConvert.DeserializeObject<IEnumerable<Productos>>(result);



            foreach (var Prodctos in Resultado)
			{
                ProductoCompleto = "ID:" + Prodctos.ID + "Producto: " + Prodctos.Nombre  + " - Precio: $ " + Prodctos.Precio;
				
				ListView();
			}
            return Resultado;
		}




		private async void BtnGuardar_Click(object sender, System.EventArgs e)
		{
			TxtName = FindViewById<EditText>(Resource.Id.EditTextNombreProducto);
			TxtCdBarras = FindViewById<EditText>(Resource.Id.EditTextCdBarras);
			TxtPrice = FindViewById<EditText>(Resource.Id.EditTextPrecioProducto);

			if (TxtName.Text == "" || TxtCdBarras.Text == "" || TxtPrice.Text == "")
			{
				AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
				alertDialogBuilder.SetMessage(Resource.String.MissingErrorConsultadoEnviandoDatosAlservicio);
				alertDialogBuilder.Show();
			}

			else
			{
				NOMBRE = TxtName.Text;
				CDBARRAS = Convert.ToInt32(TxtCdBarras.Text);
				PRECIO = Convert.ToDouble(TxtPrice.Text);
				await Agregar(NOMBRE, CDBARRAS, PRECIO, "Prueba");


		   }

		}		//Para POST Guadar registro
		public static async Task<Productos> Agregar(string nombre, int codigo, double precio, string local)
		{
			Productos producto = new Productos()
			{

				//ID = id,

				Nombre = nombre,
				Codigo = codigo,
				Precio = precio,
				Local = local,
			};

			HttpClient client = new HttpClient();
			var response = await client.PostAsync("http://10.215.152.29/Compras2/api/Productos/", new StringContent(JsonConvert.SerializeObject(producto),
																										 Encoding.UTF8, "application/json"));

			return JsonConvert.DeserializeObject<Productos>(await response.Content.ReadAsStringAsync());
		}



        private async void BtnModificar_Click(object sender, EventArgs e)
        {

			TxtName = FindViewById<EditText>(Resource.Id.EditTextNombreProducto);
			TxtCdBarras = FindViewById<EditText>(Resource.Id.EditTextCdBarras);
			TxtPrice = FindViewById<EditText>(Resource.Id.EditTextPrecioProducto);

			if (TxtName.Text == "" || TxtCdBarras.Text == "" || TxtPrice.Text == "")
			{
				AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
				alertDialogBuilder.SetMessage(Resource.String.MissingErrorConsultadoEnviandoDatosAlservicio);
				alertDialogBuilder.Show();
			}

			else
			{
				NOMBRE = TxtName.Text;
				CDBARRAS = Convert.ToInt32(TxtCdBarras.Text);
				PRECIO = Convert.ToDouble(TxtPrice.Text);
                await Modificar(NOMBRE, CDBARRAS, PRECIO, "Modificado");




			}
        }

       

		public static async Task<Productos> Modificar(string nombre, int codigo, double precio, string local)
		{
            
			Productos producto = new Productos()
			{
                
				ID=IDProducto,
			    Nombre = nombre,
				Codigo = codigo,
				Precio = precio,
				Local = local,
			};

			HttpClient client = new HttpClient();
			var response = await client.PutAsync("http://10.215.152.29/Compras2/api/Productos/"+ IDProducto, new StringContent(JsonConvert.SerializeObject(producto),
																										 Encoding.UTF8, "application/json"));
			Console.WriteLine( "Respuesta MOdificar "+response);
            return JsonConvert.DeserializeObject<Productos>(await response.Content.ReadAsStringAsync());
		}

		private async void BtnEliminar_Click(object sender, System.EventArgs e)
        {
            await Delete();
            //servicio.Eliminar(ConectaConModelo.Id);
            //var TxtID = FindViewById<TextView>(Resource.Id.EditTextViewMostrarIDProducto);
            TxtName = FindViewById<EditText>(Resource.Id.EditTextNombreProducto);
            TxtCdBarras = FindViewById<EditText>(Resource.Id.EditTextCdBarras);
            TxtPrice = FindViewById<EditText>(Resource.Id.EditTextPrecioProducto);


			TxtName.Text = "";
			TxtCdBarras.Text = "";
            TxtPrice.Text = "0";


        }


		public static async Task Delete()
			{

				
					
				HttpClient client = new HttpClient();
				var response = await client.DeleteAsync("http://10.215.152.29/Compras2/api/Productos/" + IDProducto);

				Console.WriteLine(response);
				//return JsonConvert.DeserializeObject<Productos>(await response.Content.ReadAsStringAsync());
			}


        private  void BtnLimpiar_Click(object sender, System.EventArgs e)
        {
            //ConsumirServicio();


           
            TxtName.Text = "";
            TxtCdBarras.Text = "";
            TxtPrice.Text = "0";
		}


        public async Task<IEnumerable<Productos>> MostrarProductoEnCampos(string Cadena)
		{
			TxtName = FindViewById<EditText>(Resource.Id.EditTextNombreProducto);
			TxtCdBarras = FindViewById<EditText>(Resource.Id.EditTextCdBarras);
			TxtPrice = FindViewById<EditText>(Resource.Id.EditTextPrecioProducto);
			string result = await client.GetStringAsync(URLProductos);
			var Resultado = JsonConvert.DeserializeObject<IEnumerable<Productos>>(result);




			foreach (var Prodctos in Resultado)
			{
                if (Cadena.Contains(Prodctos.ID.ToString()))
				{
                    IDProducto = Prodctos.ID;
					TxtName.Text = Prodctos.Nombre;
                    TxtCdBarras.Text = Convert.ToString(Prodctos.Codigo);
                    TxtPrice.Text = Convert.ToString(Prodctos.Precio);
					Prodctos.Local = "Defecto";
					//ProductoCompleto = "Producto: " + Prodctos.Nombre + " - Precio: $ " + Prodctos.Precio;
					return Resultado;
				}
				//ListView();
			}
			return Resultado;
		}
        //Productos productoenbase = new Productos();
     
        private async void Lista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var lista = FindViewById<ListView>(Resource.Id.LVProducts);
            string str = lista.GetItemAtPosition(e.Position).ToString();

            await MostrarProductoEnCampos(str);




        }
        public void ListView()
        {
            var lista = FindViewById<ListView>(Resource.Id.LVProducts);



            datos.Add(ProductoCompleto);
            //datos.Add(Convert.ToString(apoyoObjeto.apoyosServicios));
            //datos.Add(ProductoCompleto);

            //lo que va a mostrar la vista listview
            lista.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, datos);

            lista.ItemClick += Lista_ItemClick;
        }
    }
}

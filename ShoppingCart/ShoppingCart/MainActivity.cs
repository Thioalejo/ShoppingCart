using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using ShoppingCart.Service;
using ShoppingCart.ViewModel;
using System;
using Android.Content.PM;
using System.Net.Http;
using Newtonsoft.Json;
using Android.Content;
using ShoppingCart.Model;
using System.Threading.Tasks;
using System.Text;

namespace ShoppingCart
{
    [Activity(Label = "@string/ApplicationName", Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        private List<string> datos;
        TextView TxtID;
        TextView TxtName;
        EditText TxtCdBarras;
        TextView TxtPrice;
        //esto podria ser un servicio en azure o para consumir servicio rest, en este caso seria local
        ProductService servicio = new ProductService();
        //recordar que no se debe conectar directamente con el model, para eso se usa el view model que hace este proceso
        // por tanto yo desde la vista me comunico con el viewmodel no el modelo y el view model hace el mapeo de datos
        ProductViewModel ConectaConModelo = new ProductViewModel();
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
           // ConsultarProductos();
            await ObtenerProductos();
        }


		// Get
		public async Task<IEnumerable<Productos>> ObtenerProductos()
		{
			
			string result = await client.GetStringAsync("http://10.215.152.16/api/Productos");
			var Resultado = JsonConvert.DeserializeObject<IEnumerable<Productos>>(result);



            foreach (var Prodctos in Resultado)
			{
                ProductoCompleto = "Producto: " + Prodctos.Nombre  + " - Precio: $ " + Prodctos.Precio;
				
				ListView();
			}
            return Resultado;
		}

		

        //public async void ConsultarProductos()
        //{

           // HttpClient client = new HttpClient();
            //string text = await client.GetStringAsync("");

            //Productos prod = JsonConvert.DeserializeObject<Productos>(text);

		//	try
          //  {
			//	var list = await "http://10.215.152.16/api/Productos".GetRequest<List<Productos>>();

              //  // TODO: Borrar este check
				
				//	foreach (var producto in list)
					//{
                  //      ProductoCompleto = "Producto: " + producto. Nombre + " Precio $ " + producto.Precio;
						//ProductoCompleto = Convert.ToString(user.Precio);∫∫
						//System.Diagnostics.Debug.WriteLine(user.Nombre );
						//System.Diagnostics.Debug.WriteLine(user.Precio);
                      //  ListView();
					//}
					
				
            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine("Se ha generado un error al consultar el servicio" + ex.Message);
			//}

        //}

       



        private void BtnModificar_Click(object sender, EventArgs e)
        {

            //TxtID = FindViewById<EditText>(Resource.Id.EditTextViewMostrarIDProducto);
            TxtName = FindViewById<TextView>(Resource.Id.EditTextNombreProducto);
            TxtCdBarras = FindViewById<EditText>(Resource.Id.EditTextCdBarras);
            TxtPrice = FindViewById<TextView>(Resource.Id.EditTextPrecioProducto);


            ConectaConModelo.Name = TxtName.Text;
            ConectaConModelo.CdBarras = TxtCdBarras.Text;
            ConectaConModelo.Price = Convert.ToDecimal(TxtPrice.Text);


            servicio.Modificar(ConectaConModelo);
        }

        private void BtnEliminar_Click(object sender, System.EventArgs e)
        {
            TxtID = FindViewById<EditText>(Resource.Id.btnEliminar);
            servicio.Eliminar(ConectaConModelo.Id);
            //var TxtID = FindViewById<TextView>(Resource.Id.EditTextViewMostrarIDProducto);
            TxtName = FindViewById<TextView>(Resource.Id.EditTextNombreProducto);
            TxtCdBarras = FindViewById<EditText>(Resource.Id.EditTextCdBarras);
            TxtPrice = FindViewById<TextView>(Resource.Id.EditTextPrecioProducto);


            ConectaConModelo.Id = "";
            ConectaConModelo.Name = TxtName.Text = "";
            ConectaConModelo.CdBarras = TxtCdBarras.Text = "";
            ConectaConModelo.Price = 0;
            TxtPrice.Text = "";

            
        }

        

        private async void BtnLimpiar_Click(object sender, System.EventArgs e)
        {
            //ConsumirServicio();

            TxtName = FindViewById<TextView>(Resource.Id.EditTextNombreProducto);
            TxtCdBarras = FindViewById<EditText>(Resource.Id.EditTextCdBarras);
            TxtPrice = FindViewById<TextView>(Resource.Id.EditTextPrecioProducto);

            TxtID.Text = "";
            TxtName.Text = "";
            TxtCdBarras.Text = "";
            TxtPrice.Text = "0";
		}

        private async void BtnGuardar_Click(object sender, System.EventArgs e)
        {
            //TxtID = FindViewById<TextView>(Resource.Id.TextViewMostrarIDProducto);
            TxtName = FindViewById<TextView>(Resource.Id.EditTextNombreProducto);
            TxtCdBarras = FindViewById<EditText>(Resource.Id.EditTextCdBarras);
            TxtPrice = FindViewById<TextView>(Resource.Id.EditTextPrecioProducto);

           
            if(TxtName.Text == "" || TxtCdBarras.Text == "" || TxtPrice.Text == "")
            {
                AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
                alertDialogBuilder.SetMessage(Resource.String.MissingFieldsError);
                alertDialogBuilder.Show();
            }
            else
            {
                string NOMBRE = TxtName.Text;
                int CDBARRAS = Convert.ToInt32(TxtCdBarras.Text);
                double PRECIO = Convert.ToDouble(TxtPrice.Text);


                await Agregar(NOMBRE,CDBARRAS,PRECIO,"Prueba");
            }
           
            try
            {
                ConectaConModelo.Price = Convert.ToDecimal(TxtPrice.Text);
            }
            catch (Exception)
            {
                AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
                alertDialogBuilder.SetMessage("Debe ingresar un numero valido en el precio del producto");
                alertDialogBuilder.Show();
            }
            
            
        }

        //Para POST Guadar registro
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
			var response = await client.PostAsync("http://10.215.152.16/api/Productos", new StringContent(JsonConvert.SerializeObject(producto),
																										 Encoding.UTF8, "application/json"));

			return JsonConvert.DeserializeObject<Productos>(await response.Content.ReadAsStringAsync());
		}



        Productos productoenbase = new Productos();
     
        private void Lista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
           
            TxtName = FindViewById<TextView>(Resource.Id.EditTextNombreProducto);
            TxtCdBarras = FindViewById<EditText>(Resource.Id.EditTextCdBarras);
            TxtPrice = FindViewById<TextView>(Resource.Id.EditTextPrecioProducto);

            //client.GetStringAsync()
            //TxtID.Text = productoenbase.ID.ToString();
            TxtName.Text = productoenbase.Nombre;
            TxtCdBarras.Text = productoenbase.Codigo.ToString();
            TxtPrice.Text = Convert.ToString(productoenbase.Precio);
           
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

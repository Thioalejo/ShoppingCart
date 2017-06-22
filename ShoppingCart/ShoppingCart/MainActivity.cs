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

namespace ShoppingCart
{
    [Activity(Label = "                 Carrito de Compras", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
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

            datos = new List<string>();
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            // TxtID = FindViewById<TextView>(Resource.Id.btnModificar);

            // TxtID = FindViewById<TextView>(Resource.Id.EditTextViewMostrarIDProducto);
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
            TxtID = FindViewById<TextView>(Resource.Id.btnEliminar);
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

        

        private void BtnLimpiar_Click(object sender, System.EventArgs e)
        {
            ConsumirServicio();
            TxtID = FindViewById<TextView>(Resource.Id.TextViewMostrarIDProducto);
            TxtName = FindViewById<TextView>(Resource.Id.EditTextNombreProducto);
            TxtCdBarras = FindViewById<EditText>(Resource.Id.EditTextCdBarras);
            TxtPrice = FindViewById<TextView>(Resource.Id.EditTextPrecioProducto);

            TxtID.Text = "";
            TxtName.Text = "";
            TxtCdBarras.Text = "";
            TxtPrice.Text = "0";
        }

        private void BtnGuardar_Click(object sender, System.EventArgs e)
        {
            //TxtID = FindViewById<TextView>(Resource.Id.TextViewMostrarIDProducto);
            TxtName = FindViewById<TextView>(Resource.Id.EditTextNombreProducto);
            TxtCdBarras = FindViewById<EditText>(Resource.Id.EditTextCdBarras);
            TxtPrice = FindViewById<TextView>(Resource.Id.EditTextPrecioProducto);

            //ConectaConModelo.Id = TxtID.Text;
            if(TxtName.Text == "" || TxtCdBarras.Text == "" || TxtPrice.Text == "")
            {
                AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
                alertDialogBuilder.SetMessage("Debe llenar todos los campos");
                alertDialogBuilder.Show();
            }
            else
            {
                ConectaConModelo.Name = TxtName.Text;
                ConectaConModelo.CdBarras = TxtCdBarras.Text;
                ConectaConModelo.Price = Convert.ToDecimal(TxtPrice.Text);

                ConectaConModelo.Guardar();


                //puede ir en otro metodo aparte.


                ProductoCompleto = "Producto: " + ConectaConModelo.Name + " Precio: $ " + ConectaConModelo.Price;
                //para crear lista con productos
                ListView();
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

        Apoyo apoyoObjeto;
        public async void ConsumirServicio()
        {
            string url = "http://plataforma.promexico.gob.mx/sys/gateway.aspx?UID=f2ea359b-a03d-456b-a01a-0f8afab41344";
            HttpClient httpClient = new HttpClient();

            //descargar json y deseraliacion y mostrar en el lisview
            string jsonResultado = await httpClient.GetStringAsync(url);

            apoyoObjeto = JsonConvert.DeserializeObject<Apoyo>(jsonResultado);


            //ListView();
        }
        private void Lista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            TxtID = FindViewById<TextView>(Resource.Id.TextViewMostrarIDProducto);
            TxtName = FindViewById<TextView>(Resource.Id.EditTextNombreProducto);
            TxtCdBarras = FindViewById<EditText>(Resource.Id.EditTextCdBarras);
            TxtPrice = FindViewById<TextView>(Resource.Id.EditTextPrecioProducto);


            TxtID.Text = ConectaConModelo.Id;
            TxtName.Text = ConectaConModelo.Name;
            TxtCdBarras.Text = ConectaConModelo.CdBarras;
            TxtPrice.Text = Convert.ToString(ConectaConModelo.Price);
           
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

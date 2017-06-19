using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using ShoppingCart.Service;
using ShoppingCart.ViewModel;
using System;

namespace ShoppingCart
{
    [Activity(Label = "ShoppingCart", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private List<string> datos;

        //esto podria ser un servicio en azure o para consumir servicio rest, en este caso seria local
        ProductService servicio = new ProductService();
        //recordar que no se debe conectar directamente con el model, para eso se usa el view model que hace este proceso
        // por tanto yo desde la vista me comunico con el viewmodel no el modelo y el view model hace el mapeo de datos
        ProductViewModel ConectaConModelo = new ProductViewModel();


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            BotonesYEventos();

        }

        public void BotonesYEventos()
        {
            var btnGuardar = FindViewById<Button>(Resource.Id.btnGuardar);
            btnGuardar.Click += BtnGuardar_Click;

            var btnLimpiar = FindViewById<Button>(Resource.Id.btnLimpiar);
            btnLimpiar.Click += BtnLimpiar_Click;

            var btnMostrar = FindViewById<Button>(Resource.Id.btnMostrar);
            btnMostrar.Click += BtnMostrar_Click;

            var btnEliminar = FindViewById<Button>(Resource.Id.btnEliminar);
            btnEliminar.Click += BtnEliminar_Click;

            var btnModificar = FindViewById<Button>(Resource.Id.btnModificar);
            btnModificar.Click += BtnModificar_Click;
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            var TxtID = FindViewById<TextView>(Resource.Id.btnEliminar);
            
            //var TxtID = FindViewById<TextView>(Resource.Id.EditTextViewMostrarIDProducto);
            var TxtName = FindViewById<TextView>(Resource.Id.EditTextNombreProducto);
            var TxtCdBarras = FindViewById<EditText>(Resource.Id.EditTextCdBarras);
            var TxtPrice = FindViewById<TextView>(Resource.Id.EditTextPrecioProducto);

            
            ConectaConModelo.Name = TxtName.Text;
            ConectaConModelo.CdBarras = TxtCdBarras.Text;
            ConectaConModelo.Price = Convert.ToDouble(TxtPrice.Text);
            

            servicio.Modificar(ConectaConModelo);
        }

        private void BtnEliminar_Click(object sender, System.EventArgs e)
        {
            var TxtID = FindViewById<TextView>(Resource.Id.btnEliminar);
            servicio.Eliminar(ConectaConModelo.Id);
            //var TxtID = FindViewById<TextView>(Resource.Id.EditTextViewMostrarIDProducto);
            var TxtName = FindViewById<TextView>(Resource.Id.EditTextNombreProducto);
            var TxtCdBarras = FindViewById<EditText>(Resource.Id.EditTextCdBarras);
            var TxtPrice = FindViewById<TextView>(Resource.Id.EditTextPrecioProducto);

            ConectaConModelo.Id = "";
            ConectaConModelo.Name = TxtName.Text = "";
            ConectaConModelo.CdBarras = TxtCdBarras.Text = "";
            ConectaConModelo.Price = 0;
            TxtPrice.Text = "";
        }

        private void BtnMostrar_Click(object sender, System.EventArgs e)
        {
            var TxtID = FindViewById<TextView>(Resource.Id.TextViewMostrarIDProducto);
            var TxtName = FindViewById<TextView>(Resource.Id.EditTextNombreProducto);
            var TxtCdBarras = FindViewById<EditText>(Resource.Id.EditTextCdBarras);
            var TxtPrice = FindViewById<TextView>(Resource.Id.EditTextPrecioProducto);

            TxtID.Text = ConectaConModelo.Id;
            TxtName.Text = ConectaConModelo.Name;
            TxtCdBarras.Text = ConectaConModelo.CdBarras;
            TxtPrice.Text = Convert.ToString(ConectaConModelo.Price);
        }

        private void BtnLimpiar_Click(object sender, System.EventArgs e)
        {
            var TxtID = FindViewById<TextView>(Resource.Id.TextViewMostrarIDProducto);
            var TxtName = FindViewById<TextView>(Resource.Id.EditTextNombreProducto);
            var TxtCdBarras = FindViewById<EditText>(Resource.Id.EditTextCdBarras);
            var TxtPrice = FindViewById<TextView>(Resource.Id.EditTextPrecioProducto);

            TxtID.Text = "";
            TxtName.Text = "";
            TxtCdBarras.Text = "";
            TxtPrice.Text = "0";
        }

        private void BtnGuardar_Click(object sender, System.EventArgs e)
        {
            //var TxtID = FindViewById<TextView>(Resource.Id.TextViewMostrarIDProducto);
            var TxtName = FindViewById<TextView>(Resource.Id.EditTextNombreProducto);
            var TxtCdBarras = FindViewById<EditText>(Resource.Id.EditTextCdBarras);
            var TxtPrice = FindViewById<TextView>(Resource.Id.EditTextPrecioProducto);


            //ConectaConModelo.Id = TxtID.Text;

            ConectaConModelo.Name = TxtName.Text;
            ConectaConModelo.CdBarras = TxtCdBarras.Text;
            ConectaConModelo.Price = Convert.ToDouble(TxtPrice.Text);

            

            var lista = FindViewById<ListView>(Resource.Id.LVProducts);

            datos = new List<string>();
            string ProductoCompleto = "Producto: " + ConectaConModelo.Name + "Precio: " + ConectaConModelo.Price;
            datos.Add(ProductoCompleto);

            //lo que va a mostrar la vista listview
            lista.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, datos);
            ConectaConModelo.Guardar();
            lista.ItemClick += Lista_ItemClick;
            //ListaProducts = FindViewById<ListView>(Resource.Id.listView1);

            //itemsList = new List<string>();
            //itemsList.Add(modelo.Name);
            //itemsList.Add(modelo.Price.ToString());


            //ArrayAdapter<string> adaptar = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, itemsList);


            //ListaProducts.Adapter = adaptar;
        }

        private void Lista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            
        }
    }
}

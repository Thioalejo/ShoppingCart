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
using ShoppingCart.Model;
using System.Collections.ObjectModel;
using ShoppingCart.Service;
using System.ComponentModel.Design;
using System.Windows.Input;
using System.Threading.Tasks;



namespace ShoppingCart.ViewModel
{
    public class ProductViewModel:ProductModel
    {
        public ObservableCollection<ProductModel> Products { get; set; }
        

        //esto podria ser un servicio en azure o para consumir servicio rest, en este caso seria local
        ProductService servicio = new ProductService();
        ProductModel modelo;



        
        public ProductViewModel()
        {
            Products = servicio.Consultar();
        }

        //permite que cuando de clic a un boton o tabla o cualquier evento en una vista se ejecute aqui en el viewmodel

         

        public void Guardar()
        {
            IsBusy = true;
            Guid IdProductUni = Guid.NewGuid();
            modelo = new ProductModel()
            {
                Id = IdProductUni.ToString(),
                Name = Name,
                CdBarras = CdBarras,
                Price = Price
            };
            servicio.Guardar(modelo);
            Task.Delay(2000);
            IsBusy = false;
        }


        public void Modificar()
        {
            IsBusy = true;
            ///Guid IdProductUni = Guid.NewGuid();
            modelo = new ProductModel()
            {
                Id = Id,
                Name = Name,
                CdBarras = CdBarras,
                Price = Price
            };
            servicio.Modificar(modelo);
            Task.Delay(2000);
            IsBusy = false;
        }


        public void Eliminar()
        {
            IsBusy = true;
            Guid IdProductUni = Guid.NewGuid();
            //modelo = new ProductModel()
            //{
            //    Id = Convert.ToInt32(IdProductUni),
            //    Name = Name,
            //    CdBarras = CdBarras,
            //    Price = Price
            //};
            servicio.Eliminar(Id);
            Task.Delay(2000);
            IsBusy = false;
        }

        public void Limpiar()
        {
            Id = "";
            Name = "";
            CdBarras = "";
            Price = 0;

        }

       
    }
}
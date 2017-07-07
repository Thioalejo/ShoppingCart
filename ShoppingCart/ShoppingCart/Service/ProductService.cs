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
using System.Collections.ObjectModel;


namespace ShoppingCart.Service
{
    public class ProductService
    {
        public ObservableCollection<ProductModel> products { get; set; }

        public ProductService()
        {
            if(products == null)
            {
                products = new ObservableCollection<ProductModel>();
            }
        }

        public ObservableCollection<ProductModel> Consultar()
        {
            return products;
        }

        public void Guardar(ProductModel modelo)
        {
            products.Add(modelo);
        }

        public void Modificar(ProductModel modelo)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Id == modelo.Id)
                {
                    products[i] = modelo;
                }
            }
        }


        public void Eliminar(string idProduct)
        {
            ProductModel modelo = products.FirstOrDefault(p => p.Id == idProduct);
            products.Remove(modelo);
        }
    }
}
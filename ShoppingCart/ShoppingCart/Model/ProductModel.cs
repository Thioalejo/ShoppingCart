﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShoppingCart.Model
{
   public class ProductModel:INotifyPropertyChanged
    {
    
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string nameProperty = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameProperty));
        }

        private bool isBusy =false;

        public bool IsBusy
        {
            get { return isBusy =false; }
            set { isBusy = value;
                OnPropertyChanged();
            }
        }


        private string id;

        public string Id
        {
            get { return id; }
            set { id = value;
                OnPropertyChanged();

            }
        }


        private string cdbarras;

        public string CdBarras
        {
            //return $"{CdBarras}{Name}{Price}";
            get { return cdbarras; }
            set { cdbarras = value;
                OnPropertyChanged();
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value;
                OnPropertyChanged();
            }
        }

        private decimal price;

        public decimal Price
        {
            get { return price; }
            set { price = value;
                OnPropertyChanged();
            }
        }


        //private string mensaje;

        //public string Mensaje
        //{
        //    get { return mensaje; }
        //    set { mensaje = value;
        //        OnPropertyChanged();
        //    }
        //}


    }
}
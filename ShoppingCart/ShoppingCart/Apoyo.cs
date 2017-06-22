using System;
namespace ShoppingCart
{
	public class Apoyo
	{
		public Apoyosservicio[] apoyosServicios { get; set; }
	}

	public class Apoyosservicio
	{
		public string Tipo { get; set; }
		public string Descripcion { get; set; }
		public string Monto { get; set; }
		public string Limites { get; set; }
	}


}


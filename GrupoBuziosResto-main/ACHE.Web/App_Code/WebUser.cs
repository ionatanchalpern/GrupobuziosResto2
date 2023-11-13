using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACHE.Model {
	public class WebUser {
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public bool EsAdmin { get; set; }
		public string Email { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
		public int ID { get; set; }
		public string TipoUsuario { get; set; }
        public string usuario { get; set; }
        public bool PagoPorHabilitado { get; set; }
        public string TipoLogin { get; set; }

        public WebUser(string usuario, string email, string nombre, string apellido, int ID, bool esAdmin, string tipoUsuario, string telefono, string celular, bool pagoPorHabilitado, string tipoLogin)
        {
			this.Email = email;
			this.Apellido = apellido;
			this.Nombre = nombre;
            this.ID = ID;
			this.EsAdmin = esAdmin;
			this.TipoUsuario = tipoUsuario;
            this.Telefono = telefono;
            this.Celular = celular;
            this.PagoPorHabilitado = pagoPorHabilitado;
            this.TipoLogin = tipoLogin;
		}
	}
}
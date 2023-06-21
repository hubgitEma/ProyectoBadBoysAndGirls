//referenciamos la capa datitos con negocio
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CapaDatitos;
// agregamos la capa datitos 
namespace CapaNegocito
{
    //clase tipo publica
    public class CP_Usuarios
    {
        private CD_Usuarios OCD = new CD_Usuarios();
        // tableUSUS 2
        public DataTable MostrarUsuario() {
            DataTable tabla = new DataTable();
            tabla = OCD.Mostrar();
            return tabla;
        }

        // DE TIPO STRING TODO POR LO COMBOBOXS
        public void InsertarUsuario(string USUARIO_NO, string CI, string NOMBRE, string PATERNO, string MATERNO, string CARGO, string PROFESION, string USUARIO, string PASSWORD) {
            OCD.Insertar(USUARIO_NO, CI, NOMBRE, PATERNO, MATERNO, CARGO, PROFESION, USUARIO, PASSWORD);
        }

        public void EditarUsuario(string USUARIO_NO, string CI, string NOMBRE, string PATERNO, string MATERNO, string CARGO, string PROFESION, string USUARIO, string PASSWORD)
        {
            OCD.EditarP(USUARIO_NO, CI, NOMBRE, PATERNO, MATERNO, CARGO, PROFESION, USUARIO, PASSWORD);
        }

        public void EliminarUsuario(string id) {
            OCD.Eliminar(id);
        }

        public bool VerificaLoggin(string usu, String pass) {
            return OCD.Loggin(usu, pass);
        }

        public DataTable MostrarUsuarioAscendente()
        {
            DataTable tabla = new DataTable();
            tabla = OCD.MostrarAsc();
            return tabla;
        }

        public DataTable MostrarUsuarioDescendente()
        {
            DataTable tabla = new DataTable();
            tabla = OCD.MostrarDesc();
            return tabla;
        }

        public DataTable Busqueda(string ci,string nom,string pat,string mat,string usu)
        {
            DataTable tabla = new DataTable();
            tabla = OCD.Busqueda(ci,nom,pat,mat,usu);
            return tabla;
        }
    }
}

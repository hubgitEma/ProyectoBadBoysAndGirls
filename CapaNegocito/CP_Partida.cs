using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CapaDatitos;
namespace CapaNegocito
{
    public class CP_Partida
    {

        private CD_PARTIDA OCD = new CD_PARTIDA();
        // tableUSUS 2
        public DataTable Mostrar()
        {
            DataTable tabla = new DataTable();
            tabla = OCD.Mostrar();
            return tabla;
        }

        // DE TIPO STRING TODO POR LO COMBOBOXS
        public void Insertar(string nom, string part, string vu, string cof, string depre, string act, string usu)
        {
            OCD.InsertarP(nom, Convert.ToInt32(part), Convert.ToDouble(vu), Convert.ToDouble(cof), depre, act,  usu);
        }

        public void Editar(string nom, string part, string vu, string cof, string depre, string act, string usu, string id)
        {
            OCD.EditarP(nom, Convert.ToInt32(part), Convert.ToDouble(vu), Convert.ToDouble(cof), depre, act, usu, id);
        }

        public void Eliminar(string id)
        {
            OCD.Eliminar(id);
        }


        public DataTable MostrarAscendente()
        {
            DataTable tabla = new DataTable();
            tabla = OCD.MostrarAsc();
            return tabla;
        }

        public DataTable MostrarDescendente()
        {
            DataTable tabla = new DataTable();
            tabla = OCD.MostrarDesc();
            return tabla;
        }

        public DataTable Busqueda(string part, string p, string vu, string dep, string act)
        {
            DataTable tabla = new DataTable();
            tabla = OCD.Busqueda(part, p,  vu, dep,  act);
            return tabla;
        }
    }
}

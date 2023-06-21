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
    class CP_Oficina
    {
        private CD_Oficina OCD = new CD_Oficina();
        // tableUSUS 2
        public DataTable Mostrar()
        {
            DataTable tabla = new DataTable();
            tabla = OCD.Mostrar();
            return tabla;
        }

        // DE TIPO STRING TODO POR LO COMBOBOXS
        public void Insertar(string dt, string co, string nomof, string res, string ubi)
        {
            OCD.InsertarP(dt, Convert.ToInt32(co), nomof, res, ubi);
        }

        public void Editar(string dt, string co, string nomof, string res, string ubi,string id)
        {
            OCD.EditarP(dt, Convert.ToInt32(co), nomof, res, ubi, id);
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

        public DataTable Busqueda(string dep, string codOf, string nomOf, string resp, string ubi)
        {
            DataTable tabla = new DataTable();
            tabla = OCD.Busqueda(dep, codOf, nomOf, resp, ubi);
            return tabla;
        }
    }
}

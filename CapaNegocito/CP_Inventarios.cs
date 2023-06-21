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
    class CP_Inventarios
    {
        private CD_Inventarios OCD = new CD_Inventarios();
        // tableUSUS 2
        public DataTable Mostrar()
        {
            DataTable tabla = new DataTable();
            tabla = OCD.Mostrar();
            return tabla;
        }

        // DE TIPO STRING TODO POR LO COMBOBOXS
        public void Insertar(string aux, string part, string ce, string ca, string des, string es, string esp, string emp, string proc, string fe, string obs)
        {
            OCD.InsertarP(aux, part, ce, ca, des, es, esp, emp, proc, Convert.ToDateTime(fe), obs);
        }

        public void Editar(string aux, string part, string ce, string ca, string des, string es, string esp, string emp, string proc, string fe, string obs, string id)
        {
            OCD.EditarP(aux, part, ce, ca, des, es, esp, emp, proc, Convert.ToDateTime(fe), obs,id);
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

        public DataTable Busqueda(string part, string codEnt, string codAnt, string desc, string est, string espe, string emp, string fech)
        {
            DataTable tabla = new DataTable();
            tabla = OCD.Busqueda( part,  codEnt, codAnt, desc, est, espe, emp,Convert.ToDateTime(fech));
            return tabla;
        }    
}
}

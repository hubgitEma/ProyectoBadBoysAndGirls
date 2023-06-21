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
    class CP_Empleados
    {
        private CD_Empledos OCD = new CD_Empledos();
        // tableUSUS 2
        public DataTable MostrarEmpleados()
        {
            DataTable tabla = new DataTable();
            tabla = OCD.Mostrar();
            return tabla;
        }

        // DE TIPO STRING TODO POR LO COMBOBOXS
        public void InsertarEmpleados(string CI, string EXP, string NOMBRE, string CARGO, string OFICINA, string UNIDAD, string AREA, string CEL, string PROF, string USU)
        {
            OCD.InsertarP(CI, EXP, NOMBRE, CARGO, OFICINA, UNIDAD, AREA, CEL, PROF, USU);
        }

        public void EditarEmpleados(string CI, string EXP, string NOMBRE, string CARGO, string OFICINA, string UNIDAD, string AREA, string CEL, string PROF, string USU,string id)
        {
            OCD.EditarP(CI, EXP, NOMBRE, CARGO, OFICINA, UNIDAD, AREA, CEL, PROF, USU,id);
        }

        public void EliminarEmpleado(string id)
        {
            OCD.Eliminar(id);
        }
        

        public DataTable MostrarEmpleadoAscendente()
        {
            DataTable tabla = new DataTable();
            tabla = OCD.MostrarAsc();
            return tabla;
        }

        public DataTable MostrarEmpleadoDescendente()
        {
            DataTable tabla = new DataTable();
            tabla = OCD.MostrarDesc();
            return tabla;
        }

        public DataTable Busqueda(string ci, string nom, string of, string uni, string usu)
        {
            DataTable tabla = new DataTable();
            tabla = OCD.Busqueda(ci, nom, of, uni, usu);
            return tabla;
        }    
}
}

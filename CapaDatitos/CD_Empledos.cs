using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace CapaDatitos
{
    public class CD_Empledos
    {
        private Conexion conn = new Conexion();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();
        // tableUSUS 1 
        public DataTable Mostrar()
        {
            //transac sql
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM EMPLEADOS";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conn.CerrarConexion();
            return tabla;
        }

        public DataTable MostrarDesc()
        {
            //transac sql
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM EMPLEADOS ORDER BY CONVERT(INT,SUBSTRING(EMP_NO,3,LEN(EMP_NO))) DESC";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conn.CerrarConexion();
            return tabla;
        }

        public DataTable MostrarAsc()
        {
            //transac sql
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM EMPLEADOS ORDER BY CONVERT(INT,SUBSTRING(EMP_NO,3,LEN(EMP_NO))) ASC";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conn.CerrarConexion();
            return tabla;
        }
        public ArrayList MostrarComboBoxPartida()
        {
            //transac sql
            ArrayList cb = new ArrayList();
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM empleados";
            leer = comando.ExecuteReader();
            while (leer.Read())
            {
                cb.Add(leer["EMP_NO"] + "|  " + leer["CI"] + "-." + leer["NOMBRE"]);

            }
            conn.CerrarConexion();
            return cb;
        }

        public string RecuperaComboBoxPartida(string id)
        {
            //transac sql
            string cb = "";
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM empleados WHERE EMP_NO='" + id + "'";
            leer = comando.ExecuteReader();
            if (leer.Read())
            {
                cb = leer["EMP_NO"] + "|  " + leer["CI"] + "-." + leer["NOMBRE"];

            }
            conn.CerrarConexion();
            return cb;
        }



        public DataTable Busqueda(string ci,string nom)
        {
            //transac sql
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM EMPLEADOS WHERE CI LIKE '"+ci+"' OR NOMBRE LIKE'"+nom+"' ";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conn.CerrarConexion();
            return tabla;
        }
                

        public void InsertarP(string CI, string EXP, string NOMBRE, string CARGO, string OFICINA, string UNIDAD, string AREA, string CEL,string PROF,string USU)
        {
            // PARA EL PROCEDIMIENTO
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "INSERT_EMPLEADOS";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@CI", CI);
            comando.Parameters.AddWithValue("@EXP", EXP);
            comando.Parameters.AddWithValue("@NOMBRE", NOMBRE);
            comando.Parameters.AddWithValue("@CARGO", CARGO);
            comando.Parameters.AddWithValue("@OFICINA", OFICINA);
            comando.Parameters.AddWithValue("@UNIDAD", UNIDAD);
            comando.Parameters.AddWithValue("@AREA_TRABAJO", AREA);
            comando.Parameters.AddWithValue("@CELULAR", CEL);
            comando.Parameters.AddWithValue("@PROFESION", PROF);
            comando.Parameters.AddWithValue("@USUARIO", USU);
            // Y ASI SUCESIVAMENTE
            comando.ExecuteNonQuery();
            //buffer
            comando.Parameters.Clear();
        }

        public void EditarP(string CI, string EXP, string NOMBRE, string CARGO, string OFICINA, string UNIDAD, string AREA, string CEL, string PROF, string USU,String id)
        {
            // PARA EL PROCEDIMIENTO
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "UPDATE_EMPLEADOS";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@CI", CI);
            comando.Parameters.AddWithValue("@EXP", EXP);
            comando.Parameters.AddWithValue("@NOMBRE", NOMBRE);
            comando.Parameters.AddWithValue("@CARGO", CARGO);
            comando.Parameters.AddWithValue("@OFICINA", OFICINA);
            comando.Parameters.AddWithValue("@UNIDAD", UNIDAD);
            comando.Parameters.AddWithValue("@AREA_TRABAJO", AREA);
            comando.Parameters.AddWithValue("@CELULAR", CEL);
            comando.Parameters.AddWithValue("@PROFESION", PROF);
            comando.Parameters.AddWithValue("@USUARIO", USU);
            comando.Parameters.AddWithValue("@id", id);
            comando.ExecuteNonQuery();
            // buffer
            comando.Parameters.Clear();
        }

        public void Eliminar(string id)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "DELETE FROM EMPLEADOS WHERE EMP_NO='" + id + "'";
            comando.CommandType = CommandType.Text;
            comando.ExecuteNonQuery();            
        }
        
    }
}

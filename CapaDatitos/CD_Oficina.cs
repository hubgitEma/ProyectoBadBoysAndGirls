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
    public class CD_Oficina
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
            comando.CommandText = "SELECT * FROM OFICINA";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conn.CerrarConexion();
            return tabla;
        }

        public ArrayList MostrarComboBoxOficina()
        {
            //transac sql
            ArrayList cb = new ArrayList();
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM OFICINA";
            leer = comando.ExecuteReader();
            while (leer.Read())
            {
                cb.Add(leer["OFICINA_NO"] +"|  "+leer["COD_OFICINA"] + " " + leer["DEPTO"] + " " + leer["NOMBRE_OF"] );

            }
            conn.CerrarConexion();
            return cb;
        }
        public string RecuperaComboBoxOficina(string ID)
        {
            //transac sql
            string cboo="";
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM OFICINA WHERE OFICINA_NO='"+ID+"' ";
            leer = comando.ExecuteReader();
            if (leer.Read())
            {
                cboo = leer["OFICINA_NO"] + "|  " + leer["COD_OFICINA"] + " " + leer["DEPTO"] + " " + leer["NOMBRE_OF"];

            }
            conn.CerrarConexion();
            return cboo;
        }
        public DataTable MostrarDesc()
        {
            //transac sql
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM OFICINA ORDER BY CONVERT(INT,SUBSTRING(OFICINA_NO,4,LEN(OFICINA_NO))) DESC";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conn.CerrarConexion();
            return tabla;
        }

        public DataTable MostrarAsc()
        {
            //transac sql
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM OFICINA ORDER BY CONVERT(INT,SUBSTRING(OFICINA_NO,4,LEN(OFICINA_NO))) ASC";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conn.CerrarConexion();
            return tabla;
        }

        public DataTable Busqueda(string dep, string codOf, string nomOf, string resp, string ubi)
        {
            //transac sql
            comando.Connection = conn.AbrirConexion();
            if (!resp.Equals("")) {
                resp = "%" + resp + "%";
            }
            if (!ubi.Equals(""))
            {
                ubi = "%" + ubi + "%";
            }
            comando.CommandText = "SELECT * FROM OFICINA WHERE DEPTO LIKE '"+dep+"' OR COD_OFICINA LIKE '"+codOf+"' OR NOMBRE_OF LIKE '"+nomOf+"' OR RESPONSABLE LIKE '"+ resp + "' OR UBICACION LIKE '"+ubi+"'";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conn.CerrarConexion();
            return tabla;
        }


        public void InsertarP(string dt, int co, string nomof, string res, string ubi)
        {
            // PARA EL PROCEDIMIENTO
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "INSERT_OFICINA";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@DEPTO", dt);
            comando.Parameters.AddWithValue("@COD_OFICINA", co);
            comando.Parameters.AddWithValue("@NOMBRE_OF", nomof);
            comando.Parameters.AddWithValue("@RESPONSABLE", res);
            comando.Parameters.AddWithValue("@UBICACION", ubi); 
            // Y ASI SUCESIVAMENTE
            comando.ExecuteNonQuery();
            //buffer
            comando.Parameters.Clear();
        }

        public void EditarP(string dt, int co, string nomof, string res, string ubi, String id)
        {
            // PARA EL PROCEDIMIENTO
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "UPDATE_OFICINA";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@DEPTO", dt);
            comando.Parameters.AddWithValue("@COD_OFICINA", co);
            comando.Parameters.AddWithValue("@NOMBRE_OF", nomof);
            comando.Parameters.AddWithValue("@RESPONSABLE", res);
            comando.Parameters.AddWithValue("@UBICACION", ubi);
            comando.Parameters.AddWithValue("@id", id);
            comando.ExecuteNonQuery();
            // buffer
            comando.Parameters.Clear();
        }

        public void Eliminar(string id)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "DELETE FROM OFICINA WHERE OFICINA_NO='" + id + "'";
            comando.CommandType = CommandType.Text;
            comando.ExecuteNonQuery();
        }
    }
}

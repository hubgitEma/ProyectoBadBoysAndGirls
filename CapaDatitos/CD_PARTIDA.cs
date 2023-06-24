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
    public class CD_PARTIDA
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
            comando.CommandText = "SELECT * FROM PARTIDA";
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
            comando.CommandText = "SELECT * FROM PARTIDA";
            leer = comando.ExecuteReader();
            while (leer.Read())
            {
                cb.Add(leer["PARTIDA_NO"] + "|  " + leer["NOMBRE"] + "-." + leer["PARTIDA"]);

            }
            conn.CerrarConexion();
            return cb;
        }

        public string RecuperaComboBoxPartida(string id)
        {
            //transac sql
            string cb = "";
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM PARTIDA WHERE PARTIDA_NO='" + id + "'";
            leer = comando.ExecuteReader();
            if (leer.Read())
            {
                cb=leer["PARTIDA_NO"] + "|  " + leer["NOMBRE"] + "-." + leer["PARTIDA"] ;

            }
            conn.CerrarConexion();
            return cb;
        }


        public DataTable MostrarDesc()
        {
            //transac sql
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM PARTIDA ORDER BY CONVERT(INT,SUBSTRING(PARTIDA_NO,6,LEN(PARTIDA_NO))) DESC";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conn.CerrarConexion();
            return tabla;
        }

        public DataTable MostrarAsc()
        {
            //transac sql
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM PARTIDA ORDER BY CONVERT(INT,SUBSTRING(PARTIDA_NO,6,LEN(PARTIDA_NO))) ASC";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conn.CerrarConexion();
            return tabla;
        }

        public DataTable Busqueda(string part, string p, string vu, string dep, string act)
        {
            //transac sql
            comando.Connection = conn.AbrirConexion();
            if (!part.Equals(""))
            {
                part = "%" + part + "%";
            }            
            comando.CommandText = "SELECT * FROM PARTIDA WHERE NOMBRE LIKE '"+part+"' OR PARTIDA LIKE '"+p+"' OR VIDA_UTIL LIKE '"+vu+"' OR DEPRESACION LIKE '"+dep+"' OR ACTUALIZA LIKE '"+act+"'";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conn.CerrarConexion();
            return tabla;
        }


        public void InsertarP(string nom, int part, float vu, float cof, string depre, string act, string usu)
        {
            // PARA EL PROCEDIMIENTO
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "INSERT_PARTIDA";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@NOMBRE", nom);
            comando.Parameters.AddWithValue("@PARTIDA", part);
            comando.Parameters.AddWithValue("@VIDA_UTIL", vu);
            comando.Parameters.AddWithValue("@COEFICIENTE", cof);
            comando.Parameters.AddWithValue("@DEPRESACION", depre);
            comando.Parameters.AddWithValue("@ACTUALIZA", act);
            comando.Parameters.AddWithValue("@USUARIO", usu);        
            // Y ASI SUCESIVAMENTE
            comando.ExecuteNonQuery();
            //buffer
            comando.Parameters.Clear();
        }

        public void EditarP(string nom, int part, double vu, double cof, string depre, string act, string usu, String id)
        {
            // PARA EL PROCEDIMIENTO
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "UPDATE_PARTIDA";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@NOMBRE", nom);
            comando.Parameters.AddWithValue("@PARTIDA", part);
            comando.Parameters.AddWithValue("@VIDA_UTIL", vu);
            comando.Parameters.AddWithValue("@COEFICIENTE", cof);
            comando.Parameters.AddWithValue("@@DEPRESACION", depre);
            comando.Parameters.AddWithValue("@ACTUALIZA", act);
            comando.Parameters.AddWithValue("@USUARIO", usu);
            comando.Parameters.AddWithValue("@id", id);
            comando.ExecuteNonQuery();
            // buffer
            comando.Parameters.Clear();
        }

        public void Eliminar(string id)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "DELETE FROM PARTIDA WHERE PARTIDA_NO='" + id + "'";
            comando.CommandType = CommandType.Text;
            comando.ExecuteNonQuery();
        }
    }
}

// biblioteca de clases
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
// prurba
using System.Collections;

namespace CapaDatitos
{
    public class CD_Usuarios
    {
        private Conexion conn = new Conexion();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();
        // tableUSUS 1 
        public DataTable Mostrar() {
            //transac sql
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM USUARIOS";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conn.CerrarConexion();
            return tabla;
        }

        public ArrayList MostrarComboBoxUsuario()
        {
            //transac sql
            ArrayList cb = new ArrayList();
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM USUARIOS";
            leer = comando.ExecuteReader();
            while (leer.Read())
            {
                cb.Add(leer["USUARIO_NO"] +"|  "+leer["CI"] + "-." + leer["NOMBRE"] + " " + leer["PATERNO"] + " " + leer["MATERNO"]);
             
            }           
            conn.CerrarConexion();
            return cb;
        }

        public string RecuperaComboBoxUsuario(string id)
        {
            //transac sql
            string cb="";
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM USUARIOS WHERE USUARIO_NO='"+id+"'";
            leer = comando.ExecuteReader();
            if (leer.Read())
            {
                cb = leer["USUARIO_NO"] + "|  " + leer["CI"] + "-." + leer["NOMBRE"] + " " + leer["PATERNO"] + " " + leer["MATERNO"];

            }
            conn.CerrarConexion();
            return cb;
        }

        public DataTable MostrarDesc()
        {
            //transac sql
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM USUARIOS ORDER BY CONVERT(INT,SUBSTRING(USUARIO_NO,5,LEN(USUARIO_NO))) DESC";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conn.CerrarConexion();
            return tabla;
        }

        public DataTable MostrarAsc()
        {
            //transac sql
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM USUARIOS ORDER BY CONVERT(INT,SUBSTRING(USUARIO_NO,5,LEN(USUARIO_NO))) ASC";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conn.CerrarConexion();
            return tabla;
        }

        public DataTable Busqueda(string ci,string nom,string pat,string mat,string usu)
        {
            //transac sql
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM USUARIOS WHERE CI='"+ci+"' OR NOMBRE='"+nom+"' OR PATERNO='"+mat+"' OR USUARIO='"+usu+"'";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conn.CerrarConexion();
            return tabla;
        }
        /*public DataTable MostrarP()
        {
            //exex sql
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "MostrarUsuario";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conn.CerrarConexion();
            return tabla;
        }*/
        /*
        public void Insertar(string USUARIO_NO,string CI,string NOMBRE,string PATERNO,string MATERNO,string CARGO,string PROFESION,string USUARIO,string PASSWORD)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "";
            comando.CommandType = CommandType.Text;
            comando.ExecuteNonQuery();
        }
        */
        public void InsertarP(string CI, string NOMBRE, string PATERNO, string MATERNO, string CARGO, string PROFESION, string USUARIO, string PASSWORD)
        {
            // PARA EL PROCEDIMIENTO
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "INSERT_USUARIO";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@ci",CI);
            comando.Parameters.AddWithValue("@nom", NOMBRE);
            comando.Parameters.AddWithValue("@pat", PATERNO);
            comando.Parameters.AddWithValue("@mat", MATERNO);
            comando.Parameters.AddWithValue("@car", CARGO);
            comando.Parameters.AddWithValue("@pro", PROFESION);
            comando.Parameters.AddWithValue("@usu", USUARIO);
            comando.Parameters.AddWithValue("@pass", PASSWORD);
            // Y ASI SUCESIVAMENTE
            comando.ExecuteNonQuery();
            //buffer
            comando.Parameters.Clear();
        }

        public void EditarP(string CI, string NOMBRE, string PATERNO, string MATERNO, string CARGO, string PROFESION, string USUARIO, string PASSWORD, string USUARIO_NO) {
            // PARA EL PROCEDIMIENTO
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "UPDATE_USUARIO";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@ci", CI);
            comando.Parameters.AddWithValue("@nom", NOMBRE);
            comando.Parameters.AddWithValue("@pat", PATERNO);
            comando.Parameters.AddWithValue("@mat", MATERNO);
            comando.Parameters.AddWithValue("@car", CARGO);
            comando.Parameters.AddWithValue("@pro", PROFESION);
            comando.Parameters.AddWithValue("@usu", USUARIO);
            comando.Parameters.AddWithValue("@pass", PASSWORD);
            comando.Parameters.AddWithValue("@id", USUARIO_NO);            
            comando.ExecuteNonQuery();
            // buffer
            comando.Parameters.Clear();
        }

        public void Eliminar(string id)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "DELETE FROM USUARIOS WHERE USUARIO_NO='"+id+"'";
            comando.CommandType = CommandType.Text;
            comando.ExecuteNonQuery();
        }

        /*public void EliminarP(string id) {
            //proc eliminar para producto 
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "EliminarProducto";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@", id);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
        }
        */
        // LOGIN
        public bool Loggin(string usuario, string pass)
        {
            bool tabla = false;
            //transac sql
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "SELECT * FROM USUARIOS WHERE USUARIO LIKE '" + usuario + "' AND PASSWORD LIKE '" + pass + "'";
            leer = comando.ExecuteReader();
            if (leer.HasRows)
            {
                tabla = true;
            }

            conn.CerrarConexion();
            return tabla;
        }            
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CapaDatitos
{
    class Conexion
    {        
        private SqlConnection conn = new SqlConnection("server=OLIVER-QUINO\\OLIVERQUINO;database=InvActFijBadBoysAndGirls;user id = BAD_BOYS_AND_GIRLS; password =BadBoysAndGirls2023");
        public SqlConnection AbrirConexion() {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }
        public SqlConnection CerrarConexion()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            return conn;
        }
    }
}

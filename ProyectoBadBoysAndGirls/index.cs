
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocito;


namespace ProyectoBadBoysAndGirls
{
    public partial class index : Form
    {
        CP_Usuarios log = new CP_Usuarios();
        public index()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (log.VerificaLoggin(txtUsuario.Text, txtContraseña.Text))
            {
                MessageBox.Show("Bienvenido al sistema de Activos Fijos", "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                FromEmpleados fm = new FromEmpleados();
                FromUsuarios u = new FromUsuarios();
                u.Show();
                fm.ShowDialog();// muestra y no se cierra
                this.Hide();// oculta
            }
            else {
                MessageBox.Show("La constraseña es incorrecta", "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

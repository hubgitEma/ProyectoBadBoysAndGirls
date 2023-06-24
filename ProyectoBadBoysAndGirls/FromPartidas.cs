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
using ZXing;


namespace ProyectoBadBoysAndGirls
{
    public partial class FromPartidas : Form
    {
        CP_Partida OCPG = new CP_Partida();
        // creo q es para editar
        private string id = null;
        //para el metodo de adicion
        private bool Editar = false;
        public FromPartidas()
        {
            InitializeComponent();
            MostrarGral();
            txtId.Hide();
            BloqueBotones(true, false, true, true);
            BloqueoTextBox(false);
            txtNombre.Focus();           
        }

        private void MostrarGral()
        {
            // tableUSUS 3
            CP_Partida OCPG = new CP_Partida();
            dgv.DataSource = OCPG.Mostrar();           
        }

        void usuario() {
            CP_Usuarios cbusu = new CP_Usuarios();
            foreach (var item in cbusu.CbUsuario())
            {
                CbUsu.Items.Add(item);
            }
        }
        public void GeneraQR()
        {
            string txt = txtNombre.Text + " " + txtPartida.Text + " " + txtVidautil.Text;
            if (txt != "")
            {
                BarcodeWriter br = new BarcodeWriter();
                br.Format = BarcodeFormat.QR_CODE;
                Bitmap bm = new Bitmap(br.Write(txt), 200, 200);
                // muestra temporalmente
                pbGuardar.Image = bm;
            }
        }

        void BloqueBotones(bool nu, bool agre, bool mod, bool eli)
        {
            btnNuevo.Enabled = nu;
            btnGuardar.Enabled = agre;
            btnModificar.Enabled = mod;
            btnEliminar.Enabled = eli;
        }

        void BloqueoTextBox(bool sw)
        {
            txtNombre.Enabled = sw;
            txtPartida.Enabled = sw;
            txtVidautil.Enabled = sw;
            txtCoeficiente.Enabled = sw;
            cht1.Enabled = sw;
            chf1.Enabled = sw;
            cht2.Enabled = sw;
            chf2.Enabled = sw;
            CbUsu.Enabled = sw;
        }

        void limpia()
        {
            txtNombre.Text = "";
            txtPartida.Text = "";
            txtVidautil.Text = "";
            txtCoeficiente.Text = "";
            cht1.Checked = false;
            chf1.Checked = false;
            cht2.Checked = false;
            chf2.Checked = false;
            CbUsu.Items.Clear();
        }
        private void FromPartidas_Load(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Editar = false;
            BloqueoTextBox(true);
            limpia();
            usuario();
            BloqueBotones(false, true, false, false);
            btnSalir.Text = "Cancelar";            
            pbGuardar.Image = System.Drawing.Image.FromFile("D:\\UPEA\\8-1-2023\\soft\\PROYECTO_GRUPAL\\C#\\ProyectoBadBoysAndGirls\\ProyectoBadBoysAndGirls\\Resources\\carita1.jpeg");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string usu = Convert.ToString(CbUsu.SelectedItem);
            usu = usu.Substring(0, usu.IndexOf('|'));
            string dep="";
            if (cht1.Checked)
            {
                dep = "T";
            }
            else {
                if (chf1.Checked)
                {
                    dep = "F";
                }
            }
            string ac="";
            if (cht1.Checked)
            {
                ac = "T";
            }
            else
            {
                if (chf2.Checked)
                {
                    ac = "F";
                }
            }
            if (Editar == false)
            {
                try
                {
                    
                    OCPG.Insertar(txtNombre.Text, txtPartida.Text, txtVidautil.Text, txtCoeficiente.Text,dep,ac,usu);
                    MessageBox.Show("Se registro correctamente ", "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MostrarGral();
                    limpia();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo registrar los datos por: " + ex.Message, "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            if (Editar == true)
            {
                try
                {
                    OCPG.Editar(txtNombre.Text, txtPartida.Text, txtVidautil.Text, txtCoeficiente.Text, dep, ac, Convert.ToString(CbUsu.SelectedItem), txtId.Text);
                    MessageBox.Show("Se modifico correctamente ", "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MostrarGral();
                    limpia();
                    Editar = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo modificar los datos por: " + ex.Message, "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            BloqueoTextBox(false);
            BloqueBotones(true, false, true, true);            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            usuario();
            if (dgv.SelectedRows.Count > 0)
            {
                Editar = true;
                // para recuperar en el combo box              
                txtNombre.Text= dgv.CurrentRow.Cells["NOMBRE"].Value.ToString();
                txtPartida.Text = dgv.CurrentRow.Cells["PARTIDA"].Value.ToString();
                txtVidautil.Text = dgv.CurrentRow.Cells["VIDA_UTIL"].Value.ToString();
                txtCoeficiente.Text = dgv.CurrentRow.Cells["COEFICIENTE"].Value.ToString();
                if (dgv.CurrentRow.Cells["DEPRESACION"].Value.ToString().Equals("T"))
                {
                    cht1.Checked = true;
                }
                else {
                    chf1.Checked = true;
                }
                if (dgv.CurrentRow.Cells["ACTUALIZA"].Value.ToString().Equals("T"))
                {
                    cht2.Checked = true;
                }
                else
                {
                    chf2.Checked = true;
                }
                string ida= dgv.CurrentRow.Cells["USUARIO"].Value.ToString();
                CP_Usuarios cbusu = new CP_Usuarios();                
                CbUsu.SelectedItem = cbusu.RUF(ida);
                txtId.Text = dgv.CurrentRow.Cells["PARTIDA_NO"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Selecciona una fila por favor : ", "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            BloqueoTextBox(true);
            BloqueBotones(false, true, false, false);
            btnSalir.Text = "Cancelar";
            dgv.Enabled = false;            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                id = dgv.CurrentRow.Cells["PARTIDA_NO"].Value.ToString();
                OCPG.Eliminar(id);
                MostrarGral();
            }
            else
            {
                MessageBox.Show("Selecciona una fila por favor : ", "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            BloqueBotones(true, false, true, true);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (btnSalir.Text.Equals("Salir"))
            {
                Dispose();
            }
            else
            {
                MostrarGral();
                BloqueBotones(true, false, true, true);
                BloqueoTextBox(false);
                btnSalir.Text = "Salir";
                limpia();
                usuario();
                dgv.Enabled = true;
                pbGuardar.Image = System.Drawing.Image.FromFile("D:\\UPEA\\8-1-2023\\soft\\PROYECTO_GRUPAL\\C#\\ProyectoBadBoysAndGirls\\ProyectoBadBoysAndGirls\\Resources\\carita1.jpeg");
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {           
            CP_Partida OCPG = new CP_Partida();
            if (btnListar.Text.Equals("Lista Ascendente"))
            {
                dgv.DataSource = OCPG.MostrarAscendente();
                btnListar.Text = "Lista Descendente";
            }
            else
            {
                dgv.DataSource = OCPG.MostrarDescendente();
                btnListar.Text = "Lista Ascendente";
            }
        }

        private void txtPartida_TextChanged(object sender, EventArgs e)
        {
            GeneraQR();
            txtPartida.Text=txtPartida.Text.ToUpper();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            txtNombre.Text = txtNombre.Text.ToUpper();
            txtNombre.Select(txtNombre.Text.Length,0);
            GeneraQR();            
        }

        private void txtVidautil_TextChanged(object sender, EventArgs e)
        {           
            GeneraQR();            
        }

        private void txtCoeficiente_TextChanged(object sender, EventArgs e)
        {
            GeneraQR();
        }

        private void part(object sender, KeyPressEventArgs e)
        {

            if (!(e.KeyChar >= 48 && e.KeyChar <= 57) && !(e.KeyChar == 127) && !(e.KeyChar == 8))
            {
                MessageBox.Show("Ingrese solo números", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // si el valor es verdadero
                e.Handled = true;
                // retorna el valor en el parametro
                return;
            }
        }

        private void coe(object sender, KeyPressEventArgs e)
        {

            if (!(e.KeyChar >= 48 && e.KeyChar <= 57) && !(e.KeyChar == 127) && !(e.KeyChar == 8) && !(e.KeyChar == 46) && !(e.KeyChar == 44))
            {
                MessageBox.Show("Ingrese solo números", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // si el valor es verdadero
                e.Handled = true;
                // retorna el valor en el parametro
                return;
            }
        }

        private void vu(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= 48 && e.KeyChar <= 57) && !(e.KeyChar == 127) && !(e.KeyChar == 8))
            {
                MessageBox.Show("Ingrese solo números", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // si el valor es verdadero
                e.Handled = true;
                // retorna el valor en el parametro
                return;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= 65 && e.KeyChar <= 90) && !(e.KeyChar >= 97 && e.KeyChar <= 122) && !(e.KeyChar == 32) && !(e.KeyChar == 8))
            {
                MessageBox.Show("Ingrese solo texto", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // si el valor es verdadero
                e.Handled = true;
                // retorna el valor en el parametro
                return;
            }
        }

        private void txtBuscaroficinas_TextChanged(object sender, EventArgs e)
        {
            dgv.DataSource = null;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {            
            dgv.DataSource = OCPG.Busqueda(txtBusca.Text, "", "", "", "");
            
        }

        private void pictureBox4_MouseClick(object sender, MouseEventArgs e)
        {
            //dgv.DataSource = null;
        }
    }
}

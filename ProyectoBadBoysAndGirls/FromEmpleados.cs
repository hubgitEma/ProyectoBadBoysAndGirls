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
    public partial class FromEmpleados : Form
    {
        CP_Empleados OCPG = new CP_Empleados();
        // creo q es para editar
        private string id = null;
        //para el metodo de adicion
        private bool Editar = false;
        public FromEmpleados()
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
            CP_Empleados OCPG = new CP_Empleados();
            dgv.DataSource = OCPG.MostrarEmpleadoAscendente();            
            
        }

        void comboBox() {
            CP_Usuarios cbusu = new CP_Usuarios();
            foreach (var item in cbusu.CbUsuario())
            {
                CbUsuario.Items.Add(item);
            }
            CP_Oficina cbo = new CP_Oficina();
            for (int i = 0; i < cbo.CbOficina().Count; i++)
            {
                CbOficina.Items.Add(cbo.CbOficina()[i]);
            }
        }
        public void GeneraQR()
        {
            string txt = txtNombre.Text;
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
            btnEditar.Enabled = mod;
            btnEliminar.Enabled = eli;
        }

        void BloqueoTextBox(bool sw)
        {
            txtNombre.Enabled = sw;
            txtProfesion.Enabled = sw;
            txtCargo.Enabled = sw;
            txtCelular.Enabled = sw;
            txtCi.Enabled = sw;
            txtUnidad.Enabled = sw;
            CbExp.Enabled = sw;
            CbOficina.Enabled = sw;
            CbUsuario.Enabled = sw;
        }

        void limpiar()
        {
            txtNombre.Text = "";
            txtProfesion.Text = "";
            txtCargo.Text = "";
            txtCelular.Text = "";
            txtCi.Text = "";
            txtUnidad.Text = "";
            CbExp.Text = "";
            CbOficina.Items.Clear();
            CbUsuario.Items.Clear();
        }
        private void pbPdf_Click(object sender, EventArgs e)
        {

        }

        private void gunaPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string usu = Convert.ToString(CbUsuario.SelectedItem);
            usu = usu.Substring(0, usu.IndexOf('|'));
            string exp = Convert.ToString(CbExp.SelectedItem);
            string ofi = Convert.ToString(CbOficina.SelectedItem);
            ofi = ofi.Substring(0, ofi.IndexOf('|'));
            if (Editar == false)
            {
                try
                {
                    OCPG.InsertarEmpleados(txtCi.Text, exp, txtNombre.Text, txtCargo.Text, ofi, txtUnidad.Text, txtAreadeTrabajo.Text, txtCelular.Text, txtProfesion.Text, usu);
                    MessageBox.Show("Se registro correctamente ", "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MostrarGral();
                    limpiar();
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
                    OCPG.EditarEmpleados(txtCi.Text, exp, txtNombre.Text, txtCargo.Text, ofi, txtUnidad.Text, txtAreadeTrabajo.Text, txtCelular.Text, txtProfesion.Text, usu, txtId.Text);
                    MessageBox.Show("Se modifico correctamente ", "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MostrarGral();
                    limpiar();
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            comboBox();
            if (dgv.SelectedRows.Count > 0)
            {
                Editar = true;
                txtCi.Text = dgv.CurrentRow.Cells["CI"].Value.ToString();
                CbExp.Text = dgv.CurrentRow.Cells["EXP"].Value.ToString();
                txtNombre.Text = dgv.CurrentRow.Cells["NOMBRE"].Value.ToString();
                txtCargo.Text = dgv.CurrentRow.Cells["CARGO"].Value.ToString();
                // para recuperar en el combo box              
                String ida=dgv.CurrentRow.Cells["OFICINA"].Value.ToString();
                CP_Oficina cbo = new CP_Oficina();                
                CbOficina.SelectedItem= cbo.ROF(ida);
                txtUnidad.Text = dgv.CurrentRow.Cells["UNIDAD"].Value.ToString();
                txtAreadeTrabajo.Text = dgv.CurrentRow.Cells["AREA_TRABAJO"].Value.ToString();
                txtCelular.Text = dgv.CurrentRow.Cells["CELULAR"].Value.ToString();
                txtProfesion.Text = dgv.CurrentRow.Cells["PROFESION"].Value.ToString();
                //usuario
                ida= dgv.CurrentRow.Cells["USUARIO"].Value.ToString();
                CP_Usuarios cbusu = new CP_Usuarios();
                CbUsuario.SelectedItem = cbusu.RUF(ida);
                id = dgv.CurrentRow.Cells["EMP_NO"].Value.ToString();
                txtId.Text = dgv.CurrentRow.Cells["EMP_NO"].Value.ToString();

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

        private void FromEmpleados_Load(object sender, EventArgs e)
        {
            //MostrarGral();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                id = dgv.CurrentRow.Cells["EMP_NO"].Value.ToString();
                OCPG.EliminarEmpleado(id);
                MostrarGral();
            }
            else
            {
                MessageBox.Show("Selecciona una fila por favor : ", "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Listar(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void com(object sender, EventArgs e)
        {

        }

        public void GeneraQR(string txt)
        {
            if (txt != "")
            {
                BarcodeWriter br = new BarcodeWriter();
                br.Format = BarcodeFormat.QR_CODE;
                Bitmap bm = new Bitmap(br.Write(txt), 200, 200);
                // muestra temporalmente
                pbGuardar.Image = bm;
            }
        }

        private void gunaTransfarantPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void QR(object sender, EventArgs e)
        {
            string qr = txtCi.Text + " " + CbOficina.SelectedItem + " " + txtNombre.Text + " " + txtCargo.Text + " " + CbOficina.SelectedItem + " " + txtUnidad.Text + "  " + txtAreadeTrabajo.Text + " " + txtCelular.Text + " " + txtProfesion.Text;
            GeneraQR(qr);
        }

        private void gunaPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            CP_Empleados OCPG = new CP_Empleados();
            if (btnListar.Text.Equals("Lista Ascendente"))
            {                
                dgv.DataSource = OCPG.MostrarEmpleadoAscendente();
                btnListar.Text = "Lista Descendente";
            }
            else
            {
                dgv.DataSource = OCPG.MostrarEmpleadoDescendente();
                btnListar.Text = "Lista Ascendente";
            }
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
                limpiar();
                dgv.Enabled = true;
                pbGuardar.Image = System.Drawing.Image.FromFile("D:\\UPEA\\8-1-2023\\soft\\PROYECTO_GRUPAL\\C#\\ProyectoBadBoysAndGirls\\ProyectoBadBoysAndGirls\\Resources\\carita1.jpeg");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {            
            Editar = false;
            BloqueoTextBox(true);
            limpiar();
            comboBox();
            BloqueBotones(false, true, false, false);
            btnSalir.Text = "Cancelar";
            //comboBox();
            pbGuardar.Image = System.Drawing.Image.FromFile("D:\\UPEA\\8-1-2023\\soft\\PROYECTO_GRUPAL\\C#\\ProyectoBadBoysAndGirls\\ProyectoBadBoysAndGirls\\Resources\\carita1.jpeg");
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            GeneraQR();
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
               if (!(e.KeyChar >= 65 && e.KeyChar <= 90) && !(e.KeyChar >= 97 && e.KeyChar <= 122) &&!(e.KeyChar == 32) && !(e.KeyChar == 8))
            {
                MessageBox.Show("Ingrese solo texto", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // si el valor es verdadero
                e.Handled = true;
                // retorna el valor en el parametro
                return;
            }
        }

        private void txtProfesion_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCargo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtAreadeTrabajo_TextChanged(object sender, EventArgs e)
        {
            txtAreadeTrabajo.Text = txtAreadeTrabajo.Text.ToUpper();
            txtAreadeTrabajo.Select(txtAreadeTrabajo.Text.Length, 0);
            GeneraQR();
        }

        private void txtAreadeTrabajo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtUnidad_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCi_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtNombre_TextChanged_1(object sender, EventArgs e)
        {
            txtNombre.Text = txtNombre.Text.ToUpper();
            txtNombre.Select(txtNombre.Text.Length, 0);
            GeneraQR();
        }

        private void txtProfesion_TextChanged(object sender, EventArgs e)
        {
            txtProfesion.Text = txtProfesion.Text.ToUpper();
            txtProfesion.Select(txtProfesion.Text.Length, 0);
            GeneraQR();
        }

        private void txtCargo_TextChanged(object sender, EventArgs e)
        {
            txtCargo.Text = txtProfesion.Text.ToUpper();
            txtCargo.Select(txtCargo.Text.Length, 0);
            GeneraQR();
        }

        private void txtCi_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUnidad_TextChanged(object sender, EventArgs e)
        {
            txtUnidad.Text = txtUnidad.Text.ToUpper();
            txtUnidad.Select(txtUnidad.Text.Length, 0);
            GeneraQR();
        }
    }
}


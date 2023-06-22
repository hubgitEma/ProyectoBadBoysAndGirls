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
        }
        private void MostrarGral()
        {
            // tableUSUS 3
            CP_Empleados OCPG = new CP_Empleados();
            dgv.DataSource = OCPG.MostrarEmpleadoAscendente();
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

        void limpiar()
        {
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
                    //limpiar();
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
                    //limpiar();
                    Editar = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo modificar los datos por: " + ex.Message, "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
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
                MessageBox.Show(cbo.ROF(ida));
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
    }
}


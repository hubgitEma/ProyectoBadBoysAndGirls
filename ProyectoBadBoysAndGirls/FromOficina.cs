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
    public partial class FromOficina : Form
    {
        CP_Oficina OCPG = new CP_Oficina();
        // creo q es para editar
        private string id = null;
        //para el metodo de adicion
        private bool Editar = false;
        public FromOficina()
        {
            InitializeComponent();
            MostrarGral();
            txtId.Hide();
            BloqueBotones(true,false,true,true);
            BloqueoTextBox(false);
            txtCodigoOficina.Focus();            
        }

        private void MostrarGral()
        {
            // tableUSUS 3
            CP_Oficina OCPG = new CP_Oficina();
            dgv.DataSource = OCPG.Mostrar();            
        }

        public void GeneraQR()
        {
            string txt = txtNombre.Text + " " + txtResponsable.Text + " " + txtUbicacion.Text;
            if (txt != "")
            {
                BarcodeWriter br = new BarcodeWriter();
                br.Format = BarcodeFormat.QR_CODE;
                Bitmap bm = new Bitmap(br.Write(txt), 200, 200);
                // muestra temporalmente
                pbGuardar.Image = bm;
            }
        }

        void BloqueBotones(bool nu,bool agre,bool mod,bool eli)
        {
            btnNuevo.Enabled = nu;
            btnGuardar.Enabled = agre;
            btnModificar.Enabled = mod;
            btnEliminar.Enabled = eli;
        }

        void BloqueoTextBox(bool sw ) {
            cbDepartamento.Enabled = sw;
            txtCodigoOficina.Enabled = sw;
            txtNombre.Enabled = sw;
            txtResponsable.Enabled = sw;
            txtUbicacion.Enabled = sw;
        }

        void limpia() {
            cbDepartamento.Items.Clear();
            txtCodigoOficina.Text = "";
            txtNombre.Text = "";
            txtResponsable.Text = "";
            txtUbicacion.Text = "";
        }

        

        bool Verifica() {
            if ((cbDepartamento.SelectedIndex.Equals(" ")) || (txtCodigoOficina.Text.Equals(" ")) || (txtNombre.Text.Equals(" ")) || (txtResponsable.Text.Equals(" ")) || txtUbicacion.Text.Equals(" ")) {
                MessageBox.Show("No debe existir campos vacios", "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Editar = false;
            BloqueoTextBox(true);
            limpia();
            BloqueBotones(false,true,false,false);
            btnSalir.Text = "Cancelar";
            comboBox();
            pbGuardar.Image = System.Drawing.Image.FromFile("D:\\UPEA\\8-1-2023\\soft\\PROYECTO_GRUPAL\\C#\\ProyectoBadBoysAndGirls\\ProyectoBadBoysAndGirls\\Resources\\carita1.jpeg");
        }

        void comboBox() {
            cbDepartamento.Items.Add("LA PAZ");
            cbDepartamento.Items.Add("COCHABAMBA");
            cbDepartamento.Items.Add("SANTA CRUZ");
            cbDepartamento.Items.Add("BENI");
            cbDepartamento.Items.Add("SUCRE");
            cbDepartamento.Items.Add("TARIJA");
            cbDepartamento.Items.Add("ORURO");
            cbDepartamento.Items.Add("POTOSI");
            cbDepartamento.Items.Add("PANDO");
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //          string CodO = Convert.ToString(cbDepartamento.SelectedItem);
            //            CodO = CodO.Substring(0, CodO.IndexOf('|'));                        
            if (Editar == false )
            {
                try
                {
                    OCPG.Insertar(Convert.ToString(cbDepartamento.SelectedItem), txtCodigoOficina.Text, txtNombre.Text, txtResponsable.Text, txtUbicacion.Text);
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
                   OCPG.Editar(Convert.ToString(cbDepartamento.SelectedItem), txtCodigoOficina.Text, txtNombre.Text, txtResponsable.Text, txtUbicacion.Text, txtId.Text);
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
            BloqueBotones(true,false,true,true);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            comboBox();
            if (dgv.SelectedRows.Count > 0)
            {
                Editar = true;
                // para recuperar en el combo box              
                cbDepartamento.SelectedItem = dgv.CurrentRow.Cells["DEPTO"].Value.ToString();                                
                txtCodigoOficina.Text = dgv.CurrentRow.Cells["COD_OFICINA"].Value.ToString();
                txtNombre.Text = dgv.CurrentRow.Cells["NOMBRE_OF"].Value.ToString();
                txtResponsable.Text = dgv.CurrentRow.Cells["RESPONSABLE"].Value.ToString();
                txtUbicacion.Text = dgv.CurrentRow.Cells["UBICACION"].Value.ToString();                           
                txtId.Text = dgv.CurrentRow.Cells["OFICINA_NO"].Value.ToString();
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
                id = dgv.CurrentRow.Cells["OFICINA_NO"].Value.ToString();                
                OCPG.Eliminar(id);
                MostrarGral();
            }
            else
            {
                MessageBox.Show("Selecciona una fila por favor : ", "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            BloqueBotones(true, false, true, true);
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            if (btnListar.Text.Equals("Lista Ascendente"))
            {
                CP_Oficina OCPG = new CP_Oficina();
                dgv.DataSource = OCPG.MostrarAscendente();
                btnListar.Text = "Lista Descendente";
            }
            else {
                CP_Oficina OCPG = new CP_Oficina();
                dgv.DataSource = OCPG.MostrarDescendente();
                btnListar.Text = "Lista Ascendente";
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (btnSalir.Text.Equals("Salir"))
            {
                Dispose();
            }
            else {
                MostrarGral();
                BloqueBotones(true,false,true,true);
                BloqueoTextBox(false);
                btnSalir.Text = "Salir";
                limpia();
                dgv.Enabled = true;
                pbGuardar.Image = System.Drawing.Image.FromFile("D:\\UPEA\\8-1-2023\\soft\\PROYECTO_GRUPAL\\C#\\ProyectoBadBoysAndGirls\\ProyectoBadBoysAndGirls\\Resources\\carita1.jpeg");
            }
        }

        private void gunaPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FromOficina_Load(object sender, EventArgs e)
        {
            comboBox();
        }

        private void Numero(object sender, KeyPressEventArgs e)
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

        private void Texto(object sender, KeyPressEventArgs e)
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

        private void txtCodigoOficina_TextChanged(object sender, EventArgs e)
        {
            GeneraQR();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            GeneraQR();
        }

        private void txtResponsable_TextChanged(object sender, EventArgs e)
        {
            GeneraQR();
        }

        private void txtUbicacion_TextChanged(object sender, EventArgs e)
        {
            GeneraQR();
        }
    }
}

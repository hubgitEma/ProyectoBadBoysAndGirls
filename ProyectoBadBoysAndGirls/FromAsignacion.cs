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
    public partial class FromAsignacion : Form
    {
        CP_Inventarios OCPG = new CP_Inventarios();
        // creo q es para editar
        private string id = null;
        //para el metodo de adicion
        private bool Editar = false;
        public FromAsignacion()
        {
            InitializeComponent();
            MostrarGral();
            txtId.Hide();
            BloqueBotones(true, false, true, true);
            BloqueoTextBox(false);
        }

        private void MostrarGral()
        {
            // tableUSUS 3
            CP_Inventarios OCPG = new CP_Inventarios();
            dgv.DataSource = OCPG.Mostrar();
        }

        public void GeneraQR()
        {
            string txt = txtPartida.Text;
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
            txtAuxiliar.Enabled = sw;
            txtPartida.Enabled = sw;
            txtCodigoEntidad.Enabled = sw;
            txtCodigoAntiguo.Enabled = sw;
            txtSerie.Enabled = sw;
            txtDescripcion.Enabled = sw;
            txtEstado.Enabled = sw;
            txtEspecifica.Enabled = sw;
            txtEmpNo.Enabled = sw;
            txtProcedencia.Enabled = sw;
            dtpFechadeIngreso.Enabled = sw;
            txtObservacion.Enabled = sw;
        }

        void limpia()
        {
            txtAuxiliar.Text = "";
            txtPartida.Text = "";
            txtCodigoEntidad.Text = "";
            txtCodigoAntiguo.Text = "";
            txtSerie.Text = "";
            txtDescripcion.Text = "";
            txtEstado.Text = "";
            txtEspecifica.Text = "";
            txtEmpNo.Text = "";
            txtProcedencia.Text = "";
            dtpFechadeIngreso.Text = "";
            txtObservacion.Text = "";
            txtEmpNo.Items.Clear();
        }

        private void FromAsignacion_Load(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

            Editar = false;
            BloqueoTextBox(true);
            limpia();
            BloqueBotones(false, true, false, false);
            btnSalir.Text = "Cancelar";
            pbGuardar.Image = System.Drawing.Image.FromFile("D:\\UPEA\\8-1-2023\\soft\\PROYECTO_GRUPAL\\C#\\ProyectoBadBoysAndGirls\\ProyectoBadBoysAndGirls\\Resources\\carita1.jpeg");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string usu = Convert.ToString(txtEmpNo.SelectedItem);
            usu = usu.Substring(0, usu.IndexOf('|'));
            if (Editar == false)
            {
                try
                {

                    //OCPG.Insertar(txtAuxiliar.Text, txtPartida.Text, txtCodigoEntidad.Text, txtCodigoAntiguo.Text, txtSerie.Text, txtDescripcion.Text, txtEstado.Text, txtEspecifica.Text,usu, txtProcedencia.Text, dtpFechadeIngreso.Text, txtObservacion.Text);
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
                    // OCPG.Editar(txtAuxiliar.Text, txtPartida.Text, txtCodigoEntidad.Text, txtCodigoAntiguo.Text, txtSerie.Text, txtDescripcion.Text, txtEstado.Text, txtEspecifica.Text, usu, txtProcedencia.Text, dtpFechadeIngreso.Text, txtObservacion.Text, txtId.Text);
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
            if (dgv.SelectedRows.Count > 0)
            {
                Editar = true;
                // para recuperar en el combo box              
                txtAuxiliar.Text = dgv.CurrentRow.Cells["AUXILIAR"].Value.ToString();
                txtPartida.Text = dgv.CurrentRow.Cells["PARTIDA"].Value.ToString();
                txtCodigoEntidad.Text = dgv.CurrentRow.Cells["COD_ENTIDAD"].Value.ToString();
                txtCodigoAntiguo.Text = dgv.CurrentRow.Cells["COD_ANTIGUO"].Value.ToString();
                txtSerie.Text = dgv.CurrentRow.Cells["SERIE"].Value.ToString();
                txtDescripcion.Text = dgv.CurrentRow.Cells["DESCRIPCION"].Value.ToString();
                txtEstado.Text = dgv.CurrentRow.Cells["ESTADO"].Value.ToString();
                txtEspecifica.Text = dgv.CurrentRow.Cells["ESPECIFICA"].Value.ToString();
                txtProcedencia.Text = dgv.CurrentRow.Cells["PROCEDENCIA"].Value.ToString();
                dtpFechadeIngreso.Text = dgv.CurrentRow.Cells["FECHA_INGRESO"].Value.ToString();
                txtObservacion.Text = dgv.CurrentRow.Cells["OBSERVACION"].Value.ToString();
                string ida = dgv.CurrentRow.Cells["EMP_NO"].Value.ToString();
                CP_Empleados cbusu = new CP_Empleados();
                //txtEmpNo.SelectedItem = cbusu.RUF(ida);
                txtId.Text = dgv.CurrentRow.Cells["INV_NO"].Value.ToString();
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
                id = dgv.CurrentRow.Cells["INV_NO"].Value.ToString();
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
                dgv.Enabled = true;
                pbGuardar.Image = System.Drawing.Image.FromFile("D:\\UPEA\\8-1-2023\\soft\\PROYECTO_GRUPAL\\C#\\ProyectoBadBoysAndGirls\\ProyectoBadBoysAndGirls\\Resources\\carita1.jpeg");
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            CP_Inventarios OCPG = new CP_Inventarios();
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
    }
}

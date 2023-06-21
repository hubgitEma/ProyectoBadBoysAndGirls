// Agregamos la capa de negocio
// Establecer como Proyecto inicial
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// agregamos 
using CapaNegocito;

namespace ProyectoBadBoysAndGirls
{
    public partial class FromUsuarios : Form
    {
        CP_Usuarios OCPG = new CP_Usuarios();
        // creo q es para editar
        private string idUsuario = null;
        //para el metodo de adicion
        private bool Editar = false;
        public FromUsuarios()
        {
            InitializeComponent();
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            FromUsuarios fu = new FromUsuarios();
            fu.ShowDialog();
        }
        private void MostrarUsuarios()
        {
            // tableUSUS 3
            CP_Usuarios OCP = new CP_Usuarios();
            dgv1.DataSource = OCP.MostrarUsuario();
        }


        //btn guardar
        private void Insert()
        {
            if (Editar == false)
            {
                try
                {
                    //OCPG.InsertarUsuario();
                    MessageBox.Show("Se registro correctamente ", "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MostrarUsuarios();
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
                    //OCPG.EditarUsuario();
                    MessageBox.Show("Se modifico correctamente ", "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MostrarUsuarios();
                    //limpiar();
                    Editar = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo modificar los datos por: " + ex.Message, "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //Para editar datagriew
        private void btnSelectDGV(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                Editar = true;
                //cajatxt.tex=dgv.CurrentRow.Cells["AtributoBD"].Value.ToString();
                idUsuario = dgv.CurrentRow.Cells[""].Value.ToString();
            }
            else
            {
                MessageBox.Show("Selecciona una fila por favor : ", "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar(DataGridView dgv, string id)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                idUsuario = dgv.CurrentRow.Cells[""].Value.ToString();
                OCPG.EliminarUsuario(idUsuario);
                MostrarUsuarios();
            }
            else
            {
                MessageBox.Show("Selecciona una fila por favor : ", "Bad Boys And Girls", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void index_Load(object sender, EventArgs e)
        {
            MostrarUsuarios();
        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

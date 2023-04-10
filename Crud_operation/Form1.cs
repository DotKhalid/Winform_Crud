using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Crud_operation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Crud;User ID=sa;Password=aptech");
        public int S_ID;


        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentsRecord();
        }

        private void GetStudentsRecord()
        {
            
            SqlCommand cmd = new SqlCommand("select * from Student",conn);
            DataTable dt = new DataTable();

            conn.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();

            Student_grid.DataSource = dt;
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("Insert Into Student Values (@Name,@Email,@Phone,@Address)", conn);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Name", txt_name.Text);
                cmd.Parameters.AddWithValue("@Email", txt_email.Text);
                cmd.Parameters.AddWithValue("@Phone", txt_phone.Text);
                cmd.Parameters.AddWithValue("@Address", txt_address.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Student succefully added", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentsRecord();

                ReseteFormControls();
            }
        }

        private bool IsValid()
        {
            if (txt_name.Text == string.Empty)
            {
                MessageBox.Show("Student Name is Required", "failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if(S_ID>0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE Student SET Name = @Name, Email = @Email, Phone = @Phone, Address = @Address WHERE S_ID = @ID", conn);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Name", txt_name.Text);
                cmd.Parameters.AddWithValue("@Email", txt_email.Text);
                cmd.Parameters.AddWithValue("@Phone", txt_phone.Text);
                cmd.Parameters.AddWithValue("@Address", txt_address.Text);
                cmd.Parameters.AddWithValue("@ID", this.S_ID);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Student Information succefully Updated", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentsRecord();

                ReseteFormControls();

            }
            else
            {
                MessageBox.Show("Please Select Student to update", "Select", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (S_ID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Student WHERE S_ID = @ID", conn);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.S_ID);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Student succefully DELETED", "DELETE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentsRecord();

                ReseteFormControls();

            }
            else
            {
                MessageBox.Show("Please Select Student to update", "Select", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            ReseteFormControls();
        }

        private void ReseteFormControls()
        {
            
            txt_name.Clear();
            txt_email.Clear();
            txt_address.Clear();
            txt_phone.Clear();

            txt_name.Focus();
        }

        private void Student_grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            S_ID = Convert.ToInt32(Student_grid.SelectedRows[0].Cells[0].Value);
            txt_name.Text = Student_grid.SelectedRows[0].Cells[1].Value.ToString();
            txt_email.Text = Student_grid.SelectedRows[0].Cells[2].Value.ToString();
            txt_phone.Text = Student_grid.SelectedRows[0].Cells[3].Value.ToString();
            txt_address.Text = Student_grid.SelectedRows[0].Cells[4].Value.ToString();
        }
    }
}

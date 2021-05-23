using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-KCB2BH6\MSSQLSERVER02;Initial Catalog=CRUD;Integrated Security=True");
        public int StudentID;

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentsRecord();
        }

        private void GetStudentsRecord()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Students",con);

            DataTable dt = new DataTable();

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            StudentsDataGridView.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("insert into Students values (@name, @FatherName, @Roll, @Address, @Mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtStudent.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFather.Text);
                cmd.Parameters.AddWithValue("@Roll", txtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);

                con.Open();



                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("New Student is successfully saved in database", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentsRecord();
                ResetFormControl();

            }
        }

        private bool IsValid()
        {
            if(txtStudent.Text == String.Empty)
            {
                MessageBox.Show("Student Name Required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetFormControl();

        }

        private void ResetFormControl()
        {
            StudentID = 0;
            txtStudent.Clear();
            txtRoll.Clear();
            txtMobile.Clear();
            txtFather.Clear();
            txtAddress.Clear();

            txtStudent.Focus();
        }

        private void StudentsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentID = Convert.ToInt32(StudentsDataGridView.SelectedRows[0].Cells[0].Value);
            txtStudent.Text = StudentsDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtFather.Text = StudentsDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtRoll.Text = StudentsDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = StudentsDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtMobile.Text = StudentsDataGridView.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE Students SET Name = @name,FatherName = @FatherName,RollNumber = @Roll,Address = @Address,Mobile = @Mobile WHERE StudentID = @id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", this.StudentID);
                cmd.Parameters.AddWithValue("@name", txtStudent.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFather.Text);
                cmd.Parameters.AddWithValue("@Roll", txtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);

                con.Open();



                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Student Information is Updated successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentsRecord();
                ResetFormControl();
            }
            else
            {
                MessageBox.Show("Please Select an student to Update his information", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Students WHERE StudentID = @id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", this.StudentID);
              

                con.Open();



                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Student Information is Deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentsRecord();
                ResetFormControl();
            }
            else
            {
                MessageBox.Show("Please Select an student to delete", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}

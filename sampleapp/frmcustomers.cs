using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace studyCaseApp
{
    public partial class frmcustomers : Form
    {
        public frmcustomers()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudyCaseDb;Integrated Security=True");
        DataSet daset = new DataSet();
        private void frmcustomersList_Load(object sender, EventArgs e)
        {
            ShowRecords();

        }
        private void ShowRecords()
        {
            baglantı.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from customers", baglantı);
            adtr.Fill(daset, "customers");
            dataGridView1.DataSource = daset.Tables["customers"];
            baglantı.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand command = new SqlCommand("delete from customers where Id='" + dataGridView1.CurrentRow.Cells["Id"].Value.ToString() + "'", baglantı);
            command.ExecuteNonQuery();
            baglantı.Close();
            daset.Tables["customers"].Clear();
            ShowRecords();
            MessageBox.Show("Müşteri Başarıyla Silindi");
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand command = new SqlCommand("update customers set Name=@Name,Address=@Address,Email=@Email where Id=@Id", baglantı);
            command.Parameters.AddWithValue("@Id", txtId.Text);
            command.Parameters.AddWithValue("@Name", txtName.Text);
            command.Parameters.AddWithValue("@Address", txtAddress.Text);
            command.Parameters.AddWithValue("@Email", txtEmail.Text);
            command.ExecuteNonQuery();
            baglantı.Close();
            daset.Tables["customers"].Clear();
            ShowRecords();
            MessageBox.Show("Müşteri Kaydı Başarıyla Güncellendi");
            foreach (Control item in panel2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = string.Empty;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
            txtName.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
            txtAddress.Text = dataGridView1.CurrentRow.Cells["Address"].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells["Email"].Value.ToString();

        }



        private void button1_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand command = new SqlCommand("insert into customers(Name,Address,Email) values(@Name,@Address,@Email) ", baglantı);
            command.Parameters.AddWithValue("@Name", txtNameAdd.Text);
            command.Parameters.AddWithValue("@Address", txtAddressAdd.Text);
            command.Parameters.AddWithValue("@Email", txtEmailAdd.Text);
            command.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Müşteri Kaydı Başarıyla Eklendi");
            daset.Tables["customers"].Clear();
            ShowRecords();
            foreach (Control item in panel1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = string.Empty;
                }
            }
        }

        private void txtNameSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglantı.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from customers where Name like '%" + txtNameSearch.Text + "%'", baglantı);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglantı.Close();

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankSimulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-1PLLIFRV\SQLEXPRESS;Initial Catalog=DbBankaSim;Integrated Security=True");
        private void lnkKayitOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 frm = new Form3();
            frm.Show();
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("Select * from TBLKİSİLER where HESAPNO=@p1 and ŞİFRE=@p2", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@p1", mtbHesapNo.Text);
            sqlCommand.Parameters.AddWithValue("@p2", tbxParola.Text);
            sqlCommand.ExecuteNonQuery();
            SqlDataReader dr = sqlCommand.ExecuteReader();

            if(dr.Read())
            {
                Form2 frm = new Form2();    
                frm.hesapno = mtbHesapNo.Text;
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı HesapNo veya Parola !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            sqlConnection.Close();
        }
    }
}

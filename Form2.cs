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
{//
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-1PLLIFRV\SQLEXPRESS;Initial Catalog=DbBankaSim;Integrated Security=True");
        void bakiye()
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("select BAKİYE FROM TBLHESAP WHERE HESAPNO=@P1", sqlConnection);
            command.Parameters.AddWithValue("@P1", hesapno);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                lblbakiye.Text = dr[0].ToString();
            }
            sqlConnection.Close();
        }

        void giren()
        {
            SqlCommand command = new SqlCommand("EXECUTE GELEN @P1", sqlConnection);
            command.Parameters.AddWithValue("@P1", lblHesapNo.Text);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        void giden()
        {
            SqlCommand command = new SqlCommand("EXECUTE GIDEN @P1", sqlConnection);
            command.Parameters.AddWithValue("@P1", hesapno);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt1 = new DataTable();
            adapter.Fill(dt1);
            dataGridView1.DataSource = dt1;
        }
        
        public string hesapno;
        private void Form2_Load(object sender, EventArgs e)
        {
            lblHesapNo.Text = hesapno;
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("select * from TBLKİSİLER where HESAPNO=@P1", sqlConnection);
            cmd.Parameters.AddWithValue("@P1", hesapno);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lblAdSoyad.Text = reader[1] + " " + reader[2];
                lblTC.Text = reader[3].ToString();
                lblTelefon.Text = reader[4].ToString();
            }
            sqlConnection.Close();
            bakiye();
            giren();
            giden();
        }
        private void btnGönder_Click(object sender, EventArgs e)
        {
            
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO TBLHAREKET (GÖNDEREN,TUTAR,ALICI) values (@P1,@P2,@P3)", sqlConnection);
            cmd.Parameters.AddWithValue("@P1", lblHesapNo.Text);
            cmd.Parameters.AddWithValue("@P2", Convert.ToDecimal(tbxTutar.Text));
            cmd.Parameters.AddWithValue("@P3", mtbHesapNO.Text);
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("İşlem Başarılı !", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bakiye();
            giren();
            giden();

        }
    }
}

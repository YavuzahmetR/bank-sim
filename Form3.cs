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

namespace BankSimulation
{//
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-1PLLIFRV\SQLEXPRESS;Initial Catalog=DbBankaSim;Integrated Security=True");
             
        private void btnKayitOl_Click(object sender, EventArgs e)
        {           
             connection.Open();
             SqlCommand command = new SqlCommand("insert into TBLKİSİLER (AD,SOYAD,TC,TELEFON,HESAPNO,ŞİFRE,BAKİYE) values (@K1,@K2,@K3,@K4,@K5,@K6,@K7)", connection);
             command.Parameters.AddWithValue("@K1", tbxAd.Text);
             command.Parameters.AddWithValue("@K2", tbxSoyad.Text);
             command.Parameters.AddWithValue("@K3", mtbTC.Text);
             command.Parameters.AddWithValue("@K4", mtbTelefon.Text);
             command.Parameters.AddWithValue("@K5", mtbHesapno.Text);
             command.Parameters.AddWithValue("@K6", tbxSifre.Text);
             command.Parameters.AddWithValue("@K7", tbxBakiye.Text);
             command.ExecuteNonQuery();
             connection.Close();

             connection.Open();
             SqlCommand command1 = new SqlCommand("INSERT INTO TBLHESAP (HESAPNO,BAKİYE,AD,SOYAD) VALUES (@P1,@P2,@P3,@P4)", connection);
             command1.Parameters.AddWithValue("@P1", mtbHesapno.Text);
             command1.Parameters.AddWithValue("@P2", Convert.ToDecimal(tbxBakiye.Text));
             command1.Parameters.AddWithValue("@P3", tbxAd.Text);
             command1.Parameters.AddWithValue("@P4", tbxSoyad.Text);
             command1.ExecuteNonQuery();
             connection.Close();
             MessageBox.Show("Kayıt Oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
             this.Close();

             Form1 frm = new Form1();
             frm.Show();
        }
        int kontrol;
        private void btnRandom_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select * from TBLHESAP", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                kontrol = Convert.ToInt32(reader[0]);
            }
            connection.Close();

            Random random = new Random();
            int sayi = random.Next(100000, 1000000);

            if(sayi != kontrol)
            {
                mtbHesapno.Text = sayi.ToString();
            }
            else
            {
                int sayiyeni = random.Next(100000, 1000000);
                sayi = sayiyeni;
            }
        }
    }
}

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

namespace DataGridViewVeriGuncellemeVeArama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglan=new SqlConnection("Data Source=IRFAN\\SQLEXPRESS;Initial Catalog=Kitaplar;Integrated Security=True");
        public void verilerigoster(string veriler)
        {
            SqlDataAdapter da=new SqlDataAdapter(veriler,baglan);
            DataSet ds=new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            verilerigoster("Select *from kitapbilgi");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("insert into kitapbilgi(kitapad,kitapyazar,basimevi,sayfa) Values(@adi,@yazari,@basimyeri,@sayfasi)", baglan);
            komut.Parameters.AddWithValue("@adi", textBox1.Text);
            komut.Parameters.AddWithValue("@yazari", textBox2.Text);
            komut.Parameters.AddWithValue("@basimyeri", textBox3.Text);
            komut.Parameters.AddWithValue("@sayfasi", textBox4.Text);
            komut.ExecuteNonQuery();
            baglan.Close();
            verilerigoster("Select *from kitapbilgi");
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("delete from kitapbilgi where kitapad=@adi", baglan);
            komut.Parameters.AddWithValue("@adi", textBox5.Text);
            komut.ExecuteNonQuery();
            baglan.Close();
            verilerigoster("Select *from kitapbilgi");
            textBox5.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("update kitapbilgi set kitapyazar='" + textBox2.Text + "',basimevi='" + textBox3.Text + "',sayfa='" + textBox4.Text + "'where kitapad='" + textBox1.Text + "'", baglan);
            komut.ExecuteNonQuery();
            baglan.Close();
            verilerigoster("Select *from kitapbilgi");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilialan = dataGridView1.SelectedCells[0].RowIndex;
            string kitapad= dataGridView1.Rows[secilialan].Cells[0].Value.ToString();
            string kitapyazar = dataGridView1.Rows[secilialan].Cells[1].Value.ToString();
            string basimevi = dataGridView1.Rows[secilialan].Cells[2].Value.ToString();
            string sayfa = dataGridView1.Rows[secilialan].Cells[3].Value.ToString();
            textBox1.Text = kitapad;
            textBox2.Text = kitapyazar;
            textBox3.Text = basimevi;
            textBox4.Text= sayfa;


        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select *from kitapbilgi where kitapad like '%" + textBox6.Text + "%'", baglan);
            SqlDataAdapter da= new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource= ds.Tables[0];
            baglan.Close();
        }
    }
}

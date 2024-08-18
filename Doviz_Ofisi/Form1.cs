using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;

namespace Doviz_Ofisi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       SqlConnection baglan=new SqlConnection("Data Source=DESKTOP-LO1OC4N\\SQLEXPRESS01;Initial Catalog=XML_Doviz_Ofis;Integrated Security=True");
        public void veri()
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("select * from kur", baglan);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
          
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string bugun = "https://www.tcmb.gov.tr/kurlar/today.xml";
            var xmldosya=new XmlDocument();
            xmldosya.Load(bugun);

            string dolarAlis = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
            lblDolarAlisKur.Text = dolarAlis;

            string dolarSatis = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
            lblDolarSatisKur.Text = dolarSatis;

            string euroAlis = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerXml;
            lblEuroAlisKur.Text = euroAlis;

            string euroSatis = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;
            lblEuroSatisKur.Text = euroSatis;
        }

        private void lblDolarAlis_Click(object sender, EventArgs e)
        {
            txtKur.Text = lblDolarAlisKur.Text;
        }

        private void lblDolarSatis_Click(object sender, EventArgs e)
        {
            txtKur.Text = lblDolarSatisKur.Text;
        }

        private void lblEuroAlis_Click(object sender, EventArgs e)
        {
            txtKur.Text = lblEuroAlisKur.Text;

        }

        private void lblEuroSatis_Click(object sender, EventArgs e)
        {
            txtKur.Text = lblEuroSatisKur.Text;

        }

        private void btnSatisYap_Click(object sender, EventArgs e)
        {
            double kur, miktar, tutar;
            kur=Convert.ToDouble(txtKur.Text);
            miktar=Convert.ToDouble(txtMiktar.Text);
            tutar = kur * miktar;
            txtTutar.Text=tutar.ToString();
            baglan.Open();

        }

        private void txtKur_TextChanged(object sender, EventArgs e)
        {
            txtKur.Text = txtKur.Text.Replace(".", ".");
        }

        private void btnSatisYap2_Click(object sender, EventArgs e)
        {
            double kur=Convert.ToDouble(txtKur.Text);
            int miktar = Convert.ToInt32(txtMiktar.Text);
            int tutar =Convert.ToInt32(miktar / kur);
            txtTutar.Text= tutar.ToString();
            int kalan;
            kalan = miktar % tutar;
            txtKalan.Text= kalan.ToString();    

        }
    }
}

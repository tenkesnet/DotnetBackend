using ADODB;
using Dapper;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JScript;
using MyDataGrid.Services;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.Formula.PTG;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Convert = System.Convert;

namespace MyDataGrid
{
    public partial class MainForm : Form
    {
        Procedures procedures=new Procedures();
        public int index;
        string filePath;
        MainForm formMain;
        AnyExcelForm anyExcelForm;
        public TabControl tabControlTabellak = new TabControl();
        ComboBox cmbBetoltAdatbazisbol = new ComboBox();

        public MainForm()
        {
            InitializeComponent();
            
        }
        public void button1_Click(object sender, EventArgs e)
        {
            tabControlTabellak.TabPages.Clear();
            this.Controls.Remove(tabControlTabellak);
            filePath = procedures.dialogOpen(openFileDialog1);

            tabControlTabellak = procedures.tabellakFeltoltese(filePath);
            this.Controls.Add(tabControlTabellak);

            tabControlTabellak.SelectedIndexChanged += SelectedIndexChanged; //A létrehozott TabControl lapjainak változásakor a SelectedIndexChanged metódust hívja meg
        }
        public void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        private void button2_ClickAsync(object sender, EventArgs e)
        {
            if (filePath == null)
                return;
            procedures.insertToDatabase(filePath);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            cmbBetoltAdatbazisbol= procedures.cmbBetolt();
            this.Controls.Add(cmbBetoltAdatbazisbol);
            cmbBetoltAdatbazisbol.SelectedIndexChanged += CmbBetoltAdatbazisbol_SelectedIndexChanged;
        }
        private void CmbBetoltAdatbazisbol_SelectedIndexChanged(object sender, EventArgs e)
        {          
            //tabControlTabellak= procedures.cmbSelectedIndexChanged(tabControlTabellak);
            this.Controls.Add(tabControlTabellak);
        }

        public void SelectedIndexChanged(Object sender, EventArgs e)  //A TabControl lapjainak változásakor hívódik meg
        {
            if (tabControlTabellak.SelectedIndex > -1)
            {
                double[] stat = new double[3];
                //stat = procedures.statisztika(tabControlTabellak.SelectedIndex);
                textBox1.Text= Convert.ToString(stat[0]);
                textBox2.Text = Convert.ToString(stat[1]);
                textBox3.Text = Convert.ToString(stat[2]);
            }
        }

        public void button4_Click(object sender, EventArgs e)
        {
            anyExcelForm = new AnyExcelForm();
            anyExcelForm.Show();
        }

    }
}
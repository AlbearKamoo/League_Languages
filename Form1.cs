using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LLC_Csharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Dictionary<String, String> langList = new Dictionary<string, string>();
            langList.Add("cs_CZ", "Čeština");
            langList.Add("nl_NL", "Deutsch");
            langList.Add("el_Gr", "Ελληνικά");
            langList.Add("en_US", "English");
            langList.Add("es_ES", "Español(España)");
            langList.Add("es_MX", "Español(Latinoamérica)");
            langList.Add("fr_FR", "Français");
            langList.Add("it_IT", "Italiano");
            langList.Add("hu_HU", "Magyar");
            langList.Add("pl_PL", "Polski");
            langList.Add("pt_BR", "Português");
            langList.Add("ro_RO", "Română");
            langList.Add("ru_RU", "Русский");
            langList.Add("tr_TR", "Türkçe");

            listBox1.DataSource = new BindingSource(langList, null);
            listBox1.DisplayMember = "Value";
            listBox1.ValueMember = "Key";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdialog = new FolderBrowserDialog();
            fdialog.Description = "Please select the Riot Games folder:";
            fdialog.ShowNewFolderButton = false;

            fdialog.SelectedPath = "C:\\Riot Games";

            if (fdialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fdialog.SelectedPath.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string releasePath = textBox1.Text + "\\League of Legends\\RADS\\solutions\\lol_game_client_sln\\releases";


            try
            {
                string[] patchDirectory = Directory.GetDirectories(releasePath);

                string solutionsFilePath = patchDirectory[0] + "\\deploy\\DATA\\cfg\\defaults\\locale.cfg";
                string systemFilePath = textBox1.Text + "\\League of Legends\\RADS\\system\\locale.cfg";
            
                string[] lines = { "[General]", "LanguageLocaleRegion=" + listBox1.SelectedValue };
                System.IO.File.WriteAllLines(@solutionsFilePath, lines);
                System.IO.File.WriteAllText(@systemFilePath, "locale = " + listBox1.SelectedValue);
            }
            catch (DirectoryNotFoundException except)
            {
                MessageBox.Show("Incorrect root folder!\t\t", "Directory error", MessageBoxButtons.OK);
            }

            this.Close();
        }

    }
}

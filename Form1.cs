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

            // Dictionary containing all the currently supported languages in Leaugue of Legends
            // The key represents the language code while the value is a string representing how 
            // the language would appear in the client language options
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

            // listBox that takes the previuosly defined dictionary as a data source
            // Allows the user to easily select the preferred language
            listBox1.DataSource = new BindingSource(langList, null);
            listBox1.DisplayMember = "Value";
            listBox1.ValueMember = "Key";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdialog = new FolderBrowserDialog();
            fdialog.Description = "Please select the Riot Games folder:";
            fdialog.ShowNewFolderButton = false;
            
            // Sets the default directory to the default folder path provided in the League of Legends installation
            fdialog.SelectedPath = "C:\\Riot Games";

            if (fdialog.ShowDialog() == DialogResult.OK)
            {
                // Sets the text box to contain the user's selected directory
                textBox1.Text = fdialog.SelectedPath.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Saves the first part of the necessary file paths by adding the user povided
            // directory to a string of folders that stays constant throughout 
            // League of Legends client updates
            string releasePath = textBox1.Text + "\\League of Legends\\RADS\\solutions\\lol_game_client_sln\\releases";

            // Exception handling for the provided directory
            try
            {
                // Gets the contents contained in the previously defined releases directory
                string[] patchDirectory = Directory.GetDirectories(releasePath);

                // Gets the path of the first folder contained in the releases folder, whose name changes with every
                // update to the League of Legends client, and adds the last part of the necessary file paths to it.
                string solutionsFilePath = patchDirectory[0] + "\\deploy\\DATA\\cfg\\defaults\\locale.cfg";
                string systemFilePath = textBox1.Text + "\\League of Legends\\RADS\\system\\locale.cfg";
                
                // Performs the necessary modifications to the text files that dictate the game's language options
                string[] lines = { "[General]", "LanguageLocaleRegion=" + listBox1.SelectedValue };
                System.IO.File.WriteAllLines(@solutionsFilePath, lines);
                System.IO.File.WriteAllText(@systemFilePath, "locale = " + listBox1.SelectedValue);
            }
            catch (DirectoryNotFoundException)
            {
                // Handles the case in which the required folders could not be found, and relays this to the user.
                MessageBox.Show("Incorrect root folder!\t\t", "Directory error", MessageBoxButtons.OK);
            }

            this.Close();
        }

    }
}

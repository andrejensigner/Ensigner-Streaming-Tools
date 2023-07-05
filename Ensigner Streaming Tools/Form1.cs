using System.Diagnostics;
using Newtonsoft.Json;

namespace Ensigner_Streaming_Tools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class AppSettings
        {
            public string BackgroundImagePath { get; set; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private AppSettings LoadSettings() // NaËÌta s˙bor s konfigur·ciou
        {
            string filePath = "settings.json";
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<AppSettings>(json);
            }
            else
            {
                return new AppSettings();
            }
        }

        private void SaveSettings(AppSettings settings) // UloûÌ s˙bor s konfigur·ciou
        {
            string filePath = "settings.json";
            string json = JsonConvert.SerializeObject(settings);
            File.WriteAllText(filePath, json);
        }

        private string obsPath = @"C:\Program Files\obs-studio\bin\64bit\"; // Reùazcov· premenn·

        private void button1_Click(object sender, EventArgs e) // SpustÌ OBS
        {
            try
            {
                // Spustenie OBS
                string obsExecutable = "obs64.exe";

                ProcessStartInfo startOBS = new ProcessStartInfo();
                startOBS.FileName = Path.Combine(obsPath, obsExecutable);
                startOBS.WorkingDirectory = obsPath;
                startOBS.Verb = "runas"; // Spustiù ako spr·vcu

                Console.WriteLine("Aktualna zloûka OBS: " + obsPath);

                Process.Start(startOBS);
            }
            catch (Exception ex)
            {
                string errorMessage = "Chyba: " + ex.Message + "\nNastala neËakan· chyba!";
                MessageBox.Show(errorMessage, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button2_Click(object sender, EventArgs e) // ZmenÌ cestu k OBS
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Zvolte umiestnenie OBS";
            openFileDialog.Filter = "Executable Files|*.exe";
            openFileDialog.InitialDirectory = obsPath; // Aktu·lna cesta k OBS

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                obsPath = Path.GetDirectoryName(openFileDialog.FileName);
                Console.WriteLine("NovÈ umiestnenie OBS: " + obsPath);
            }
        }

        private void button3_Click(object sender, EventArgs e) // Zmena pozadia
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select Image";
            openFileDialog.Filter = "Image Files|*.jpg;*.png;*.gif;*.bmp";

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;
                this.BackgroundImage = Image.FromFile(imagePath);
                this.BackgroundImageLayout = ImageLayout.Stretch;

                AppSettings settings = LoadSettings();
                settings.BackgroundImagePath = imagePath;
                SaveSettings(settings);
            }
        }
    }
}
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace BTAccelerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        if (sk.GetValue("DisplayName") as string == "BATTLETECH")
                        {
                            _BaseDirectory = sk.GetValue("InstallLocation") as string;
                        }
                    }
                }
            }
        }

        private string _BaseDirectory = @"E:\Games\SteamLibrary\steamapps\common\BATTLETECH";

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            double multiplier;
            try
            {
                multiplier = double.Parse(txtMultiplier.Text);
            }
            catch (Exception)
            {
                MessageBox.Show($"Can't understand multiplier entry: {txtMultiplier.Text}");
                return;
            }

            string movementSubdir = @"BattleTech_Data\StreamingAssets\data\movement";
            string[] jsonFilenames;
            try
            {
                jsonFilenames = Directory.GetFiles($"{_BaseDirectory}\\{movementSubdir}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"JSON file list failed: {ex.ToString()}");
                return;
            }

            foreach (string jsonFilename in jsonFilenames)
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(MechMovement));

                using (FileStream fs = new FileStream(jsonFilename, FileMode.Open))
                {
                    MechMovement mech = serializer.ReadObject(fs) as MechMovement;
                    if (mech == null)
                    {
                        MessageBox.Show($"Tried to edit a non mech movement file {Path.GetFileName(jsonFilename)}, bailing");
                        return;
                    }
                    mech.WalkVelocity *= multiplier;
                    mech.RunVelocity *= multiplier;
                    mech.SprintVelocity *= multiplier;
                    mech.LimpVelocity *= multiplier;
                    mech.WalkAcceleration *= multiplier;
                    mech.SprintAcceleration *= multiplier;
                    fs.SetLength(0);
                    serializer.WriteObject(fs, mech);
                }
            }
        }
    }
}

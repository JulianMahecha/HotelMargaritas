using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Group5
{
    /// <summary>
    /// Logica para NearbyHoosierHotels.xaml
    /// </summary>
    public partial class NearbyHoosierHotels : Window
    {
        public NearbyHoosierHotels()
        {
            InitializeComponent();
        }

        //Redirigir a los usuarios al menú principal.
        private void btnReturnToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newMai = new MainWindow();
            newMai.Show();
            this.Close();
        }

        //Reservas basadas en ciudad seleccionada y validación.
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            if (cboZipCode.SelectedIndex == 1)
            {
                txtOutput.Text = "===============================================================" + Environment.NewLine + "==================== HOTELES LAS MARGARITAS ==================="
                + Environment.NewLine + "===============================================================" + Environment.NewLine + "Direccion".PadRight(22) + "Telefono".PadRight(12) + "Movil".PadRight(20)
                + "Barrio".PadRight(16) + Environment.NewLine + "Calle 46 No. 23b-27".PadRight(22) + "7141527".PadRight(12) + "317-789-8989".PadRight(20)
                    + "Rosales".PadRight(16) + Environment.NewLine + "Av 68 N. 23-42".PadRight(22) + "2043227".PadRight(12) + "317-456-2154".PadRight(20)
                    + "Chapinero".PadRight(16) + Environment.NewLine + "Calle 127 No 12-65".PadRight(22) + "9154782".PadRight(12) + "812-617-2200".PadRight(20)
                    + "Candelaria".PadRight(16) + Environment.NewLine + "Calle 102 No. 25-23".PadRight(22) + "2030506".PadRight(12) + "812-541-7100".PadRight(20)
                    + "Chico".PadRight(16) + Environment.NewLine;
            }
            else if (cboZipCode.SelectedIndex == 2)
            {
                txtOutput.Text = "===============================================================" + Environment.NewLine + "==================== HOTELES LAS MARGARITAS ==================="
                + Environment.NewLine + "===============================================================" + Environment.NewLine + "Direccion".PadRight(22) + "Telefono".PadRight(12) + "Movil".PadRight(20)
                + "Barrio".PadRight(16) + Environment.NewLine + "Calle 50 No. 23b-27".PadRight(22) + "2014569".PadRight(12) + "317-789-9999".PadRight(20)
                    + "El Poblado".PadRight(16) + Environment.NewLine + "Av 68 N. 23-42".PadRight(22) + "2045227".PadRight(12) + "317-456-1818".PadRight(20)
                    + "Uribia".PadRight(16) + Environment.NewLine + "Av 25 No 23-89".PadRight(22) + "7142369".PadRight(12) + "812-617-2200".PadRight(20)
                    + "La Palma".PadRight(16) + Environment.NewLine + "Calle 10 No. 25-23".PadRight(22) + "2030506".PadRight(12) + "812-541-2320".PadRight(20)
                    + "Andurez".PadRight(16) + Environment.NewLine;
            }
            else if (cboZipCode.SelectedIndex == 3)
            {
                txtOutput.Text = "===============================================================" + Environment.NewLine + "==================== HOTELES LAS MARGARITAS ==================="
                + Environment.NewLine + "===============================================================" + Environment.NewLine + "Direccion".PadRight(22) + "Telefono".PadRight(12) + "Movil".PadRight(20)
                + "Barrio".PadRight(16) + Environment.NewLine + "Carrera 7, nº 27-42".PadRight(22) + "2068974".PadRight(12) + "317-789-8989".PadRight(20)
                    + "La Granja".PadRight(16) + Environment.NewLine + "Dg 21 No. 52-69".PadRight(22) + "7156987".PadRight(12) + "317-456-1874".PadRight(20)
                    + "San Blas".PadRight(16) + Environment.NewLine + "Km2 Via Sta Marta".PadRight(22) + "2365410".PadRight(12) + "812-617-2330".PadRight(20)
                    + "Playa Blanca".PadRight(16) + Environment.NewLine + "Cra 28 no. 26-27".PadRight(22) + "9080203".PadRight(12) + "812-541-3640".PadRight(20)
                    + "Andesino".PadRight(16) + Environment.NewLine;
            }
            else
            {
                MessageBox.Show("Por favor selecciona una ciudad.");
            }
        }

        //Borrar entrada de usuario e informe
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cboZipCode.SelectedIndex = 0;
            txtOutput.Text = "";
        }
    }
}

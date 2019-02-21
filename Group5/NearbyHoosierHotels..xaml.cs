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

            if (cboZipCode.SelectedIndex == 0)
            {
                txtOutput.Text = "===============================================================" + Environment.NewLine + "================ HOTELES LAS MARGARITAS ONE KING =============="
                + Environment.NewLine + "===============================================================" + Environment.NewLine
                + "Cama sencilla con colchon ortopedico".PadRight(16) + Environment.NewLine + "Servicio de tv por cable".PadRight(16) + Environment.NewLine + "WiFi 5".PadRight(16) 
                + Environment.NewLine;
            }
            else if (cboZipCode.SelectedIndex == 1)
            {
                txtOutput.Text = "===============================================================" + Environment.NewLine + "============= HOTELES LAS MARGARITAS ONE KING DELUXE ============"
                + Environment.NewLine + "===============================================================" + Environment.NewLine
                + "Cama doble con colchon ortopedico".PadRight(16) + Environment.NewLine + "Servicio de tv por Satelital".PadRight(16) + Environment.NewLine + "WiFi 5".PadRight(16)
                + Environment.NewLine + "Tina de Baño".PadRight(16)+ Environment.NewLine + "Esencias Para Baño".PadRight(16);
            }
            else if (cboZipCode.SelectedIndex == 2)
            {
                txtOutput.Text = "===============================================================" + Environment.NewLine + "============= HOTELES LAS MARGARITAS TWO QUEENS ============"
                + Environment.NewLine + "===============================================================" + Environment.NewLine
                + "Cama doble con colchon ortopedico Antialergenico".PadRight(16) + Environment.NewLine + "Servicio de tv por Satelital".PadRight(16) + Environment.NewLine + "WiFi 15MB".PadRight(16)
                + Environment.NewLine + "Jacuzzi".PadRight(16) + Environment.NewLine + "Esencias Para Baño".PadRight(16) + Environment.NewLine + "Netflix HD".PadRight(16);
            }
            else if (cboZipCode.SelectedIndex == 3)
            {
                txtOutput.Text = "===============================================================" + Environment.NewLine + "=========== HOTELES LAS MARGARITAS TWO QUEENS DELUXE ==========="
                + Environment.NewLine + "===============================================================" + Environment.NewLine
                + "Cama King Size con colchon ortopedico Antialergenico".PadRight(16) + Environment.NewLine + "Servicio de tv Satelital".PadRight(16) + Environment.NewLine + "WiFi 15MB".PadRight(16)
                + Environment.NewLine + "Jacuzzi".PadRight(16) + Environment.NewLine + "Esencias Para Baño".PadRight(16) + Environment.NewLine + "Netflix UHD".PadRight(16) + Environment.NewLine 
                + "Cinta de Correr".PadRight(16) + "Servicio Dog Friendly".PadRight(16);
            }
            else if (cboZipCode.SelectedIndex == 4)
            {
                txtOutput.Text = "===============================================================" + Environment.NewLine + "=========== HOTELES LAS MARGARITAS ONE KING SUITE ==========="
                + Environment.NewLine + "===============================================================" + Environment.NewLine
                + "Cama King Size con colchon ortopedico Antialergenico".PadRight(16) + Environment.NewLine + "Servicio de tv Satelital".PadRight(16) + Environment.NewLine + "WiFi 45MB".PadRight(16)
                + Environment.NewLine + "Jacuzzi".PadRight(16) +Environment.NewLine+ "Esencias Para Baño".PadRight(16) + Environment.NewLine+ "Netflix UHD & HULU".PadRight(16) + Environment.NewLine + "Cinta de Correr".PadRight(16)
                + Environment.NewLine + "Servicio Dog Friendly".PadRight(16) + Environment.NewLine + "Servicio Dog Friendly".PadRight(16) + Environment.NewLine + "Nevera Inroom".PadRight(16) + Environment.NewLine + 
                "Vista Preferencial".PadRight(16);
            }
            else if (cboZipCode.SelectedIndex == 5)
            {
                txtOutput.Text = "===============================================================" + Environment.NewLine + "=========== HOTELES LAS MARGARITAS ONE KING PRESIDENTIAL SUITE ==========="
                + Environment.NewLine + "===============================================================" + Environment.NewLine
                + "Cama King Size con colchon ortopedico Antialergenico, Espacio Amoblado tipo galeria y Chimenea".PadRight(16) + Environment.NewLine + "Servicio de tv Satelital".PadRight(16) + Environment.NewLine + "WiFi 200MB".PadRight(16)
                + Environment.NewLine + "Jacuzzi y Sauna Indoor".PadRight(16) + " , Esencias Para Baño".PadRight(16) + Environment.NewLine + "Netflix UHD, HULU y HBO".PadRight(16) + Environment.NewLine + "Cinta de Correr".PadRight(16)
                + Environment.NewLine + "Servicio Dog Friendly".PadRight(16) + Environment.NewLine + "Servicio Dog Friendly".PadRight(16) + Environment.NewLine + "Nevera Inroom".PadRight(16) + Environment.NewLine +
                "Vista Preferencial con Balcon".PadRight(16);
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

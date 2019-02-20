//Miembros del Equipo: Cristofer Sanchez, Julian Mahecha, Julian Vega, Mauricio Medina
//Image Sources: https://thenounproject.com/, https://s-ec.bstatic.com/images/hotel/max1024x768/587/58772381.jpg

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.InteropServices;

namespace Group5
{
    /// 
    /// Interacción Logica de la ventana Principal
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Establece la fecha y hora actual cuando se abre la aplicación.
            DateTime datCurrentDate = DateTime.Now;
            DateTime datCurrentTime = DateTime.Now;
            string strCurrentDate, strCurrentTime;

            strCurrentDate = datCurrentDate.ToLongDateString();
            lblCurrentDate.Content = strCurrentDate;

            strCurrentTime = datCurrentTime.ToShortTimeString();
            lblCurrentTime.Content = strCurrentTime;


            //Mensajes de bienvenida a usuarios
            if (DateTime.Now.Hour < 12 && DateTime.Now.Hour > 4)
            {
                lblGreeting.Content = "Buen Dia!";
            }
            else if (DateTime.Now.Hour < 18)
            {
                lblGreeting.Content = "Buena Tarde!";
            }
            else
            {
                lblGreeting.Content = "Buena Noche!";
            }
        }

        //Navegacion entre ventanas
        private void btnNewReservation_Click(object sender, RoutedEventArgs e)
        {
            QuoteReview newQuo = new QuoteReview();
            newQuo.Show();
            this.Close();
        }

        private void btnRoomManagement_Click(object sender, RoutedEventArgs e)
        {
            RoomManagement newRoo = new RoomManagement();
            newRoo.Show();
            this.Close();
        }

        private void btnReservationsReport_Click(object sender, RoutedEventArgs e)
        {
            ReservationReport newRep = new ReservationReport();
            newRep.Show();
            this.Close();
        }

        private void btnHotelesMargaritas_Click(object sender, RoutedEventArgs e)
        {
            NearbyHoosierHotels newNea = new NearbyHoosierHotels();
            newNea.Show();
            this.Close();
        }

        //Cerrar Ventana Principal
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

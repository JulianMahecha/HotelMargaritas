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
using Newtonsoft.Json;
using System.IO;

namespace Group5
{
    /// <summary>
    /// Logica Para QuoteReview.xaml
    /// </summary>
    public partial class QuoteReview : Window
    {
        //Propiedades
        public Reservation CurReservation { get; set; }

        //Variables Globales
        List<Room> roomList;
        List<Reservation> reservationList;
        Boolean bolRoomAvailable;

        //Metodo Constructor
        public QuoteReview()
        {
            InitializeComponent();
            roomList = new List<Room>();
            reservationList = new List<Reservation>();
            bolRoomAvailable = false;
            LoadReservations();
            LoadRooms();            
        }

        //Limpiar Formulario
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cbbRoomType.SelectedIndex = -1;
            txtNumberOfRooms.Text = "";
            dtpCheckInDate.Text = "";
            dtpCheckOutDate.Text = "";
            bolRoomAvailable = false;
            lblRoomQuantity.Visibility = Visibility.Hidden;
            lblRoomRates.Visibility = Visibility.Hidden;
            lblRoomAvailability.Visibility = Visibility.Hidden;
            lblSubtotal.Visibility = Visibility.Hidden;
            lblTax.Visibility = Visibility.Hidden;
            lblConvenienceFee.Visibility = Visibility.Hidden;
            lblTotal.Visibility = Visibility.Hidden;
            lblRoomQuantityResult.Content = "";
            lblRoomRatesResult.Content = "";
            lblRoomAvailabilityResult.Content = "";
            lblSubtotalResult.Content = "";
            lblTaxResult.Content = "";
            lblConvenienceFeeResult.Content = "";
            lblTotalResult.Content = "";
        }

        //Calcular Costo
        private void btnCalculateQuote_Click(object sender, RoutedEventArgs e)
        {
            #region Declaracion variables
            string strRoomAvailable = "No Disponible", strRoomType;
            DateTime datCheckInDate, datCheckOutDate;
            TimeSpan tspNumberOfNights;
            int intAvailableRoomQuantity, intNumberOfNights = 0, intNumberOfRooms;
            double dblRoomPrice = 0, dblSubtotal, dblTax, dblConvenienceFee, dblTotal;
            bolRoomAvailable = false;

            #endregion

            #region Validacion Entrada Usuario
            //Validar se selecciona un tipo de habitación
            if (cbbRoomType.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor escoja una habitacion");
                return;
            }

            //Validar numero de habitaciones
            if (!int.TryParse(txtNumberOfRooms.Text, out intNumberOfRooms) || intNumberOfRooms <= 0)
            {
                MessageBox.Show("Por favor, introduzca un número válido de habitaciones.");
                return;
            }

            //Validar entrada de fecha
            if (dtpCheckInDate.SelectedDate == null)
            {
                MessageBox.Show("Por favor, introduzca una fecha de entrada.");
                return;
            }
            if (dtpCheckOutDate.SelectedDate == null)
            {
                MessageBox.Show("Por favor, introduzca una fecha de salida.");
                return;
            }

            //Validar si la fecha de entrada es en el futuro.
            if (((DateTime)dtpCheckInDate.SelectedDate - DateTime.Today).Days < 0)
            {
                MessageBox.Show("Por favor, introduzca una fecha de entrada en el futuro.");
                return;
            }

            // Validar la fecha de salida es después de la fecha de entrada
            if (dtpCheckInDate.SelectedDate >= dtpCheckOutDate.SelectedDate)
            {
                MessageBox.Show("Por favor, introduzca una fecha de salida después de la fecha de entrada.");
                return;
            }
            #endregion

            //Verificar Disponibilidad de Cuartos
            ComboBoxItem cbiSelectedItem = (ComboBoxItem)cbbRoomType.SelectedItem;
            strRoomType = cbiSelectedItem.Content.ToString();

            //Validar si hay alguna habitación disponible durante la fecha deseada.
            bolRoomAvailable = CheckRoomAvailability(strRoomType, intNumberOfRooms);

            if (bolRoomAvailable)
            { 
                strRoomAvailable = "Disponible";
                lblRoomAvailabilityResult.Foreground = new SolidColorBrush(Color.FromRgb(9, 210, 97));
            }
            else
            {
                strRoomAvailable = "No Disponible";
                lblRoomAvailabilityResult.Foreground = new SolidColorBrush(Color.FromRgb(248, 167, 149));
            }
            
            //Obtener precio del cuarto. 
            foreach (Room room in roomList)
            {
                if (room.RoomType == strRoomType)
                {
                    dblRoomPrice = room.RoomPrice;
                    intAvailableRoomQuantity = room.RoomQuantity;
                }
            }

            //Calcular numero de noches.
            datCheckInDate = (DateTime)dtpCheckInDate.SelectedDate;
            datCheckOutDate = (DateTime)dtpCheckOutDate.SelectedDate;
            tspNumberOfNights = datCheckOutDate - datCheckInDate;
            intNumberOfNights = tspNumberOfNights.Days;

            //Calcular Subtotal
            dblSubtotal = dblRoomPrice * intNumberOfNights * intNumberOfRooms;

            //Calcular Iva
            dblTax = dblSubtotal * 0.19;

            //Calcular Grabamen Hospedaje
            dblConvenienceFee = 5000 * intNumberOfNights;

            //Calcular Total
            dblTotal = dblSubtotal + dblTax + dblConvenienceFee;

            //Mostrar resultado a los usuarios
            lblRoomQuantity.Visibility = Visibility.Visible;
            lblRoomRates.Visibility = Visibility.Visible;
            lblRoomAvailability.Visibility = Visibility.Visible;
            lblSubtotal.Visibility = Visibility.Visible;
            lblTax.Visibility = Visibility.Visible;
            lblConvenienceFee.Visibility = Visibility.Visible;
            lblTotal.Visibility = Visibility.Visible;

            lblRoomQuantityResult.Content = txtNumberOfRooms.Text;
            lblRoomRatesResult.Content = dblRoomPrice.ToString("C2") + " x " + intNumberOfNights.ToString() + " noche(s)";
            lblRoomAvailabilityResult.Content = strRoomAvailable;
            lblSubtotalResult.Content = dblSubtotal.ToString("C2");
            lblTaxResult.Content = dblTax.ToString("C2");
            lblConvenienceFeeResult.Content = dblConvenienceFee.ToString("C2");
            lblTotalResult.Content = dblTotal.ToString("C2");

            CurReservation = new Reservation((DateTime)dtpCheckInDate.SelectedDate, intNumberOfNights, strRoomType, intNumberOfRooms,
                                             dblRoomPrice, dblSubtotal, dblTax, dblConvenienceFee, dblTotal);
        }

        //Redirigir a los usuarios a la nueva ventana de reserva para continuar con su reserva.
        private void btnMakeReservation_Click(object sender, RoutedEventArgs e)
        {
            if (bolRoomAvailable)
            {
                NewReservation newRes = new NewReservation(CurReservation);
                newRes.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("La habitación no está disponible. Por favor ingrese otra habitación para revisar. Debe revisar la cotización antes de crear la reserva de habitación.");
                return;
            }
        }

        //Redirigir a los usuarios al menú principal.
        private void btnReturnToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newMai = new MainWindow();
            newMai.Show();
            this.Close();
        }

        //Validar fecha de entrada
        private void dtpCheckInDate_SelectedDateChange(object sender, SelectionChangedEventArgs e)
        {
            if (dtpCheckInDate.SelectedDate < DateTime.Today)
            {
                MessageBox.Show("Por favor ingrese una fecha de check-in en el futuro.");
                dtpCheckInDate.SelectedDate = null;
                return;
            }
        }

        //Validar fecha de salida
        private void dtpCheckOutDate_SelectedDateChange(object sender, SelectionChangedEventArgs e)
        {
            if (dtpCheckOutDate.SelectedDate <= DateTime.Today)
            {
                MessageBox.Show("Por favor, introduzca una fecha de salida en el futuro.");
                dtpCheckOutDate.SelectedDate = null;
                return;
            }

            if (dtpCheckOutDate.SelectedDate < dtpCheckInDate.SelectedDate)
            {
                MessageBox.Show("Introduzca una fecha de salida posterior a la fecha de entrada.");
                dtpCheckOutDate.SelectedDate = null;
                return;
            }
        }

        #region Helper Functions
        //Cargando en el archivo JSON para lista de habitaciones.
        private void LoadRooms()
        {
            string strFilePath = @"..\..\Data\Rooms.json";

            try
            {
                StreamReader reader = new StreamReader(strFilePath);
                string jsonData = reader.ReadToEnd();
                reader.Close();
                roomList = JsonConvert.DeserializeObject<List<Room>>(jsonData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando el proceso " + ex.Message);
            }
        }

        //Cargando en el archivo JSON para la lista de reservas
        private void LoadReservations()
        {
            string strFilePath = @"..\..\Data\Reservations.json";

            try
            {
                StreamReader reader = new StreamReader(strFilePath);
                string jsonData = reader.ReadToEnd();
                reader.Close();
                reservationList = JsonConvert.DeserializeObject<List<Reservation>>(jsonData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando el proceso: " + ex.Message);
            }
        }

        //Comprueba si hay espacio disponible dentro del rango de fechas
        private bool CheckRoomAvailability(string strRoomType, int intNumberOfRooms)
        {
            //Variables
            int roomQuantity = 0;
            DateTime datNewCheckInDate, datNewCheckOutDate, datReservationCheckOutDate;
            datNewCheckInDate = (DateTime)dtpCheckInDate.SelectedDate;
            datNewCheckOutDate = (DateTime)dtpCheckOutDate.SelectedDate;

            foreach (Room room in roomList)
            {
                //Obtiene el número total de habitaciones seleccionadas en el hotel.
                if (room.RoomType == strRoomType)
                    roomQuantity = room.RoomQuantity;
            }

            //Calcular Habitaciones Restantes
            foreach (Reservation reservation in reservationList)
            {
                datReservationCheckOutDate = reservation.CheckInDate.AddDays(reservation.NumberOfNights);
                if (reservation.RoomType == strRoomType &&
                    (datNewCheckInDate >= reservation.CheckInDate && datNewCheckInDate < datReservationCheckOutDate) ||
                    (datNewCheckOutDate > reservation.CheckInDate && datNewCheckOutDate <= datReservationCheckOutDate))
                {
                    roomQuantity--;
                }
            }

            if (roomQuantity - intNumberOfRooms >= 0)
                return true;
            return false;
        }
        #endregion
    }
}

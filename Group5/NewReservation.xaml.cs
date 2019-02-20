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
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.IO;

namespace Group5
{
    /// <summary>
    /// Logica para NewReservation.xaml
    /// </summary>
    public partial class NewReservation : Window
    {
        #region Propiedades y Variables
        //Propiedades
        public Reservation CurReservation { get; set; }

        //Variables Globales
        bool bolValidCreditCard = false;
        List<Reservation> reservationList = new List<Reservation>();
        #endregion

        #region Metodos Constructores
        public NewReservation()
        {
            InitializeComponent();
            imgCreditCardLogo.Visibility = Visibility.Hidden;
            LoadFile();
        }

        public NewReservation(Reservation reservation)
        {
            InitializeComponent();
            imgCreditCardLogo.Visibility = Visibility.Hidden;
            LoadFile();
            CurReservation = reservation;
            lblRoomQuantityResult.Content = CurReservation.NumberOfRooms.ToString();
            lblRoomRatesResult.Content = CurReservation.RoomPrice.ToString("C2") + " x " + CurReservation.NumberOfNights.ToString() + " noche(s)";
            lblSubtotalResult.Content = CurReservation.SubTotal.ToString("C2");
            lblTaxResult.Content = CurReservation.Tax.ToString("C2");
            lblConvenienceFeeResult.Content = CurReservation.ConvenienceFee.ToString("C2");
            lblTotalResult.Content = CurReservation.Total.ToString("C2");
        }
        #endregion  

        private void btnCreateReservation_Click(object sender, RoutedEventArgs e)
        {
            #region User Input Validation
            //Validate all entries (except email) is filled
            if (txtFirstName.Text == "" ||
                txtLastName.Text == "" ||
                txtCreditCardNumber.Text == "" ||
                txtPhone.Text == "")
            {
                MessageBox.Show("Please enter the required information (marked by *).");
                return;
            }

            //Validate phone number
            string strPhoneNumber, strEmail;
            strPhoneNumber = txtPhone.Text;
            if (strPhoneNumber.Length != 10 || !strPhoneNumber.All(Char.IsDigit))
            {
                MessageBox.Show("Please enter a valid phone number.");
                return;
            }

            //Validate email address
            strEmail = txtEmail.Text;
            if (!IsValidEmailAddress(strEmail))
            {
                MessageBox.Show("Please enter a valid email address or leave it blank.");
                return;
            }

            //Validate credit card
            if (!bolValidCreditCard || lblCreditCardType.ContentStringFormat == "")
            {
                MessageBox.Show("Please enter a valid credit card information.");
                return;
            }
            #endregion

            //Establecer los detalles del cliente para la reserva.
            CurReservation.setCustomerDetails(txtFirstName.Text, txtLastName.Text, lblCreditCardTypeResult.ContentStringFormat,
                                              txtCreditCardNumber.Text, txtPhone.Text, txtEmail.Text);

            //Muestra un cuadro de mensaje que muestra toda la información para confirmación.
            MessageBoxResult messageBoxResult = MessageBox.Show("Por favor confirme los detalles de la reserva"
                + Environment.NewLine + Environment.NewLine + CurReservation.ToString()
                , "Reserva"
                , MessageBoxButton.YesNo);

            //Agregar reserva al archivo de reserva y redirigir al usuario a la revisión de cotización
            //Ventana para preparar una nueva reserva.
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                reservationList.Add(CurReservation);
                AppendToFile(CurReservation);
                ClearUserInput();
                MessageBox.Show("Nueva reserva creada!");
                QuoteReview quoRev = new QuoteReview();
                quoRev.Show();
                this.Close();
            }
        }

        //Valida el número de tarjeta de crédito para permitir solo números válidos de tarjeta de crédito
        private void txtCreditCardNumber_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            //Variables
            string strCardNum = txtCreditCardNumber.Text.Trim().Replace(" ", ""), strCardType;
            long lngOut;
            int intCheckDigit, intCheckSum = 0;

            //Validar la entrada es todos los números
            if (!Int64.TryParse(strCardNum, out lngOut))
            {
                MessageBox.Show("Número de tarjeta de crédito no válido. Por favor solo inserte numeros.");
                txtCreditCardNumber.Background = new SolidColorBrush(Color.FromRgb(255, 200, 200));
                txtCreditCardNumber.Text = "";
                return;
            }

            //Valida que hay 13, 15, 16 dígitos ingresados
            if (strCardNum.Length != 13 && strCardNum.Length != 15 && strCardNum.Length != 16)
            {
                MessageBox.Show("Los números de tarjetas de crédito deben contener 13, 15 o 16 dígitos.");
                txtCreditCardNumber.Background = new SolidColorBrush(Color.FromRgb(255, 200, 200));
                txtCreditCardNumber.Text = "";
                return;
            }

            //Determina el tipo de tarjeta a partir del prefijo y establece la variable de tipo de tarjeta
            if (strCardNum.StartsWith("34") || strCardNum.StartsWith("37"))
                strCardType = "American Express";
            else if (strCardNum.StartsWith("6011"))
                strCardType = "Discover";
            else if (strCardNum.StartsWith("51") || strCardNum.StartsWith("52") || strCardNum.StartsWith("53") || strCardNum.StartsWith("54") || strCardNum.StartsWith("55"))
                strCardType = "MasterCard";
            else if (strCardNum.StartsWith("4"))
                strCardType = "VISA";
            else
            {
                MessageBox.Show("Tarjeta de crédito no aceptada. Por favor proporcione un método de pago diferente.");
                txtCreditCardNumber.Text = "";
                return;
            }

            //Validar número de tarjeta
            strCardNum = ReverseString(strCardNum);
            for (int i = 0; i < strCardNum.Length; i++)
            {
                intCheckDigit = Convert.ToInt32(strCardNum.Substring(i, 1));
                if ((i + 1) % 2 == 0)
                {
                    intCheckDigit *= 2;
                    if (intCheckDigit > 9)
                        intCheckDigit -= 9;
                }
                intCheckSum += intCheckDigit;
            }
            if (intCheckSum % 10 == 0)
                bolValidCreditCard = true;

            //Mostrar el resultado apropiado
            if (bolValidCreditCard)
            {
                switch (strCardType)
                {
                    case "American Express":
                        imgCreditCardLogo.Source = new BitmapImage(new Uri(@"/Images/american_express_logo.png", UriKind.Relative));
                        imgCreditCardLogo.Width = 22;
                        imgCreditCardLogo.Height = 22;
                        break;
                    case "Discover":
                        imgCreditCardLogo.Source = new BitmapImage(new Uri(@"/Images/discover_logo.png", UriKind.Relative));
                        imgCreditCardLogo.Width = 32;
                        imgCreditCardLogo.Height = 22;
                        break;
                    case "MasterCard":
                        imgCreditCardLogo.Source = new BitmapImage(new Uri(@"/Images/mastercard_logo.png", UriKind.Relative));
                        imgCreditCardLogo.Width = 37;
                        imgCreditCardLogo.Height = 22;
                        break;
                    case "VISA":
                        imgCreditCardLogo.Source = new BitmapImage(new Uri(@"/Images/visa_logo.png", UriKind.Relative));
                        imgCreditCardLogo.Width = 35;
                        imgCreditCardLogo.Height = 22;
                        break;
                }
                imgCreditCardLogo.Visibility = Visibility.Visible;
                txtCreditCardNumber.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                lblCreditCardTypeResult.Content = strCardType;
            }
            else
            {
                MessageBox.Show("Por favor, introduzca un número de tarjeta de crédito válida.");
                txtCreditCardNumber.Background = new SolidColorBrush(Color.FromRgb(255, 200, 200));
                txtCreditCardNumber.Text = "";
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

        //Borrar todos los campos de entrada
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearUserInput();
        }

        //Redirigir al usuario a la ventana de revisión de cotización y permitir que el usuario ajuste los detalles de la reserva.
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            QuoteReview quoRev = new QuoteReview();
            quoRev.Show();
            this.Close();
        }

        #region Helper Functions
        //DONEValidate if email address is valid
        private bool IsValidEmailAddress(string emailaddress)
        {
            Regex rx = new Regex(
            @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
            return rx.IsMatch(emailaddress);
        }

        //DONEReturns a reversed version of the given string s 
        private string ReverseString(string s)
        {
            char[] array = s.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        //DONEClear user input
        private void ClearUserInput()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtCreditCardNumber.Text = "";
            txtPhone.Text = "";
            lblCreditCardTypeResult.Content = "";
            txtEmail.Text = "";
        }

        //DONEAppend new reservation to the database
        private void AppendToFile(Reservation reservation)
        {
            string strFilePath = @"..\..\Data\Reservations.json";

            try
            {
                string jsonData = JsonConvert.SerializeObject(reservationList);
                File.WriteAllText(strFilePath, jsonData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in append file: " + ex.Message);
                return;
            }
        }

        //DONELoading in the JSON file
        private void LoadFile()
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
                MessageBox.Show("Error in import process: " + ex.Message);
            }
        }
        #endregion
    }
}
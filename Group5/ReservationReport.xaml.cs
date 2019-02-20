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
using Newtonsoft.Json;
using System.IO;
using System.Globalization;

namespace Group5
{
    /// <summary>
    /// Logica de ReservationReport.xaml
    /// </summary>
    public partial class ReservationReport : Window
    {
        //Variables
        List<Reservation> ReservationList;
        private TimeSpan tspNumberOfNights;

        //Metodo Constructor
        public ReservationReport()
        {
            InitializeComponent();
            dtpStartDate.DisplayDate = DateTime.Today;
            dtpEndDate.DisplayDate = DateTime.Today;
            LoadFile();
        }
        //Cargando el archivo JSON
        private void LoadFile()
        {
            string jsonData = File.ReadAllText(@"..\..\Data\Reservations.json");
            ReservationList = JsonConvert.DeserializeObject<List<Reservation>>(jsonData);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //Hacer una nueva lista para los resultados de búsqueda
            List<Reservation> reservationSearch = new List<Reservation>();
            reservationSearch.Clear();
            dtgResults.Items.Refresh();

            //Creación de la cadena LastName
            string strLastName;
            strLastName = txtLastName.Text.Trim().ToLower();
            strLastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(strLastName);

            //Validar una entrada de texto para el apellido
            double dblNum;
            if (Double.TryParse(strLastName, out dblNum))
            {
                MessageBox.Show("Por favor, introduzca un valor de texto para el apellido.");
                return;
            }

            //Creando fecha para CheckInDate
            DateTime dtpStart = new DateTime();
            DateTime dtpEnd = new DateTime();

            //Validar entradas de usuario
            if (dtpStartDate.SelectedDate == null && dtpEndDate.SelectedDate == null && strLastName == "")
            {
                MessageBox.Show("Introduzca un nombre o un intervalo de fechas en el cuadro de entrada correcto.");
                return;
            }
            else if (dtpStartDate.SelectedDate != null && dtpEndDate.SelectedDate == null && strLastName == "")
            {
                MessageBox.Show("Introduzca una fecha de finalización para completar el intervalo de fechas.");
                return;
            }
            else if (dtpStartDate.SelectedDate == null && dtpEndDate.SelectedDate != null && strLastName == "")
            {
                MessageBox.Show("Introduzca una fecha de inicio para completar el intervalo de fechas.");
                return;
            }

            //Asignación de resultados al datagrid basado en criterios de búsqueda:

            //Cuando se seleccionan las fechas de inicio / finalización y el apellido está en blanco
            if (dtpStartDate.SelectedDate != null && dtpEndDate.SelectedDate != null && strLastName == "")
            {
                dtpStart = dtpStartDate.SelectedDate.Value.Date;
                dtpEnd = dtpEndDate.SelectedDate.Value.Date;
                tspNumberOfNights = dtpEnd - dtpStart;
                if (tspNumberOfNights.Days <= 0)
                {
                    MessageBox.Show("Introduzca una fecha de finalización posterior a la fecha de inicio.");
                    return;
                }

                //Asignando resultados a datagrid
                dtgResults.ItemsSource = reservationSearch;
                dtgResults.Items.Refresh();

                reservationSearch = ReservationList.Where(r =>
                    r.CheckInDate >= dtpStart &&
                    r.CheckInDate <= dtpEnd).ToList();

                dtgResults.Items.Refresh();
                dtgResults.ItemsSource = reservationSearch;
            }
            //Cuando las fechas de inicio / finalización están en blanco pero el apellido no es
            else if (dtpStartDate.SelectedDate == null && dtpEndDate.SelectedDate == null && strLastName != "")
            {
                //Asignando resultados a datagrid
                dtgResults.ItemsSource = reservationSearch;
                dtgResults.Items.Refresh();

                reservationSearch = ReservationList.Where(r =>
                    r.LastName.StartsWith(strLastName)).ToList();

                dtgResults.Items.Refresh();
                dtgResults.ItemsSource = reservationSearch;
            }
            //Cuando todas las entradas no están en blanco
            else if (dtpStartDate.SelectedDate != null && dtpEndDate.SelectedDate != null && strLastName != "")
            {
                dtpStart = dtpStartDate.SelectedDate.Value.Date;
                dtpEnd = dtpEndDate.SelectedDate.Value.Date;
                tspNumberOfNights = dtpEnd - dtpStart;
                if (tspNumberOfNights.Days <= 0)
                {
                    MessageBox.Show("Introduzca una fecha de finalización posterior a la fecha de inicio.");
                    return;
                }

                //Asignando resultados a datagrid
                dtgResults.ItemsSource = reservationSearch;
                dtgResults.Items.Refresh();

                reservationSearch = ReservationList.Where(r =>
                    r.LastName.StartsWith(strLastName) &&
                    r.CheckInDate >= dtpStart &&
                    r.CheckInDate <= dtpEnd).ToList();

                dtgResults.Items.Refresh();
                dtgResults.ItemsSource = reservationSearch;
            }
        }

        //Borrar las entradas y datagrid
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            dtgResults.ItemsSource = "";
            dtpStartDate.Text = "";
            dtpEndDate.Text = "";
            txtLastName.Text = "";
        }

        //Redirigir a los usuarios al menú principal.
        private void btnReturntoMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newMai = new MainWindow();
            newMai.Show();
            this.Close();
        }
    }
}

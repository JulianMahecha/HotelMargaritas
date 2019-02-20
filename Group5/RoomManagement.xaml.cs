using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Group5
{
    /// <summary>
    /// Logica para RoomManagement.xaml
    /// </summary>
    public partial class RoomManagement : Window
    {
        //Variable
        List<Room> roomList;

        //Metodo Constructor
        public RoomManagement()
        {
            InitializeComponent();

            //dtgResults.Columns[2].CellStyle = "C2";

            //Creando una lista para las habitaciones.
            roomList = new List<Room>();
            dtgResults.ItemsSource = roomList;

            //Borrado de entradas
            cbxRoomType.SelectedIndex = 0;
            txtPrice.Text = "";
            txtQuantity.Text = "";

            //Cargando en el archivo JSON
            string strFilePath = @"..\..\Data\Rooms.json";

            try
            {
                StreamReader reader = new StreamReader(strFilePath);
                string jsonData = reader.ReadToEnd();
                reader.Close();

                roomList = JsonConvert.DeserializeObject<List<Room>>(jsonData);

                dtgResults.ItemsSource = roomList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la importación: " + ex.Message);
            }

            dtgResults.Items.Refresh();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //Crear variables para precio, cantidad y tipo de habitación
            string strPrice, strQuantity, strRoomType;

            //Asignación de entradas a Precio / Cantidad
            strPrice = txtPrice.Text;
            strQuantity = txtQuantity.Text;

            //Validando Precio / Cantidad
            Double dblPrice;
            int intQuantity;

            if (!Double.TryParse(txtPrice.Text, out dblPrice) || dblPrice <= 0)
            {
                MessageBox.Show("Por favor ingrese un valor numérico mayor a $ 0 para el precio.");
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out intQuantity) || intQuantity <= 0)
            {
                MessageBox.Show("Por favor, ingrese un valor numérico mayor que 0 para Cantidad.");
                return;
            }

            //Asignación de entradas al tipo de habitación y creación de validación.
            if (cbxRoomType.SelectedIndex == 1)
                strRoomType = "One King";
            else if (cbxRoomType.SelectedIndex == 2)
                strRoomType = "One King Deluxe";
            else if (cbxRoomType.SelectedIndex == 3)
                strRoomType = "Two Queens";
            else if (cbxRoomType.SelectedIndex == 4)
                strRoomType = "Two Queen Deluxe";
            else if (cbxRoomType.SelectedIndex == 5)
                strRoomType = "One King Suite";
            else if (cbxRoomType.SelectedIndex == 6)
                strRoomType = "One King Presidential Suite";
            else
            {
                strRoomType = "";
                MessageBox.Show("Por favor seleccione un tipo de cuarto.");
                return;
            }


            //ForEach Loop para editar el tipo de habitación seleccionada
            foreach (Room r in roomList)
            {
                if (r.RoomType == strRoomType)
                {
                    r.RoomPrice = dblPrice;
                    r.RoomQuantity = intQuantity;
                }

                dtgResults.Items.Refresh();
            }

            //Sobrescribiendo nuestro archivo
            StreamWriter writer = new StreamWriter(@"..\..\Data\Rooms.json", false);
            string jsonData = JsonConvert.SerializeObject(roomList);
            writer.Write(jsonData);
            writer.Close();
        }

        //Borrado de entradas
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtPrice.Text = "";
            txtQuantity.Text = "";
            cbxRoomType.SelectedIndex = 0;
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

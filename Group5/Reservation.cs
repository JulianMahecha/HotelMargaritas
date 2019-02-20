using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group5
{
    public class Reservation
    {
        //Propiedades
        public DateTime CheckInDate { get; set; }
        public int NumberOfNights { get; set; }
        public string RoomType { get; set; }
        public int NumberOfRooms { get; set; }
        public double RoomPrice { get; set; }
        public double SubTotal { get; set; }
        public double Tax { get; set; }
        public double ConvenienceFee { get; set; }
        public double Total { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreditCardType { get; set; }
        public string CreditCardNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        //Metodos
        public Reservation()
        {
            //Inicializar valores por defecto para las propiedades.
            CheckInDate = DateTime.Today;
            NumberOfNights = 0;
            RoomType = "One King";
            NumberOfRooms = 0;
            RoomPrice = 80000;
            SubTotal = 0;
            Tax = 0;
            ConvenienceFee = 0;
            Total = 0;
            FirstName = "";
            LastName = "";
            CreditCardType = "";
            CreditCardNumber = "";
            Phone = "";
            Email = "";
        }

        public Reservation(DateTime checkInDate, int nights, string type,
                           int rooms, double rates, double subTotal, double tax,
                           double convenienceFee, double total)
        {
            CheckInDate = checkInDate;
            NumberOfNights = nights;
            RoomType = type;
            NumberOfRooms = rooms;
            RoomPrice = rates;
            SubTotal = subTotal;
            Tax = tax;
            ConvenienceFee = convenienceFee;
            Total = total;
        }

        public Reservation(DateTime checkInDate, int nights, string type,
                           int rooms, double rates, double subTotal, double tax,
                           double convenienceFee, double total, string firstName,
                           string lastName, string ccType, string ccNumber,
                           string phone, string email)
        {
            CheckInDate = checkInDate;
            NumberOfNights = nights;
            RoomType = type;
            NumberOfRooms = rooms;
            RoomPrice = rates;
            SubTotal = subTotal;
            Tax = tax;
            ConvenienceFee = convenienceFee;
            Total = total;
            FirstName = firstName;
            LastName = lastName;
            CreditCardType = ccType;
            CreditCardNumber = ccNumber;
            Phone = phone;
            Email = email;
        }

        public void setCustomerDetails(string firstName, string lastName, 
                                       string ccType, string ccNumber,
                                       string phone, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            CreditCardType = ccType;
            CreditCardNumber = ccNumber;
            Phone = phone;
            Email = email;
        }

        //Devuelve una cadena que muestra las propiedades de un cliente
        public override string ToString()
        {
            string strOutput = "";

            strOutput += "DETALLES DE RESERVA" + Environment.NewLine +
                         "Fecha de entrada: " + CheckInDate.ToLongDateString() + Environment.NewLine +
                         "Numero de noches: " + NumberOfNights.ToString() + Environment.NewLine +
                         "Tipo de cuarto: " + RoomType + Environment.NewLine +
                         "Numero de cuartos: " + NumberOfRooms.ToString() + Environment.NewLine +
                         "Precio de cuarto: " + RoomPrice.ToString("C2") + Environment.NewLine +
                         "Subtotal: " + SubTotal.ToString("C2") + Environment.NewLine +
                         "Iva (19%): " + Tax.ToString("C2") + Environment.NewLine +
                         "Gravamen de Hospedaje: " + ConvenienceFee.ToString("C2") + Environment.NewLine +
                         "Total: " + Total.ToString("C2") + Environment.NewLine +
                          Environment.NewLine +
                         "DETALLES DE CLIENTE" + Environment.NewLine +
                         "Nombres: " + FirstName + Environment.NewLine +
                         "Apellidos: " + LastName + Environment.NewLine +
                         "Numero de TDC: " + CreditCardNumber + Environment.NewLine +
                         "Tipo de tarjeta: " + CreditCardType + Environment.NewLine +
                         "Telefono: " + Phone + Environment.NewLine +
                         "Email: " + Email + Environment.NewLine;

            return strOutput;
        }

    }
}

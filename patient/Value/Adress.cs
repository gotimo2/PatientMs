
using System.Text.RegularExpressions;

namespace PatientMs.Value
{

    public class Adress
    {
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }

        public Adress(string street, string houseNumber, string postalCode)
        {
            if (street.Length < 1)
            {
                throw new ArgumentException("Street name must be at least one character");
            }

            Street = street;

            if (houseNumber.Length < 1)
            {
                throw new ArgumentException("HouseNumber must be at least one character");
            }

            HouseNumber = houseNumber;

            if (!Regex.IsMatch(postalCode, @"^[1-9][0-9]{3} ?(?!sa|sd|ss)[a-z]{2}$", RegexOptions.IgnoreCase)) //https://stackoverflow.com/questions/17898523/regular-expression-for-dutch-zip-postal-code
            {
                throw new ArgumentException("Postal code is invalid!");
            }

            PostalCode = postalCode;
        }
    }
}

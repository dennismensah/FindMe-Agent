using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FindMe.Data
{
    [Table("User")]
    public class User
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string CompanyTitle { get; set; }
        public string CompanyAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Coordinate1 { get; set; }
        public string Coordinate2 { get; set; }
        public string Coordinate3 { get; set; }
        public string Coordinate4 { get; set; }
        public string Coordinate5 { get; set; }
    }

    public class Coordinates
    {
        public string Latittude { get; set; }
        public string Longitude { get; set; }
    }

}

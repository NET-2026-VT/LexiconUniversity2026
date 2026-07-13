using System;
using System.Collections.Generic;
using System.Text;

namespace LexiconUniversity2026.Core.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }


        //Foreign Key
        public int StudentId { get; set; }

        //Navigational Property
        public Student Student { get; set; }
    }
}

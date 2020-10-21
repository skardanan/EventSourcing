using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcing.ApplicationService.Dtos
{
    public class PersonDto
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalCode { get; set; }
        public string BirthDate { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
    }
}

using SkepERP.Models;
using System;

namespace SkepERP.Dto
{
    public class CreatePhoneDto
    {
        public int PhoneType { get; set; }

        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return $"CreatePhoneDto: [ PhoneType: {PhoneType}, PhoneNumber: {PhoneNumber} ]";
        }
    }
}

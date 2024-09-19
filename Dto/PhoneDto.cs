using SkepERP.Models;
using System;

namespace SkepERP.Dto
{
    public class PhoneDto
    {
        public string PhoneType { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"PhoneDto: [ PhoneType: {PhoneType}, PhoneNumber: {PhoneNumber} ]";
        }
    }
}

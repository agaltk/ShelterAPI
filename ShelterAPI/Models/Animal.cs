using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShelterApi.Models
{
    public class Animal
    {

        public int Id { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public int Size { get; set; }
        public string Name { get; set; }
        public DateTime Added { get; set; }
        public string Strain { get; set; }

    }
}

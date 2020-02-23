using Newtonsoft.Json;
using ShelterApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShelterAPI.Models
{
    public class AnimalFilter : ICloneable
    {

        public int Age { get; set; }
        public int Weight { get; set; }
        public int Size { get; set; }
        public string Strain { get; set; }
        public string OrderBy { get; set; }
        private static IEnumerable<string> allowedOrderBy = new string[] { "Added", "Size" };
        public AnimalFilter()
        {
            Age = 0;
            Weight = 0;
            Size = 0;
            Strain = "";
            OrderBy = "";
        }
        public bool ParametersExist()
        {
            return Age != 0 || Weight != 0 || Size != 0 || Strain.Length != 0;
        }
        public bool OrderParameterExist()
        {
            return OrderBy.Length != 0;
        }
        public object Clone()
        {
            var jsonString = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject(jsonString, this.GetType());
        }

        public IEnumerable<Animal> FilterAnimals(IEnumerable<Animal> animals) // filter animals by age, weight, size or strain
        {
            var result = animals.Where(a => (this.Age == 0 || a.Age == this.Age) &&
                                             (this.Weight == 0 || a.Weight == this.Weight) &&
                                            (this.Size == 0 || a.Size == this.Size) &&
                                           (this.Strain.Length == 0 || a.Strain.Equals(this.Strain)));
            return result;
        }

        public IEnumerable<Animal> SortAnimals(IEnumerable<Animal> animals) // filter animals by one of allowed parameter from allowedOrderBy collection
        {
            var propertyName = OrderBy.Split(" ")[0];
            if (allowedOrderBy.Contains(propertyName))
            {

                if (OrderBy.EndsWith(" desc")) animals = animals.OrderByDescending(a => a.GetType().GetProperty(propertyName).GetValue(a, null));
                else animals = animals.OrderBy(a => a.GetType().GetProperty(propertyName).GetValue(a, null));

            }
            return animals;
        }
    }
}

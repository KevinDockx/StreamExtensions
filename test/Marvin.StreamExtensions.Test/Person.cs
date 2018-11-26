using System;
using System.Collections.Generic;
using System.Text;

namespace Marvin.StreamExtensions.Test
{
    public class Person
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var input = (Person)obj;
            return input.Name == Name;
        }

        // generate hashcode
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 13; 
                var myStrHashCode =
                    !string.IsNullOrEmpty(Name) ?
                        Name.GetHashCode() : 0; 
                return hashCode;
            }
        }
    }
}

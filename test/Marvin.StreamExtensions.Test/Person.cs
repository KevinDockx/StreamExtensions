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
            return !string.IsNullOrEmpty(Name)
                                    ? Name.GetHashCode()
                                    : 0;
        }
    }
}

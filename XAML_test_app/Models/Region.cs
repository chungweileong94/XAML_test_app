namespace XAML_test_app.Models
{
    public class Region
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Region(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}

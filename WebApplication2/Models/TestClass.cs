namespace WebApplication2.Models
{
    public class TestClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> SimpleList { get; set; }
        public List<ListTest> ClassList { get; set; }
    }

    public class ListTest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
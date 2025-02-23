namespace FindMeARoommate.Models
{
    public class Dormitories
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int MaxCapacity { get; set; }
        public List<Students>? StudentsList { get; set; }
    }
}

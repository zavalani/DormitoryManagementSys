namespace FindMeARoommate.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int DormitoriesId { get; set; }
        public Dormitories? Dormitories { get; set; }
    }
}

namespace UsandoDapper.Models
{
    public class CareerItem
    {
        public Guid CareerId { get; set; }
        public string Title { get; set; }
        public Course course { get; set; }
    }
}
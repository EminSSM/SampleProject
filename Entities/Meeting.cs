namespace Entities
{
    public class Meeting : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string DocumentPath { get; set; }
    }
}

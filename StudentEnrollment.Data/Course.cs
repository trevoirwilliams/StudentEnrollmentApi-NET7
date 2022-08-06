namespace StudentEnrollment.Data
{
    public class Course : BaseEntity
    {
        public string Title { get; set; }
        public int Credits { get; set; }
    }
}
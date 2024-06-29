namespace ReactServer.Models
{
    public class UserCourse(long accountId, long courseId, bool status = true)
    {
        public int Id { get; set; }
        public long AccountId { get; set; } = accountId;
        public long CourseId { get; set; } = courseId;
        public bool Status { get; set; } = status;
        public Course Course { get; set; }
    }


}
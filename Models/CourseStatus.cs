using System.Collections.Generic;

namespace ReactServer.Models
{
    public class CourseStatus
    {
        public CourseStatus(string title)
        {
            Title = title;
        }

        public CourseStatus()
        {
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public long? LanguageId { get; set; }

        public List<Course> Courses { get; private set; }

        public void Edit(string title)
        {
            Title = title;
        }
    }
}
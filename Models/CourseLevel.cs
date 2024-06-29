using System.Collections.Generic;

namespace ReactServer.Models
{
    public class CourseLevel
    {
        public CourseLevel(string title)
        {
            Title = title;
        }

        public CourseLevel()
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
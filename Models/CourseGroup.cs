using System;
using System.Collections.Generic;
namespace ReactServer.Models
{
    public class CourseGroup 
    {
        public int Id { get; set; }
        public string Title { get; private set; }
        public bool IsRemove { get; private set; }
       
        public string Picture { get; private set; }
        public string SinglePicture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public int? SubGroupId { get; private set; }
        public DateTimeOffset? UpdateTime { get; private set; }
        public List<Course> Courses { get; private set; }
        public CourseGroup SubGroup { get; private set; }
        public List<CourseGroup> Groups { get; set; }
    }
}
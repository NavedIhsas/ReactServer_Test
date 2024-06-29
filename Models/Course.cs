

using System;
using System.Collections.Generic;

namespace ReactServer.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ShortDescription { get; private set; }
        public string File { get; private set; }
        public double Price { get; private set; }
        public string Code { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string KeyWords { get; private set; }
        public string MetaDescription { get; private set; }
        public string DemoVideoPoster { get; private set; }
        public string Slug { get; private set; }
        public int CourseGroupId { get; private set; }
        public int? CourseLevelId { get; private set; }
        public int? CourseStatusId { get; private set; }
        public int? TeacherId { get; private set; }
        public string CanonicalAddress { get; private set; }
        public CourseGroup CourseGroup { get; private set; }
        public CourseLevel CourseLevel { get; private set; }
        public CourseStatus CourseStatus { get; private set; }
        public List<UserCourse> UserCourses { get; private set; }
        //public List<ForumQuestion> Questions { get; private set; }
    }
}
using System;

namespace ReactServer.Models
{
    public class UserCourseInfo
    {
        public int Id { get; set; }
        public string NationalCode { get; private set; }
        public string NationalPhoto { get; private set; }
        public string PersonalPhoto { get; private set; }
        public string FatherName { get; private set; }
        public bool IsConfirm { get; private set; }
        public int UserCourseId { get; private set; }
        public DateTime BirthDate { get; private set; }
        public int CityId { get; private set; }
        public string Gender { get; private set; }
        public string Mobile { get; private set; }
        public string Description { get; private set; }
        public string Area { get; private set; }
        public UserCourse UserCourse { get; private set; }

    }

}

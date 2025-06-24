﻿namespace StudentEventAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public ICollection<Registration> Registrations { get; set; }
    }
}

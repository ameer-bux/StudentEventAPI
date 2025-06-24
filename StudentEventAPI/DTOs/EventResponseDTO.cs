﻿using System;

namespace StudentEventAPI.DTOs
{
    public class EventResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Venue { get; set; }
        public DateTime Date { get; set; }
    }
}

using System;
namespace MockInterviews.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } // Optional description
        public ICollection<InterviewRequest> InterviewRequests { get; set; }
    }
}


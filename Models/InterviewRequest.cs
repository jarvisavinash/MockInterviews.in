using System;
namespace MockInterviews.Models
{
    public class InterviewRequest
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public User Candidate { get; set; } // Navigation property to User
        public int TopicId { get; set; }
        public Topic Topic { get; set; } // Navigation property to Topic
        public string Status { get; set; }
        public DateTime? ScheduledDateTime { get; set; }
        public int? InterviewerId { get; set; }
        public User Interviewer { get; set; } // Optional: Navigation to Interviewer
    }

}


using System;
namespace MockInterviews.Models
{
    public class InterviewRequest
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public User Candidate { get; set; }
        public string Topic { get; set; }
        // Waiting, Accepted, Scheduled
        public string Status { get; set; }
        public DateTime? ScheduledDateTime { get; set; }
        public int? InterviewerId { get; set; }
        public User Interviewer { get; set; }
    }
}


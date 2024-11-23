using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MockInterviews.Models
{
    public class User
    {
        public int Id { get; set; }

        // Email for login
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Hashed Password for security
        public string PasswordHash { get; set; }

        // Role (Candidate or Interviewer)
        [Required]
        public string Role { get; set; }

        // Confirm Password (not stored, only used for validation during registration)
        
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        // Password for registration (not stored, only used for validation)
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Navigation properties
        public ICollection<InterviewRequest> InterviewRequests { get; set; }
    }
}

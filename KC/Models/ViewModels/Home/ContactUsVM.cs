using System;
using System.ComponentModel.DataAnnotations;

namespace KC.Models.ViewModels.Home
{
    public class ContactUsVM
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Required(ErrorMessage = "Please enter your email address so that we can get back to you")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please ensure you leave us a message")]
        public string Message { get; set; }

        public string OrderNumber { get; set; }

        public bool ShowConfirmation { get; set; }
    }
}

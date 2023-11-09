using CandidateSystem.Shared.Proterty.CustomAttribute;
using System.ComponentModel.DataAnnotations;


namespace CandidateSystem
{
    public class Candidate
    {
        
        public int CandidateID { get; set; }

        /// <summary>
        /// Candidate full name
        /// </summary>
        [Required(ErrorMessage = "The Name Can not be empty")]
        public string CandidateName { get; set; }

        /// <summary>
        /// Candidate dob
        /// </summary>
        [Required(ErrorMessage ="Date of Birth can not be empty")]
        public DateTime CandidateDateOfBirth { get; set; }

        /// <summary>
        /// address of candidate
        /// </summary>
        public string CandidateAddress { get; set; }

        /// <summary>
        /// phone number of candidate 
        /// </summary>
        [PhoneValidate(ErrorMessage = "invalid")]
        public string CandidateNumber{ get; set; }

        /// <summary>
        /// string of path where cv has been stored
        /// </summary>
        [Required(ErrorMessage = "Import CV")]
        public string CandidateCV { get; set; }

        /// <summary>
        /// Email address of candidate
        /// </summary>
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string CandidateEmail { get; set; }

        /// <summary>
        /// where do you know us
        /// </summary>
        public string Resource { get; set;}

        /// <summary>
        /// job title id : by programing languages
        /// </summary>
        [Required(ErrorMessage = "Job Title Can not be empty")]
        public int JobTitleID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string JobTitle { get; set; } = "";

        
        /// <summary>
        /// job position Id : intern, fresher, staff ,..
        /// </summary>
        [Required(ErrorMessage = "Job Position Can not be empty")]
        public int JobPositionID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string JobPosition { get; set; } = "";

        /// <summary>
        ///  Has You apply for this job before
        ///  0: never , 1: Yes
        /// </summary>
        public int HasApplyBefore { get; set; } = 0;

        /// <summary>
        /// DelFlag
        /// 1: still on, 1: deleted 
        /// </summary>
        public int DelFlag{ get; set; } = 1;

        /// <summary>
        /// go to CandidateStatusCode in Static file
        /// </summary>
        public int CandidateStatusID { get; set; } = (int)CandidateStatusCode.ReceiveCV;

        /// <summary>
        /// 
        /// </summary>
        public string CandidateStatus { get; set; } = "";


        /// <summary>
        /// can we contact to you? 0 = not contact yet , 1 = can contact, 2 = can't contact
        /// </summary>
        public int CanContactID { get; set; } = (int)CandidateStatusCode.NotContactYet;

        public string CanContact { get; set; } = "";

        /// <summary>
        /// Deny reason or note
        /// </summary>
        public string DenyReason { get; set; } = "";

        /// <summary>
        /// Deny reason or note
        /// </summary>
        public int HasEmailFlag { get; set; } = 0;

        /// <summary>
        /// to handle checkbox in data table
        /// </summary>
        public bool isChecked { get; set; } = false;

        /// <summary>
        /// delete me
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            return (this.CandidateID + " " + this.CandidateName + " " + this.CandidateDateOfBirth + " " + this.CandidateNumber + " " + this.CandidateEmail + " " + this.Resource + "" + this.JobTitle + " " + this.JobPosition  + " " + this.CandidateCV);
        }
    }
}

using System.ComponentModel.DataAnnotations;
namespace CandidateSystem.Shared
{
    public class InterviewMeeting
    {
        /// <summary>
        /// InterviewMeetingID
        /// </summary>
        public int InterviewMeetingID { get; set; }
        /// <summary>
        /// InterviewMeetingHeader
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string InterviewMeetingHeader { get; set; }
        /// <summary>
        /// InterviewMeetingPlace
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string InterviewMeetingPlace { get; set; }
        /// <summary>
        /// InterviewMeetingDate
        /// </summary>
        [Required]
        public DateTime InterviewMeetingDate { get; set; }

        /// <summary>
        /// InterviewMeetingRoomID
        /// </summary>
        [Required]
        public int InterviewMeetingRoomID { get; set; }
        /// <summary>
        /// CandidateID
        /// </summary>
        public int CandidateID { get; set; }
        /// <summary>
        /// isHaveAssess
        /// </summary>
        public int isHaveAssess { get; set; } = ISHAVEASSESS.NOTYET;
    }
}

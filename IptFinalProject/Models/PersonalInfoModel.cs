using System.ComponentModel.DataAnnotations;

namespace IptFinalProject.Models
{
    public class PersonalInfoModel
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Section { get; set; }
        public string Course { get; set; }
        public string YearLevel { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string EmergencyContact { get; set; }
    }
}

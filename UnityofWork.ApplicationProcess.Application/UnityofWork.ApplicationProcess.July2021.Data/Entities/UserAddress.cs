using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hahn.ApplicationProcess.July2021.Data.Entities
{
    public class UserAddress
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int AddressId { get; set; }
        [ForeignKey("UserProfile")]
        public int UserId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string PostalCode { get; set; }
    }
}

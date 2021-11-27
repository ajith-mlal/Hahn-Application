
using System.Collections.Generic;


namespace UnityofWork.ApplicationProcess.July2021.Domain.Dto
{
    public class UserProfileDto
    {
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public virtual ICollection<UserAssetDto> UserAssets { get; set; }
    }
}

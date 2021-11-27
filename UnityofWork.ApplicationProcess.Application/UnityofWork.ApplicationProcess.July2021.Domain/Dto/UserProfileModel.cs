
using System.Collections.Generic;


namespace Hahn.ApplicationProcess.July2021.Domain.Dto
{
    public class UserProfileDto
    {
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public virtual ICollection<UserAssetDto> UserAssets { get; set; }
        public virtual UserAddressDto UserAddress { get; set; }
    }
}

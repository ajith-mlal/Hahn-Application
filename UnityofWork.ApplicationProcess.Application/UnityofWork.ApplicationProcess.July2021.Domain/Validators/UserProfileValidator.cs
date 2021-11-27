using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UnityofWork.ApplicationProcess.July2021.Data.Entities;
using UnityofWork.ApplicationProcess.July2021.Domain.Interfaces;
using FluentValidation.Validators;
namespace UnityofWork.ApplicationProcess.July2021.Domain.Validators
{
    public class UserProfileValidator : AbstractValidator<UserProfile>
    {
        public readonly IApiClient _apiclient;
        public UserProfileValidator( IApiClient apiclient)
        {
            _apiclient = apiclient;
            RuleFor(p => p.FirstName).NotEmpty().NotNull().MinimumLength(3);
            RuleFor(p => p.LastName).NotEmpty().NotNull().MinimumLength(3);
            RuleFor(p => p.Age).NotEmpty().GreaterThanOrEqualTo(18);
            RuleFor(p => p.Email).EmailAddress().NotEmpty().NotNull();

            RuleForEach(eachcoin =>eachcoin.UserAssets).MustAsync(async (coin, cancellation) =>
            {
                bool exists = await IsDuplicated(coin.AssetId);
                return exists;
            }).WithMessage("ID not exists");
        }


        private async Task<bool> IsDuplicated(string coinid)
        {
            var cointlist = await _apiclient.GetCoinCap();
            return cointlist.data.Any(x => x.id.Equals(coinid));
        }
    }
}

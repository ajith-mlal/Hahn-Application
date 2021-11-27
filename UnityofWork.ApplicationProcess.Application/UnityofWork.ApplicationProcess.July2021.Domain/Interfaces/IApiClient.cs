﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityofWork.ApplicationProcess.July2021.Domain.Models;

namespace UnityofWork.ApplicationProcess.July2021.Domain.Interfaces
{
    public interface IApiClient
    {
        public Task<CoinCapModel> GetCoinCap();
    }
}

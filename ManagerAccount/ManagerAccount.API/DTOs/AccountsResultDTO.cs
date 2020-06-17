using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ManagerAccount.API.DTOs
{
    public class AccountsResultDTO
    {
        ///<summary>
        /// Tổng số bản ghi
        ///</summary>
        [JsonProperty(PropertyName = "TotalRow")]
        public int TotalRow { get; set; }

        ///<summary>
        /// Danh sách Account
        ///</summary>
        [JsonProperty(PropertyName = "ListAccount")]
        public List<AccountsDTO> ListAccount { get; set; }
    }
}
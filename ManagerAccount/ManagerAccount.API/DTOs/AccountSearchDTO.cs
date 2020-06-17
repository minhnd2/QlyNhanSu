using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ManagerAccount.API.DTOs
{
    public class AccountSearchDTO
    {
        ///<summary>
        /// Tên Nhân sự
        ///</summary>
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        ///<summary>
        /// Email nhân sự
        ///</summary>
        [JsonProperty(PropertyName = "Mail")]
        public string Mail { get; set; }

        ///<summary>
        /// Số điện thoại
        ///</summary>
        [JsonProperty(PropertyName = "Phone")]
        public string Phone { get; set; }

        ///<summary>
        /// Số trang
        ///</summary>
        [JsonProperty(PropertyName = "PageIndex")]
        public int PageIndex { get; set; }

        ///<summary>
        /// Số dòng hiển thị trên 1 trang
        ///</summary>
        [JsonProperty(PropertyName = "PageSize")]
        public int PageSize { get; set; }
    }
}
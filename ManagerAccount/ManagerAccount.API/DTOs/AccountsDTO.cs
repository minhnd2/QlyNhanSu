using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ManagerAccount.API.DTOs
{
    public partial class AccountsDTO
    {
        public AccountsDTO() { }
        ///<summary>
        ///
        ///</summary>
        [JsonProperty(PropertyName = "ID")]
        public int ID { get; set; }
        
        ///<summary>
        /// số thứ tự
        ///</summary>
        [JsonProperty(PropertyName = "Index")]
        public long Index { get; set; }

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
        /// Mật khẩu
        ///</summary>
        [JsonProperty(PropertyName = "PassWord")]
        public string PassWord { get; set; }

        ///<summary>
        /// Ngày tạo
        ///</summary>
        [JsonProperty(PropertyName = "CreateDate")]
        public DateTime CreateDate { get; set; }

        ///<summary>
        /// Người tạo
        ///</summary>
        [JsonProperty(PropertyName = "Creator")]
        public string Creator { get; set; }

        ///<summary>
        /// Ngày sửa
        ///</summary>
        [JsonProperty(PropertyName = "EditDate")]
        public DateTime EditDate { get; set; }

        ///<summary>
        /// Người sửa
        ///</summary>
        [JsonProperty(PropertyName = "Editor")]
        public string Editor { get; set; }

        ///<summary>
        /// Ngày xóa
        ///</summary>
        [JsonProperty(PropertyName = "DeleteDate")]
        public DateTime DeleteDate { get; set; }

        ///<summary>
        /// Người xóa
        ///</summary>
        [JsonProperty(PropertyName = "Deletor")]
        public string Deletor { get; set; }

        ///<summary>
        /// Trạng thái
        ///</summary>
        [JsonProperty(PropertyName = "IsStatus")]
        public bool IsStatus { get; set; }

        ///<summary>
        /// Kích hoạt
        ///</summary>
        [JsonProperty(PropertyName = "IsActive")]
        public bool IsActive { get; set; }

        ///<summary>
        /// Xóa
        ///</summary>
        [JsonProperty(PropertyName = "IsDelete")]
        public bool IsDelete { get; set; }

        ///<summary>
        /// 
        ///</summary>
        [JsonProperty(PropertyName = "SecretKey")]
        public string SecretKey { get; set; }


    }
}
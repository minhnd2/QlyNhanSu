using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using Newtonsoft.Json;

namespace ManagerAccount.Infrastructure.DataModel
{    
    [Table("Accounts")]
    public partial class Accounts
    {
        public Accounts() { }

        ///<summary>
        ///
        ///</summary>
        [JsonProperty(PropertyName = "id")]
        [Required]
        [Column("ID")]
        public long Id { get; set; }
    }
}

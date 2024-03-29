﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Hahn.ApplicationProcess.July2021.Data.Entities
{
    public class UserAssets
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserAssetId { get; set; }
        [ForeignKey("UserProfile")]
        public int UserId { get; set; }
        public string AssetId { get; set; }
      
    }
}

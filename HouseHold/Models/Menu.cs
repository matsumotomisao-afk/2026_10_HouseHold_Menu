using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace HouseHold.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string TitleEn { get; set; }

        [MaxLength(200)]
        public string Url { get; set; }

        /// <summary>
        /// SEO用スラッグ（URL生成に利用）
        /// </summary>
        [MaxLength(100)]
        public string Slug { get; set; }

        public int SortOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        /// <summary>
        /// メニューに表示するかどうか（falseなら直接アクセスのみ）
        /// </summary>
        public bool IsVisibleInMenu { get; set; } = true;

        [MaxLength(50)]
        public string IconClass { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [MaxLength(250)]
        public string DescriptionEn { get; set; }

        [MaxLength(20)]
        public string Target { get; set; } = "_self";

        [MaxLength(50)]
        public string PermissionKey { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Menu Parent { get; set; }

        public ICollection<Menu> Children { get; set; } = new List<Menu>();
    }
}

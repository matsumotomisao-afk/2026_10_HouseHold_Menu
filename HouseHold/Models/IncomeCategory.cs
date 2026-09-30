using System.ComponentModel.DataAnnotations;

namespace HouseHold.Models
{
    public class IncomeCategory
    {
        public int IncomeCategoryId { get; set; } //Navigationプロパティ構築のためには、クラス名＋Id
        [Required]
        [StringLength(50)]
        [Display(Name = "所得区分")]
        public string CategoryName { get; set; } = string.Empty;  // 例：給与、事業所得、不動産所得、等

    }
}

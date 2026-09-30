using System.ComponentModel.DataAnnotations;

namespace HouseHold.Models
{
    public class PaymentType
    {
        public int PaymentTypeId { get; set; } //Navigationプロパティ構築のためには、クラス名＋Id
        [Required]
        [StringLength(50)]
        [Display(Name = "支払区分")]
        public string TypeName { get; set; } = string.Empty;  // 支払区分（例：現金、クレジットカード、電子マネー、等）

    }
}

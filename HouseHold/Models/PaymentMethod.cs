using System.ComponentModel.DataAnnotations;

namespace HouseHold.Models
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "支払いの種類")]
        public string MethodName { get; set; } = string.Empty;   // 支払方法の詳細（例：クレジットカードの種類、電子マネーの種類など）
        public int PaymentTypeId { get; set; }                // Foreign key to PaymentType
        public PaymentType? PaymentType { get; set; }         // Navigation property

    }
}

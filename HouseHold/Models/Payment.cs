using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseHold.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }   //Navigationプロパティ構築のためには、クラス名＋Id
        [Required]
        [DisplayName]
        [Display(Name = "支払日")]
        public DateTime Posted { get; set; }

        [Required]
        [Display(Name = "入出金")]
        public bool InOut { get; set; } = false;

        [Display(Name = "科目名")]              //例：食費、交通費、光熱費など
        public int? SubjectNameId { get; set; }  // SubjectName クラス（テーブル）とリレーションシップを構築
        public SubjectName? SubjectNameNavigation { get; set; } // Navigationプロパティ


        [Display(Name = "品目名(商品名）")]      // 
        [StringLength(100)]
        public string? ItemName { get; set; } = string.Empty;

        [Display(Name = "支払先名")]
        [StringLength(200)]
        public string? PaymentName { get; set; } = string.Empty;  // 例：スーパー、コンビニ、電力会社名　等

        [Display(Name = "支払方法")]
        public int? PaymentTypeId { get; set; }  // PaymentType クラス（テーブル）とリレーションシップを構築
        public PaymentType? PaymentTypeNavigation { get; set; }  // Navigationプロパティ,useful for accessing related data in the PaymentType table.
                                                                 //public string? PaymentMethodName { get; set; }  // 支払方法の詳細（例：クレジットカードの種類、電子マネーの種類など）を表示するためのプロパティ
        [Display(Name = "支払方法の詳細")]
        public int? PaymentMethodId { get; set; }  // PaymentMethod クラス（テーブル）とリレーションシップを構築
        public PaymentMethod? PaymentMethodNavigation { get; set; }  // Navigationプロパティ,useful for accessing related data in the PaymentMethod table.

        [Display(Name = "収入カテゴリ")]

        public int? IncomeCategoryId { get; set; }   // 例：給与、事業所得、不動産所得、等
        public IncomeCategory? IncomeCategoryNavigation { get; set; }  // Navigationプロパティ,useful for accessing related data in the IncomeCategory table.

        [Display(Name = "収入の種類")]

        public int? IncomeTypeId { get; set; }    // 例：賞与、家賃収入、公的年金、銀行預金の利息、等
        public IncomeType? IncomeTypeNavigation { get; set; }  // Navigationプロパティ,useful for accessing related data in the IncomeType table.

        [Required]
        [Display(Name = "金額")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]　　//金額を通貨形式で表示するための属性
        public int Amount { get; set; }

        // DBに保存しない計算列
        [NotMapped]
        public int? Balance { get; set; }  // 計算列として使用するため、DBに保存しないようにする


    }
}

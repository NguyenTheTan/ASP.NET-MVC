using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_MVC.Models
{
    public class UserMetadata
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Yêu cầu bắt buộc")]
        [MinLength(5, ErrorMessage = "Yêu cầu tên phải nhiều hơn 5 ký tự")]
        [MaxLength(50, ErrorMessage = "Yêu cầu tên phải ít hơn 50 ký tự")]
        public string name { get; set; }

        [Required(ErrorMessage = "Yêu cầu bắt buộc")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Yêu cầu phải là Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Yêu cầu bắt buộc")]
        public string password { get; set; }


        [NotMapped]
        [Required(ErrorMessage = "Yêu cầu bắt buộc")]
        [Compare("password", ErrorMessage = "Mật khẩu không trùng khớp")]
        public string repeatpassword { get; set; }
        public int role { get; set; }
        //public Nullable<int> DiscountId { get; set; }
    }
    public class CategoryMetadata
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Yêu cầu bắt buộc")]
        public string Category1 { get; set; }
    }

    public class ProductMetadata
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Yêu cầu bắt buộc")]
        [MaxLength(50, ErrorMessage = "Tên không được nhiều hơn 50 ký tự")]
        public string Names { get; set; }

        public string Images { get; set; }
        public HttpPostedFileBase ImageName { get; set; }

        [Required(ErrorMessage = "Yêu cầu bắt buộc")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime ProductDate { get; set; }
        public bool Available { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập giá tiền")]
        [RegularExpression(@"^\d+(\d{2})?$", ErrorMessage = "Yêu cầu phải là số")]
        public decimal Prices { get; set; }
        public string Descriptions { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Yêu cầu nhập danh mục")]
        public string CategoryName { get; set; }
    }

    public class OrderMetadata
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public System.DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Yêu cầu bắt buộc")]
        public string Adds { get; set; }
        public int Amount { get; set; }

        [Required(ErrorMessage = "Yêu cầu bắt buộc")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại phải 10 số và không chứa ký tự")]
        public int Phones { get; set; }

        [Required(ErrorMessage = "Yêu cầu bắt buộc")]
        [MinLength(5, ErrorMessage = "Yêu cầu tên phải nhiều hơn 5 ký tự")]
        [MaxLength(50, ErrorMessage = "Yêu cầu tên phải ít hơn 50 ký tự")]
        public string FullNames { get; set; }
    }
}
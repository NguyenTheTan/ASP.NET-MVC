//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Category
    {

        public static List<string> getListCate()
        {
            DemoEntities1 db = new DemoEntities1();
            List<string> list = new List<string>();
            List<Category> listCate = db.Categories.ToList();
            foreach (Category c in listCate)
            {
                list.Add(c.Category1);
            }
            return list;
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            this.Products = new HashSet<Product>();
        }
    
        public int Id { get; set; }
        public string Category1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
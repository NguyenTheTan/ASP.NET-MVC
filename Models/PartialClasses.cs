using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_MVC.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {

    }

    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {

    }

    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    {

    }

    [MetadataType(typeof(OrderMetadata))]
    public partial class Order
    {

    }

}
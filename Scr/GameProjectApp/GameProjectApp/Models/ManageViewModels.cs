using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameProjectApp.Models
{
    public class ManageViewModels
    {
        public string MyProperty { get; set; } = "hej";

        public string MyFunction()
        {
            return MyProperty;
        }
    }
}
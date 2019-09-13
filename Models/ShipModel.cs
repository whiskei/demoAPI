using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoAPI.Models
{
    public class ShipModel
    {
        public int ship_id { get; set; }

        public string model { get; set; }

        public string color { get; set; }

        public List<owner> owners { get; set; }
    }
}
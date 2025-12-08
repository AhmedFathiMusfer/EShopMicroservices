

using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System;
namespace Basket.Api.Models
{
    public class ShoppingCard
    {
        public string UserName { get; set; } = default!;
        public List<ShoppingCardItem> Items { get; set; } = new();

        public decimal TotalPrice => Items.Sum(x => x.Quantity * x.Price);

        public ShoppingCard(string userName)
        {
            UserName = userName;
        }
        public ShoppingCard()
        {

        }
    }
}
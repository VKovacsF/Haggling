using System;
using System.Collections.Generic;
using System.Text;

namespace Haggling.Shared
{
        public enum ProductCategory
        {
            Fruit,
            Spice,
            Pottery,
            Textile,
            Jewellery,
            Tool,
            Livestock
        }

        public enum Rarity
        {
            Common,
            Uncommon,
            Rare,
            Legendary
        }

        public class Product
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public ProductCategory Category { get; set; }
            public Rarity Rarity { get; set; }
            public decimal AskingPrice { get; set; }
            public bool Perishable { get; set; }
        }
    }
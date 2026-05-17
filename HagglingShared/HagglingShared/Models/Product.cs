using System;
using System.Collections.Generic;
using System.Text;

namespace HagglingShared.Models
{
    /// <summary>
    /// Represents product categories.
    /// </summary>
    public enum ProductCategory
    {
        Fruit,
        Vegetable,
        Jewellery,
        Antique,
        Carpet,
        Other
    }

    /// <summary>
    /// Represents product rarity.
    /// </summary>
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Legendary
    }

    /// <summary>
    /// Represents a product.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public ProductCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the rarity.
        /// </summary>
        public Rarity Rarity { get; set; }

        /// <summary>
        /// Gets or sets the asking price.
        /// </summary>
        public decimal AskingPrice { get; set; }

        /// <summary>
        /// Gets or sets whether the product is perishable.
        /// </summary>
        public bool Perishable { get; set; }
    }
}

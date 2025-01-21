using DineConnect.RestaurantManagementService.Domain.Catalogues.ValueObjects;
using DineConnect.RestaurantManagementService.Domain.Common;
using Infrastructure.Domain.Entities;

namespace DineConnect.RestaurantManagementService.Domain.Catalogues.Entities
{

    public class MenuItem: BaseEntity<MenuItemId>
    {
        #region Constants and Static Fields
        // Add constants and static fields here
        #endregion

        #region Private & Protected Fields
        #endregion

        #region Private & Protected Methods
        
        #endregion

        #region Constructors
        private MenuItem(
            MenuItemId id,
            string name,
            string description,
            bool veg,
            bool bestseller,
            string imageUrl,
            Price price,
            ItemCategory category = ItemCategory.None): base(id)
        {
            Name = name;
            Price = price;
            ImageUrl = imageUrl;
            Veg = veg;
            Bestseller = bestseller;
            Description = description;
            Category = category;
        }
        #endregion

        #region Public Properties & Methods
        public string Name { get; private set; }
        public Price Price { get; private set; }
        public string ImageUrl { get; private set; }
        public bool Veg { get; private set; }
        public bool Bestseller { get; private set; }
        public string Description { get; private set; }
        public ItemCategory Category { get; private set; }

        #endregion


        #region Factory Methods
        public static MenuItem Create(MenuItemId id, string name, string description, Price price, 
                                ItemCategory category, bool veg, bool bestseller, string imageUrl)
        {
            return new MenuItem(id, name, description, veg, bestseller, imageUrl, price, category);
        }

        public static MenuItem Create(string name, string description, Price price, 
                                                ItemCategory category, bool veg, bool bestseller, string imageUrl)
        {
            return new MenuItem(MenuItemId.Create(), name, description, veg, bestseller, imageUrl, price, category);
        }

        #endregion




    }



}

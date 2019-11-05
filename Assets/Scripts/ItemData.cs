using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemData
{
    public static Item CreateItem(int itemID)
    {
        int id = 0;
        string name = "";
        string description = "";
        ItemType type = ItemType.Ingredient;

        string icon = "";
        string mesh = "";

        int cost = 0;

        int quantity = 0;
        int damage = 0;
        int armour = 0;
        int heal = 0;

        switch(itemID)
        {
            #region Ingredient
            case 0:
                name = "Jungle Beans";
                description = "These beans are a great source of fibre and protein, and are cultivated by the tribes living in the southern jungles.";
                icon = "JungleBeans";
                mesh = "Cube";
                break;
            case 1:
                name = "Acorn";
                description = "It's an acorn.";
                icon = "Acorn";
                mesh = "Cube";
                break;
            case 2:
                name = "Magic Mushroom";
                description = "This fungus is named for its cosmetic similarity to a wizard's hat, and its mana storing properties. This fact has disappointed many junkies in search of an exotic new fix.";
                icon = "MagicMushroom";
                mesh = "Cube";
                break;
            #endregion
            #region Potion
            case 100:
                name = "Mana Elixir";
                description = "This violet concoction quickly enhances the user's mana reserves, allowing them to keep shooting magical bursts all day. There's a crass innuendo in here somewhere.";
                icon = "PurplePotion";
                mesh = "Cube";
                break;
            case 101:
                name = "Stamina Booster";
                description = "This warm, yellow drink instantly energises the user's muscles and revitalises their mind, allowing them to stay awake and fight for longer. It's also difficult to acquire due to lobbying by disgruntled coffee farmers, who can't afford a superior competitor taking away market share.";
                icon = "YellowPotion";
                mesh = "Cube";
                break;
            #endregion
            #region Scroll
            
            case 202:
                name = "Necronomicon";
                description = "This disturbing tome can be read to summon all manner of undead beings, like skeletons, vampires and minimum-wage fast-food workers.";
                icon = "MagicalTome";
                mesh = "Cube";
                break;
            #endregion
            #region Food
            case 300:
                name = "Bread";
                description = "This baked, wheat-based food is easy to make and combines well with other foods. A common local technique is to layer the rest of the food being eaten between two slices. Experts call this arrangement a 'sandwich'.";
                icon = "Bread";
                mesh = "Cube";
                break;
            case 301:
                name = "Meat";
                description = "Hearty flesh cut out of an animal. It's not entirely clear what the original animal was, but it looks delicious.";
                icon = "Meat";
                mesh = "Cube";
                break;
            #endregion
            #region Armour
            case 400:
                name = "Hoplomachus Helmet";
                description = "This sturdy, metal helmet encompasses the entire head, with holes in the front for breathing and looking at things. Its flared sections deflect strikes by bouncing them off, and fashion criticisms by making the helmet look cooler.";
                icon = "HoplomachusHelmet";
                break;
            #endregion
            #region Weapon
            case 500:
                id = itemID;
                type = ItemType.Weapon;
                name = "Broadsword";
                description = "A sturdy, reliable blade suitable for a wide variety of combat engagements. It's so ordinary and universal that I can't think of a funny joke to write here.";
                icon = "Broadsword";
                mesh = "Cube";
                break;
            #endregion
            #region Craftable
            case 600:
                name = "Wooden Planks";
                description = "Sturdy lengths of hardwood that can be cut, carved and sanded to fit into a variety of constructions.";
                icon = "Planks";
                break;
            #endregion
            #region Money

            #endregion
            #region Quest

            #endregion
            #region Miscellaneous

            #endregion
            default:
                name = "Default Item";
                description = "A placeholder item.";
                icon = "BonusDuck";

                break;
        }

        Item temp = new Item
        {
            ID = id,
            Name = name,
            Description = description,
            Type = type,
            Icon = Resources.Load("Items/Icons/" + icon) as Texture2D,
            Mesh = Resources.Load("Items/Meshes/" + mesh) as GameObject,
            Cost = cost,
            Quantity = quantity,
            Damage = damage,
            Armour = armour,
            Heal = heal
        };
        return temp;
    }
}

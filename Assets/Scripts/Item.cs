using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Ingredient,
    Potion,
    Scroll,
    Food,
    Armour,
    Weapon,
    Ammunition,
    Craftable,
    Money,
    Quest,
    Miscellaneous
}

public class Item
{
    int id;
    string name;
    string description;
    ItemType type;

    Texture2D icon;
    GameObject mesh;

    int cost;

    int quantity;
    int damage;
    int armour;
    int heal;

    public int ID
    {
        get { return id; }
        set { id = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    public ItemType Type
    {
        get { return type; }
        set { type = value; }
    }
    public Texture2D Icon
    {
        get { return icon; }
        set { icon = value; }
    }
    public GameObject Mesh
    {
        get { return mesh; }
        set { mesh = value; }
    }
    public int Cost
    {
        get { return cost; }
        set { cost = value; }
    }
    public int Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public int Armour
    {
        get { return armour; }
        set { armour = value; }
    }
    public int Heal
    {
        get { return heal; }
        set { heal = value; }
    }
}

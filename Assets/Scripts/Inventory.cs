using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Linear
{
    public class Inventory : MonoBehaviour
    {
        public GUISkin invSkin;
        public GUIStyle boxStyle;

        public List<Item> inv = new List<Item>();
        public Item selectedItem;
        // public int selectedIndex; // Reference correct item with inv[selectedIndex];
        public bool showInv;
        public Camera playerCamera;
        public Camera inventoryCamera;

        public int slotsOnScreen = 35;

        public Vector2 scr;
        public Vector2 scrollPos;

        public int money;
        public string sortType = "";

        public Transform dropLocation;
        public GameObject curWeapon;
        public GameObject curHelm;
        [System.Serializable]
        public struct Equipment
        {
            public string name;
            public Transform location;
            public GameObject curItem;
        }
        public Equipment[] equipmentSlots;
        
        // Start is called before the first frame update
        void Start()
        {
            inv.Add(ItemData.CreateItem(0));
            inv.Add(ItemData.CreateItem(1));
            inv.Add(ItemData.CreateItem(2));
            inv.Add(ItemData.CreateItem(100));
            inv.Add(ItemData.CreateItem(101));
            inv.Add(ItemData.CreateItem(202));
            inv.Add(ItemData.CreateItem(300));
            inv.Add(ItemData.CreateItem(301));
            inv.Add(ItemData.CreateItem(400));
            inv.Add(ItemData.CreateItem(500));
            inv.Add(ItemData.CreateItem(600));
            inv.Add(ItemData.CreateItem(700));
            inv.Add(ItemData.CreateItem(800));
            inv.Add(ItemData.CreateItem(0));
            inv.Add(ItemData.CreateItem(1));
            inv.Add(ItemData.CreateItem(2));
            inv.Add(ItemData.CreateItem(100));
            inv.Add(ItemData.CreateItem(101));
            inv.Add(ItemData.CreateItem(202));
            inv.Add(ItemData.CreateItem(300));
            inv.Add(ItemData.CreateItem(301));
            inv.Add(ItemData.CreateItem(400));
            inv.Add(ItemData.CreateItem(500));
            inv.Add(ItemData.CreateItem(600));
            inv.Add(ItemData.CreateItem(700));
            inv.Add(ItemData.CreateItem(800));
            inv.Add(ItemData.CreateItem(0));
            inv.Add(ItemData.CreateItem(1));
            inv.Add(ItemData.CreateItem(2));
            inv.Add(ItemData.CreateItem(100));
            inv.Add(ItemData.CreateItem(101));
            inv.Add(ItemData.CreateItem(202));
            inv.Add(ItemData.CreateItem(300));
            inv.Add(ItemData.CreateItem(301));
            inv.Add(ItemData.CreateItem(400));
            inv.Add(ItemData.CreateItem(500));
            inv.Add(ItemData.CreateItem(600));
            inv.Add(ItemData.CreateItem(700));
            inv.Add(ItemData.CreateItem(800));
            inv.Add(ItemData.CreateItem(0));
            inv.Add(ItemData.CreateItem(1));
            inv.Add(ItemData.CreateItem(2));
            inv.Add(ItemData.CreateItem(100));
            inv.Add(ItemData.CreateItem(101));
            inv.Add(ItemData.CreateItem(202));
            inv.Add(ItemData.CreateItem(300));
            inv.Add(ItemData.CreateItem(301));
            inv.Add(ItemData.CreateItem(400));
            inv.Add(ItemData.CreateItem(500));
            inv.Add(ItemData.CreateItem(600));
            inv.Add(ItemData.CreateItem(700));
            inv.Add(ItemData.CreateItem(800));
            inv.Add(ItemData.CreateItem(0));
            inv.Add(ItemData.CreateItem(1));
            inv.Add(ItemData.CreateItem(2));
            inv.Add(ItemData.CreateItem(100));
            inv.Add(ItemData.CreateItem(101));
            inv.Add(ItemData.CreateItem(202));
            inv.Add(ItemData.CreateItem(300));
            inv.Add(ItemData.CreateItem(301));
            inv.Add(ItemData.CreateItem(400));
            inv.Add(ItemData.CreateItem(500));
            inv.Add(ItemData.CreateItem(600));
            inv.Add(ItemData.CreateItem(700));
            inv.Add(ItemData.CreateItem(800));
            inv.Add(ItemData.CreateItem(0));
            inv.Add(ItemData.CreateItem(1));
            inv.Add(ItemData.CreateItem(2));
            inv.Add(ItemData.CreateItem(100));
            inv.Add(ItemData.CreateItem(101));
            inv.Add(ItemData.CreateItem(202));
            inv.Add(ItemData.CreateItem(300));
            inv.Add(ItemData.CreateItem(301));
            inv.Add(ItemData.CreateItem(400));
            inv.Add(ItemData.CreateItem(500));
            inv.Add(ItemData.CreateItem(600));
            inv.Add(ItemData.CreateItem(700));
            inv.Add(ItemData.CreateItem(800));
            inv.Add(ItemData.CreateItem(0));
            inv.Add(ItemData.CreateItem(1));
            inv.Add(ItemData.CreateItem(2));
            inv.Add(ItemData.CreateItem(100));
            inv.Add(ItemData.CreateItem(101));
            inv.Add(ItemData.CreateItem(202));
            inv.Add(ItemData.CreateItem(300));
            inv.Add(ItemData.CreateItem(301));
            inv.Add(ItemData.CreateItem(400));
            inv.Add(ItemData.CreateItem(500));
            inv.Add(ItemData.CreateItem(600));
            inv.Add(ItemData.CreateItem(700));
            inv.Add(ItemData.CreateItem(800));
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                showInv = !showInv;
                if (showInv)
                {
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }

        private void OnGUI()
        {
            if (showInv)
            {
                inventoryCamera.enabled = true;
                playerCamera.enabled = false;
                scr.x = Screen.width / 16;
                scr.y = Screen.height / 9;

                GUI.Box(new Rect(0, 0, scr.x * 5, Screen.height), "");
                GUI.Box(new Rect(scr.x * 11, 0, scr.x * 5, Screen.height), "");
                Display();
                if (selectedItem != null)
                {
                    //GUI.skin = invSkin;
                    GUI.Box(new Rect(scr.x * 12f, scr.y * 0.25f, scr.x * 3, scr.y * 0.25f), selectedItem.Name);
                    GUI.Box(new Rect(scr.x * 12f, scr.y * 0.75f, scr.x * 3, scr.y * 3), selectedItem.Icon);
                    GUI.Box(new Rect(scr.x * 12f, scr.y * 4, scr.x * 3, scr.y * 2), selectedItem.Description);
                    //GUI.skin = null;
                }
            }
            else
            {
                inventoryCamera.enabled = false;
                playerCamera.enabled = true;
            }
        }

        void Display()
        {
            if (inv.Count <= slotsOnScreen)
            {
                for(int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + (0.25f * scr.y * i), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                    {
                        selectedItem = inv[i];
                    }
                }
            }
            else
            {
                scrollPos = GUI.BeginScrollView(new Rect(0, 0.25f * scr.y, 3.75f * scr.x, 8.5f * scr.y), scrollPos, new Rect(0, 0, 0, 8.5f * scr.y + ((inv.Count - 34) * (0.25f * scr.y))), false, true);
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + (0.25f * scr.y * i), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                    {
                        selectedItem = inv[i];
                    }
                }
                GUI.EndScrollView();
            }


            /*
            if (GUI.Button(new Rect(15 * scr.x, scr.y, scr.x, 0.5f * scr.y), "Create Item"))
            {
                //ItemData.CreateItem();
            }
            */
        }



        void UseItem(ItemType item)
        {
            switch (item)
            {
                case ItemType.Ingredient:

                    break;
                case ItemType.Potion:

                    break;
                case ItemType.Scroll:

                    break;
                case ItemType.Food:

                    break;
                case ItemType.Armour:

                    break;
                case ItemType.Weapon:

                    break;
                case ItemType.Ammunition:

                    break;
                case ItemType.Craftable:

                    break;
                case ItemType.Money:

                    break;
                case ItemType.Quest:

                    break;
                case ItemType.Miscellaneous:

                    break;
                default:

                    break;
            }

            if (GUI.Button(new Rect(0, 0, 1, 1), "Discard"))
            {

            }
        }
    }

}

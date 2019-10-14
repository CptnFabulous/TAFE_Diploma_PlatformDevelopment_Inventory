using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Linear
{
    public class Inventory : MonoBehaviour
    {
        public List<Item> inv = new List<Item>();
        public Item selectedItem;
        // public int selectedIndex; // Reference correct item with inv[selectedIndex];
        public bool showInv;

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
            inv.Add(ItemData.CreateItem(20));
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
                scr.x = Screen.width / 16;
                scr.y = Screen.height / 9;

                GUI.Box(new Rect(0, 0, scr.x * 12, Screen.height), "");
            }
        }
    }

}

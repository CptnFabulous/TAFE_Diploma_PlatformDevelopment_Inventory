using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropInventory : MonoBehaviour
{
    #region Variables
    [Header("Inventory")]
    public bool showInv;
    public List<Item> inv = new List<Item>();
    public int slotX, slotY;
    public Rect inventorySize;

    [Header("Dragging")]
    public bool isDragging;
    public int draggedFrom;
    public Item draggedItem;
    public GameObject droppedItem;

    [Header("Tooltip")]
    public int toolTipItem;
    public bool showTooltip;
    public Rect toolTipRect;

    [Header("References and Locations")]
    public Vector2 screenRatio;

    Vector2 Scr()
    {
        return new Vector2(Screen.width / screenRatio.x, Screen.height / screenRatio.y);
    }

    Rect GUIRect(float x, float y, float width, float height)
    {
        return new Rect(Scr().x * x, Scr().y * y, Scr().x * width, Scr().y * height);
    }
    #endregion

    #region Clamp to screen

    private Rect ClampToScreen(Rect r)
    {
        r.x = Mathf.Clamp(r.x, 0, Screen.width * r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height * r.height);
        return r;
    }

    #endregion

    #region Add item
    public void AddItem(int itemID)
    {
        for(int i = 0; i < inv.Count; i++)
        {
            if (inv[i].Name == null)
            {
                inv[i] = ItemData.CreateItem(itemID);
                Debug.Log("Item " + inv[i].Name + " added to inventory");
                return;
            }
        }
    }
    #endregion

    #region Drop item
    public void DropItem()
    {
        droppedItem = draggedItem.Mesh;
        droppedItem = Instantiate(droppedItem, transform.position + transform.forward * 3, Quaternion.identity);
        droppedItem.AddComponent<Rigidbody>().useGravity = true;
        droppedItem.name = draggedItem.Name;
        droppedItem = null;
    }
    #endregion

    #region Draw item
    void DrawItem(int windowID)
    {
        if (draggedItem.Icon != null)
        {
            GUI.DrawTexture(GUIRect(0, 0, 0.5f, 0.5f), draggedItem.Icon);
        }
    }
    #endregion

    #region Tooltip
    #region TooltipContent
    private string ToolTipText(int index)
    {
        return inv[index].Name + "\n" + inv[index].Description + "\nValue : $" + inv[index].Cost;
    }
    #endregion

    #region TooltipWindow
    void DrawToolTip(int windowID)
    {
        GUI.Box(GUIRect(0, 0, 6, 2), ToolTipText(toolTipItem));
    }
    #endregion
    #endregion

    #region Toggle inventory
    public void ToggleInv()
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
    #endregion
    #region Drag inventory
    void InventoryDrag(int windowID)
    {
        GUI.Box(GUIRect(0, 0.25f, 5.5f, 0.375f), "Banner");
        GUI.Box(GUIRect(0, 4.25f, 5.5f, 0.375f), "Currency Display");
        showTooltip = false;

        #region Nested For Loop
        int i = 0;
        Event e = Event.current;
        for (int y = 0; y < slotY; y++)
        {
            for (int x = 0; x < slotX; x++)
            {
                Rect slotLocation = new Rect(
                    Scr().x * 0.125f + x * (Scr().x * 0.75f),
                    Scr().y * 0.75f + y * (Scr().y * 0.65f),
                    Scr().x * 0.75f,
                    Scr().y * 0.65f
                    );
                GUI.Box(slotLocation, "");

                #region Pickup Item
                if (e.button == 0 && e.type == EventType.MouseDown && slotLocation.Contains(e.mousePosition) && !isDragging && inv[i].Name != null && !Input.GetKey(KeyCode.LeftShift))
                {
                    draggedItem = inv[i];
                    inv[i] = new Item();
                    isDragging = true;
                    draggedFrom = i;
                    Debug.Log("Player is currently dragging " + draggedItem.Name);
                }
                #endregion

                #region Place/swap item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && isDragging)
                {
                    if (inv[i].Name != null)
                    {
                        Debug.Log("Swapped your " + draggedItem.Name + " with " + inv[i].Name);
                        inv[draggedFrom] = inv[i];
                        inv[i] = draggedItem;
                        draggedItem = new Item();
                        isDragging = false;
                    }
                    else
                    {
                        Debug.Log("Placing your " + draggedItem.Name);
                        inv[i] = draggedItem;
                        draggedItem = new Item();
                        isDragging = false;
                    }
                }

                /*
                #region Swap Item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && isDragging && inv[i].Name != null)
                {
                    inv[draggedFrom] = inv[i];
                    inv[i] = draggedItem;
                    draggedItem = new Item();
                    isDragging = false;
                }
                #endregion

                #region Place Item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && isDragging && inv[i].Name == null)
                {
                    inv[i] = draggedItem;
                    draggedItem = new Item();
                    isDragging = false;
                }
                #endregion
                */
                #endregion

                #region Return Item

                #endregion

                #region Draw Item Icon
                if (inv[i].Name != null)
                {
                    GUI.DrawTexture(slotLocation, inv[i].Icon);
                    #region Set tooltip on mouse hover
                    if (slotLocation.Contains(e.mousePosition) && !isDragging && showInv)
                    {
                        toolTipItem = i;
                        showTooltip = true;
                    }
                    #endregion
                }
                #endregion
                i++;
            }
        }
        #endregion

        #region Drag Points
        GUI.DragWindow(GUIRect(0, 0, 6, 0.25f)); // Top
        GUI.DragWindow(GUIRect(0, 0.25f, 0.25f, 3.75f)); // Left
        GUI.DragWindow(GUIRect(5.5f, 0.25f, 0.25f, 3.75f)); // Right
        GUI.DragWindow(GUIRect(0, 4, 6, 0.25f)); // Bottom
        #endregion

    }
    #endregion

    #region Start
    // Start is called before the first frame update
    void Start()
    {
        inventorySize = GUIRect(1, 1, 6, 4.5f);
        for (int i = 0; i < (slotX * slotY); i++)
        {
            inv.Add(new Item());
        }

        AddItem(0);
        AddItem(1);
        AddItem(2);
        AddItem(400);
        AddItem(500);
        AddItem(501);
    }
    #endregion

    #region Update
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInv();
        }
        /*
        if (scr.x != Screen.width / 16)
        {
            scr.x = Screen.width / 16;
            scr.y = Screen.height / 9;
            inventorySize = new Rect(scr.x, scr.y, scr.x * 6, scr.y * 4.5f);
        }
        */
    }
    #endregion

    #region OnGUI
    private void OnGUI()
    {
        Event e = Event.current;

        #region Inventory when true
        if (showInv)
        {
            inventorySize = ClampToScreen(GUI.Window(1, inventorySize, InventoryDrag, "Player Inventory"));
            #region Tooltip display
            if (showTooltip)
            {
                toolTipRect = new Rect(e.mousePosition.x + 0.01f, e.mousePosition.y + 0.01f, Scr().x * 6, Scr().y * 2);
                GUI.Window(7, toolTipRect, DrawToolTip, "");
            }
            #endregion
        }
        #endregion

        #region Drop item
        if ((e.button == 0 && e.type == EventType.MouseUp && isDragging) || (isDragging && !showInv))
        {
            DropItem();
            Debug.Log("Dropped item");
            draggedItem = new Item();
            isDragging = false;
        }
        #endregion

        #region Draw item on mouse
        if (isDragging)
        {
            if (draggedItem != null)
            {
                Rect mouseLocation = new Rect(e.mousePosition.x + 0.125f, e.mousePosition.y + 0.125f, Scr().x * 0.5f, Scr().y * 0.5f);
                GUI.Window(72, mouseLocation, DrawItem, "");
            }
        }
        #endregion

    }
    #endregion

}

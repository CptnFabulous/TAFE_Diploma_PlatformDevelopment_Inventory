using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSelect : MonoBehaviour
{
    #region Variables
    public List<Item> inv = new List<Item>(8);


    [Header("Main UI")]
    public bool showSelectMenu; // Can be substituted for checking if the canvas is enabled, in a canvas version
    public bool toggleToggleable;
    //public Vector2 scr;
    public Vector2 screenRatio;

    Vector2 Scr()
    {
        return new Vector2(Screen.width / screenRatio.x, Screen.height / screenRatio.y);
    }

    Rect GUIRect(float x, float y, float width, float height)
    {
        return new Rect(Scr().x * x, Scr().y * y, Scr().x * width, Scr().y * height);
    }

    [Header("Resources")]
    public Texture2D radialTexture;
    public Texture2D slotTexture;
    [Range(0, 100)]
    public int circleScaleOffset;

    [Header("Icons")]
    public Vector2 iconSize;
    public bool showIcons, showBoxes, showBounds;
    [Range(0.1f, 1)]
    public float iconSizeNum;
    [Range(-360, 360)]
    public int radialRotation;
    [SerializeField]
    float iconOffset;

    [Header("Mouse Settings")]
    public Vector2 mouse;
    public Vector2 input;
    Vector2 circleCenter;

    [Header("Input Settings")]
    public float inputDist;
    public float inputAngle;
    public int keyIndex;
    public int mouseIndex;
    public int inputIndex;

    [Header("Sector Settings")]
    public Vector2[] slotPos;
    public Vector2[] boundPos;
    [Range(1, 32)]
    public int numOfSectors = 1;
    [Range(50, 300)]
    public float circleRadius;
    public float mouseDistance, sectorDegree, mouseAngles;
    public int sectorIndex = 0;
    public bool withinCircle;

    [Header("Misc")]
    private Rect debugWindow;
    #endregion

    #region Setup functions
    private Vector2 ScrVar(float x, float y)
    {
        return new Vector2(screenRatio.x * x, screenRatio.y * y);
    }

    private Vector2[] BoundPositions(int slots)
    {
        Vector2[] boundPos = new Vector2[slots];
        float angle = 0 + radialRotation;
        for (int i = 0; i < boundPos.Length; i++)
        {
            boundPos[i].x = circleCenter.x + circleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
            boundPos[i].y = circleCenter.y + circleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
            angle += sectorDegree;
        }

        return boundPos;
    }

    private Vector2[] SlotPositions(int slots)
    {
        Vector2[] slotPos = new Vector2[slots];
        float angle = ((iconOffset / 2) * 2) + radialRotation;
        for (int i = 0; i < slotPos.Length; i++)
        {
            slotPos[i].x = circleCenter.x + circleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
            slotPos[i].y = circleCenter.y + circleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
            angle += sectorDegree;
        }

        return slotPos;
    }

    void SetItemSlots(int slots, Vector2[] pos)
    {
        for (int i = 0; i < slots; i++)
        {
            Texture2D graphic = slotTexture;
            if (i < inv.Count)
            {
                graphic = inv[i].Icon;
            }
            //print(graphic);

            GUI.DrawTexture(new Rect(pos[i].x * (screenRatio.x * iconSizeNum * 0.5f), pos[i].y - (screenRatio.y * iconSizeNum * 0.5f), screenRatio.x * iconSizeNum, screenRatio.y * iconSizeNum), graphic);
        }
    }

    private int CheckCurrentSector(float angle)
    {
        float boundingAngle = 0;
        for (int i = 0; i < numOfSectors; i++)
        {
            boundingAngle += sectorDegree;
            if (angle < boundingAngle)
            {
                return i;
            }
        }
        return 0;
    }

    void CalculateMouseAngles()
    {
        mouse = Input.mousePosition;
        input.x = Input.GetAxis("Horizontal");
        input.y = -Input.GetAxis("Vertical");

        mouseDistance = Mathf.Sqrt(Mathf.Pow((mouse.x - circleCenter.x), 2) + Mathf.Pow((mouse.y - circleCenter.y), 2));
        inputDist = Vector2.Distance(Vector2.zero, input);

        withinCircle = mouseDistance <= circleRadius ? true : false; // Sets value of withinCircle based on if mouseDistance is within circleRadius also AGGH MY EYES THEY'RE BURNING

        if (input.x != 0 || input.y != 0) // Substitute if (input.magnitude != 0) OR if (input != Vector2.zero). ALSO, this if statement is declared twice and may be redundant.
        {
            inputAngle = (Mathf.Atan2(-input.y, input.x) * 180 / Mathf.PI) + radialRotation;
        }
        else
        {
            mouseAngles = (Mathf.Atan2(mouse.y - circleCenter.y, mouse.x - circleCenter.x) * 180 / Mathf.PI) + radialRotation;
        }

        if (mouseAngles < 0)
        {
            mouseAngles += 360;
        }
        if (inputAngle < 0)
        {
            inputAngle += 360;
        }

        mouseIndex = CheckCurrentSector(mouseAngles);
        inputIndex = CheckCurrentSector(inputAngle);

        if (input.x != 0 || input.y != 0) // Substitute if (input.magnitude != 0) OR if (input != Vector2.zero)
        {
            sectorIndex = inputIndex;
        }
        if (input.x == 0 && input.y == 0) // Substitute if (input == Vector2.zero)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                sectorIndex = mouseIndex;
            }
        }
    }

    public void AddItem(int itemID)
    {
        for (int i = 0; i < inv.Count; i++)
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

    #region Unity functions
    // Start is called before the first frame update
    void Start()
    {
        
        AddItem(0);
        AddItem(1);
        AddItem(2);
        AddItem(400);
        AddItem(500);
        AddItem(501);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            circleCenter.x = Screen.width / 2;
            circleCenter.y = Screen.height / 2;
            showSelectMenu = true;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            showSelectMenu = false;
        }
    }

    private void OnGUI()
    {
        if (showSelectMenu == true)
        {
            CalculateMouseAngles();
            sectorDegree = 360 / numOfSectors;
            iconOffset = sectorDegree / 2;
            slotPos = SlotPositions(numOfSectors);
            boundPos = BoundPositions(numOfSectors);
            // Deadzone
            GUI.Box(GUIRect(7.5f, 4, 1, 1), "");
            // Circle
            GUI.DrawTexture(new Rect(circleCenter.x - circleRadius - (circleScaleOffset / 4), circleCenter.y - circleRadius - (circleScaleOffset / 4), (circleRadius * 2) + (circleScaleOffset / 2), (circleRadius * 2) + (circleScaleOffset / 2)), radialTexture);

            if (showBoxes == true)
            {
                for (int i = 0; i < numOfSectors; i++)
                {
                    GUI.DrawTexture(new Rect(slotPos[i].x - (Scr().x * iconSizeNum * 0.5f), slotPos[i].y - (Scr().y * iconSizeNum * 0.5f), Scr().x * iconSizeNum, Scr().y * iconSizeNum), slotTexture);
                }
            }

            if (showBounds == true)
            {
                for (int i = 0; i < numOfSectors; i++)
                {
                    GUI.Box(new Rect(boundPos[i].x - (Scr().x * 0.1f * 0.5f), boundPos[i].y - (Scr().y * 0.1f * 0.5f), Scr().x * 0.1f, Scr().y * 0.1f), "");
                }
            }

            if (showIcons == true)
            {
                SetItemSlots(numOfSectors, slotPos);
            }
        }
    }
    #endregion
}

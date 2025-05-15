using UnityEngine;
using Vector2 = UnityEngine.Vector2; // Resolve ambiguity between UnityEngine and System.Numerics

public class cursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D gunCursor;
    [SerializeField] private Texture2D swordCursor;
    [SerializeField] private Texture2D axeCursor;
    [SerializeField] private Texture2D ConsumeablesCursor;
    [SerializeField] private Texture2D otherCursor;

    private Vector2 cursorHotspot;
    private string lastCursorItemName = "";

    public InventorySystem inventorySystem;

    void Start()
    {
        SetCursor(defaultCursor);
    }

    void Update()
    {
        if(inventorySystem == null)
        {
            return;
        }

        var selectedItem = inventorySystem.GetSelectedItem();

        string currentName = selectedItem != null ? selectedItem.name : "";

        if(currentName != lastCursorItemName)
        {
            if(currentName == "Pistol" || currentName == "Shutgun")
            {
                SetCursorByType(CursorType.Gun);
                Debug.Log("rarararara");
            }
            else if(currentName == "MachineGun")
            {
                SetCursorByType(CursorType.Gun);
                Debug.Log("MachineGun");
            }
            else if(currentName == "Pickaxe" || currentName == "Hammer")
            {
                SetCursorByType(CursorType.Axe);
            }
            else if(currentName == "Knife" || currentName == "BoneSword" || currentName == "WoodSword")
            {
                SetCursorByType(CursorType.Sword);
            }
            else if(currentName == "Saw" || currentName == "Axe")
            {
                SetCursorByType(CursorType.Axe);
            }
            else if(currentName == "Carrot")
            {
                SetCursorByType(CursorType.Consumeables);
            }
            else
            {
                SetCursorByType(CursorType.Other);
            }

            lastCursorItemName = currentName;
        }
    }

    public enum CursorType
    {
        Default,
        Gun,
        Sword,
        Axe,
        Consumeables,
        Other
    }

    public void SetCursorByType(CursorType type)
    {
        switch (type)
        {
            case CursorType.Gun:
                SetCursor(gunCursor);
                break;
            case CursorType.Sword:
                SetCursor(swordCursor);
                break;
            case CursorType.Axe:
                SetCursor(axeCursor);
                break;
            case CursorType.Other:
                SetCursor(otherCursor);
                break;
            case CursorType.Consumeables:
                SetCursor(ConsumeablesCursor);
                break;
            default:
                SetCursor(defaultCursor);
                break;
        }
    }

    private void SetCursor(Texture2D texture)
    {
        if (texture == null)  
        {
            return;
        }

        cursorHotspot = new Vector2(texture.width / 2, texture.height / 2);
        Cursor.SetCursor(texture, cursorHotspot, CursorMode.Auto);
    }
}


# if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class EquipmentEditorWindow : EditorWindow
{
    private int selectedHead = -1;
    private int selectedBody = -1;
    private int selectedWeapon = 0;

    private string[] headItems = new string[20];
    private string[] bodyItems = new string[20];
    private string[] weaponItems = new string[] { "None", "Rifle", "Bazooka", "Handgun" };


    static private Player player;

    [MenuItem("Tools/Equipment Editor")]
    public static void ShowWindow()
    {
        GetWindow<EquipmentEditorWindow>("Equipment Editor");

        var playerObj = GameObject.Find("Player");
        if(playerObj == null)
        {
            Debug.LogError("EquipmentEditorWindow : Not Found Player");
            return;
        }
        
        player = playerObj.GetComponent<Player>();   


    }

    private void OnEnable()
    {
        for (int i = 0; i < 20; i++)
        {
            headItems[i] = $"Head {i + 1}";
            bodyItems[i] = $"Body {i + 1}";
        }
    }

    private void OnGUI()
    {
        DrawItemGrid("Head Items", headItems, ref selectedHead, eEuipmentType.Head);
        GUILayout.Space(10);
        DrawItemGrid("Body Items", bodyItems, ref selectedBody, eEuipmentType.Body);

        GUILayout.Space(10);
        GUILayout.Label("Weapon Items", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        for (int i = 0; i < weaponItems.Length; i++)
        {
            bool isSelected = selectedWeapon == i;
            bool pressed = GUILayout.Toggle(isSelected, weaponItems[i], "Button");

            if (pressed && !isSelected)
            {
                selectedWeapon = i;
                OnItemSelected(eEuipmentType.Weapon, i, (eWeaponType)i);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(20);
        GUILayout.Label($"Selected:\nHead: {GetSelected(headItems, selectedHead)}\nBody: {GetSelected(bodyItems, selectedBody)}\nWeapon: {GetSelected(weaponItems, selectedWeapon)}");
    }

    private string GetSelected(string[] items, int index)
    {
        return index >= 0 && index < items.Length ? items[index] : "None";
    }

    private void DrawItemGrid(string label, string[] items, ref int selectedIndex, eEuipmentType type)
    {
        GUILayout.Label(label, EditorStyles.boldLabel);
        int columns = 4;
        int rows = Mathf.CeilToInt(items.Length / (float)columns);

        for (int row = 0; row < rows; row++)
        {
            GUILayout.BeginHorizontal();
            for (int col = 0; col < columns; col++)
            {
                int index = row * columns + col;
                if (index < items.Length)
                {
                    bool isSelected = selectedIndex == index;
                    bool pressed = GUILayout.Toggle(isSelected, items[index], "Button");

                    if (pressed && !isSelected)
                    {
                        selectedIndex = index;
                        OnItemSelected(type, index, (eWeaponType)index);
                    }
                }
            }
            GUILayout.EndHorizontal();
        }
    }

    private void OnItemSelected(eEuipmentType type, int index, eWeaponType itemName)
    {
        if(player == null)
        {
            var playerObj = GameObject.Find("Player");
            if (playerObj == null)
            {
                Debug.LogError("EquipmentEditorWindow : Not Found Player");
                return;
            }

            player = playerObj.GetComponent<Player>();
        }

        Debug.Log($"[ภๅย๘ตส] {type}: {itemName} (Index {index})");



        if(type == eEuipmentType.Weapon)
        {
            player.GetAnimator().runtimeAnimatorController = ResourceManager.Instance.dic[itemName];
            player.GetModelController().SetWeaponByIndex(itemName);
        }
        else
        {
            player.GetModelController().SetActiveExclusive(type, index);
        }
        
        
    }

}
#endif
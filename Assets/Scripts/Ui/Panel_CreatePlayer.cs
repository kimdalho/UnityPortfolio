using UnityEngine;
using TMPro;
using UnityEngine.UI;
/// <summary>
/// ���ο� ĳ���͸� �����Ѵ�.
/// ĳ������ �⺻ ��Ʈ����Ʈ�� ������ �¾��ǰ� ���̺��Ѵ�.
/// </summary>
public class Panel_CreatePlayer : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField tmp_InputField;
    [SerializeField]
    public Button btn_Ok;
    CharacterSlot slot;

    public void SetData(CharacterSlot slot)
    {
        this.slot = slot;
        btn_Ok.onClick.AddListener(OnClickOkButton);
    }


    public void OnClickOkButton()
    {
        slot.SetNickName(tmp_InputField.text);
        UserData.Instance.CreateNewCharacter(tmp_InputField.text,slot.index);
        gameObject.SetActive(false);        
    }

    public string GetNickName()
    {
        return tmp_InputField.text;
    }
    
}

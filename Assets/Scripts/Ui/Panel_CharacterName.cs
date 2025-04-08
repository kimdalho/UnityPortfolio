using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
/// <summary>
/// CreateCharacterSlot ��ư�� ������ Ȱ��ȭ
/// </summary>
public class Panel_CharacterName : MonoBehaviour
{

    [SerializeField]
    private TMP_InputField tmp_InputField;
    [SerializeField]
    public Button btn_Ok;
    CreateCharacterSlot slot;

    public void SetData(CreateCharacterSlot slot)
    {
        this.slot = slot;
        btn_Ok.onClick.AddListener(OnClickOkButton);
    }


    public void OnClickOkButton()
    {
        slot.SetNickName(tmp_InputField.text);
        gameObject.SetActive(false);
    }

    
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Panel_CharacterSelect���� ��ư �ʱ�ȭ
/// </summary>

public class CreateCharacterSlot : MonoBehaviour
{
    public TextMeshProUGUI tmp_Nickname;
    public Button BtnCreate;

    public void Start()
    {
        
    }

    public void SetNickName(string text)
    {
        tmp_Nickname.text = text;
    }



}

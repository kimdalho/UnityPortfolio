using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Panel_CharacterSelect���� ��ư �ʱ�ȭ
/// </summary>

public class CharacterSlot : MonoBehaviour
{
    //�ش� ���Կ� �÷��� �����ϴ��� ���� üũ
    public bool existCharacter;
    public int index;
    public TextMeshProUGUI tmp_Nickname;
    public Button BtnSelectCharacter;

    public void SetNickName(string text)
    {
        tmp_Nickname.text = text;
    }



}

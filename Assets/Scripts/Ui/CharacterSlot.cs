using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Panel_CharacterSelect에서 버튼 초기화
/// </summary>

public class CharacterSlot : MonoBehaviour
{
    //해당 슬롯에 플레이 존재하는지 여부 체크
    public bool existCharacter;
    public int index;
    public TextMeshProUGUI tmp_Nickname;
    public Button BtnSelectCharacter;

    public void SetNickName(string text)
    {
        tmp_Nickname.text = text;
    }



}

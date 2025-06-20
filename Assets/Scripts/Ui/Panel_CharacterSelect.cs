using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_CharacterSelect : MonoBehaviour
{
    [SerializeField]
    Panel_CreatePlayer panel_CharacterName;
    public ModelController model;

    public LocalizedText[] tmpStats;

    public Button btn_Back;
    public Button btn_GameAccess;
    public Button btn_DelectCharacter;

    public Image img_Status;
    
    public List<CharacterSlot> slotList;    

   
    private void Awake()
    {
        SetData();
    }

    public void SetData()
    {
        foreach (CharacterSlot slot in slotList)
        {
            slot.BtnSelectCharacter.onClick.AddListener(() => 
            {
                //해당 슬롯에 캐릭터가 이미 존재하는경우
                if(slot.existCharacter)
                {
                    ChangeSelectSlot(slot);
                }
                else
                {
                    panel_CharacterName.gameObject.SetActive(true);
                    panel_CharacterName.SetData(slot);
                    panel_CharacterName.btn_Ok.onClick.AddListener(() =>
                    {
                        slot.existCharacter = true;
                        //새로운 플레이어를 생성 또는 기존 플레이어 캐릭터 선택완료
                        img_Status.gameObject.SetActive(true);
                        model.gameObject.SetActive(true);
                        ChangeSelectSlot(slot);
                    });

                }
            });
        }

        img_Status.gameObject.SetActive(false);

        btn_Back.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            LobbySceneController.Instance.panel_Title.gameObject.SetActive(true);
            img_Status.gameObject.SetActive(false);
        });

        btn_GameAccess.onClick.AddListener(GameStart);
    }
    private void ChangeSelectSlot(CharacterSlot _slot)
    {
        var data = UserData.Instance.slots[_slot.index];

        var MaxHart = data.playerdata.GetMaxValue(eAttributeType.Health);
        var CurHealth = data.playerdata.GetCurValue(eAttributeType.Health);
        var Attack = data.playerdata.GetCurValue(eAttributeType.Attack);
        var AttackSpeed = data.playerdata.GetCurValue(eAttributeType.AttackSpeed);
        var speed = data.playerdata.GetCurValue(eAttributeType.Speed);

        tmpStats[0].UpdateLocalizedText(data.dungeonLevel);
        tmpStats[1].UpdateLocalizedText(MaxHart, CurHealth);
        tmpStats[2].UpdateLocalizedText(Attack);
        tmpStats[3].UpdateLocalizedText(AttackSpeed);
        tmpStats[4].UpdateLocalizedText(speed);
        _slot.tmp_Nickname.GetComponent<LocalizedText>().UnUpdate();
        foreach (var checkslot in slotList)
        {
            checkslot.imgbutton.color = Color.white;
        }
        _slot.imgbutton.color = Color.blue;
        UserData.Instance.CurIndex = _slot.index;
    }

    public void GameStart()
    {
        bool exsitCharacter  = UserData.Instance.CurIndex >= 0;
        if (!exsitCharacter)
            return;

        SceneContainer.Instance.LoadScene(eSceneType.GameScene);
    }

    public void LoadDefaultData()
    {
        //tmpStats[0]
    }

    public void LoadSaveData()
    {

    }
}

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
                        var data = UserData.Instance.slots[slot.index];
                        tmpStats[0].UpdateLocalizedText(data.dungeonLevel);
                        tmpStats[1].UpdateLocalizedText(data.playerAttribute.MaxHart, data.playerAttribute.CurHart);
                        tmpStats[2].UpdateLocalizedText(data.playerAttribute.atk);
                        tmpStats[3].UpdateLocalizedText(data.playerAttribute.attackSpeed);
                        tmpStats[4].UpdateLocalizedText(data.playerAttribute.speed);
                    });

                }
                UserData.Instance.CurIndex = slot.index;
            });
        }

        img_Status.gameObject.SetActive(false);

        btn_Back.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            LobbySceneController.Instance.panel_Title.gameObject.SetActive(true);
        });

        btn_GameAccess.onClick.AddListener(GameStart);
    }


    public void GameStart()
    {
        //SaveData saveData = new SaveData();
        //saveData.attribute = model.character.attribute;
        //saveData.floow = 1;
        //saveData.nickname = panel_CharacterName.GetNickName();
        //UserData.Instance.saveData = saveData;
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

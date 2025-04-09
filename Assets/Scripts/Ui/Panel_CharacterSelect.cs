using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_CharacterSelect : MonoBehaviour
{
    [SerializeField]
    Panel_CharacterName panel_CharacterName;
    public LobbyModelController model;

    public TextMeshProUGUI[] tmpStats;

    public Button btn_Back;
    public Button btn_GameAccess;
    public Button btn_DelectCharacter;
    public Button btn_man;
    public Button btn_girl;

    public Image img_Status;
    public Image img_Class;
    
    public List<CreateCharacterSlot> slotList;

   
    private void Awake()
    {
        SetData();
    }

    public void SetData()
    {
        foreach (CreateCharacterSlot slot in slotList)
        {
            slot.BtnCreate.onClick.AddListener(() => 
            {
                panel_CharacterName.gameObject.SetActive(true);
                panel_CharacterName.SetData(slot);
                panel_CharacterName.btn_Ok.onClick.AddListener(() => 
                {
                    img_Status.gameObject.SetActive(true);
                    img_Class.gameObject.SetActive(true);
                    model.gameObject.SetActive(true);
                   
                });

            });
        }

        img_Status.gameObject.SetActive(false);
        img_Class.gameObject.SetActive(false);

        btn_Back.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            LobbySceneController.Instance.panel_Title.gameObject.SetActive(true);
        });

        btn_GameAccess.onClick.AddListener(GameStart);

        btn_man.onClick.AddListener(() =>
        {
            model.SelectMan();
            ChnageAttribute();
        });

        btn_girl.onClick.AddListener(() =>
        {
            model.SelectGirl();
            ChnageAttribute();
        });
    }

    private void ChnageAttribute()
    {
        tmpStats[0].text = model.character.attribute.MaxHart.ToString();
        tmpStats[1].text = model.character.attribute.atk.ToString();
        tmpStats[2].text = model.character.attribute.speed.ToString();
    }

    public void GameStart()
    {
        SaveData saveData = new SaveData();
        saveData.attribute = model.character.attribute;
        saveData.bodyIndex = model.GetActiveBodyIndex();
        saveData.headIndex = model.GetActiveHeadIndex();        
        saveData.nickname = panel_CharacterName.GetNickName();             
        UserData.Instance.saveData = saveData;
        SceneContainer.Instance.LoadScene(eSceneType.GameScene);
    }
}

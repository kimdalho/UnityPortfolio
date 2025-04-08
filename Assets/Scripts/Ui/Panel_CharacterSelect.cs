using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_CharacterSelect : MonoBehaviour
{
    [SerializeField]
    Panel_CharacterName panel_CharacterName;
    public ModelController model;

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

        btn_GameAccess.onClick.AddListener(() => 
        {
            SceneContainer.Instance.LoadScene(eSceneType.GameScene);
        });

        btn_man.onClick.AddListener(() =>
        {
            model.SelectMan();
        });

        btn_girl.onClick.AddListener(() =>
        {
            model.SelectGirl();
        });
    }






}

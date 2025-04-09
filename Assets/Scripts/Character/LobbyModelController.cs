

using UnityEngine;

public class LobbyModelController : ModelController
{
    private readonly int MAN = 0;
    private readonly int GIRL = 1;

    [Header("�κ��")]
    public SOGameAttributeData[] models;
    public ePlayerType playerType;

    public Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    public void SelectMan()
    {
        var ge = new GameEffect();
        ge.modifierOp = eModifier.Equal;
        //�̰� ��¥ �������� ������ ��Ʈ����Ʈ�� GE�� �������ִ°� �´ٰ� ����
        ge.ApplyGameplayEffectToSelf(character, models[MAN]);
        m_heads[MAN].gameObject.SetActive(true);
        m_heads[GIRL].gameObject.SetActive(false);
        m_bodys[MAN].gameObject.SetActive(true);
        m_bodys[GIRL].gameObject.SetActive(false);
        playerType = ePlayerType.Man;

    }

    public void SelectGirl()
    {
        var ge = new GameEffect();
        ge.modifierOp = eModifier.Equal;
        //�̰� ��¥ �������� ������ ��Ʈ����Ʈ�� GE�� �������ִ°� �´ٰ� ����
        ge.ApplyGameplayEffectToSelf(character, models[GIRL]);
        m_heads[MAN].gameObject.SetActive(false);
        m_heads[GIRL].gameObject.SetActive(true);
        m_bodys[MAN].gameObject.SetActive(false);
        m_bodys[GIRL].gameObject.SetActive(true);
        playerType = ePlayerType.Gril;
    }

    public int GetActiveBodyIndex()
    {
        for (int i = 0; i < m_bodys.Length; i++)
        {
            if( m_bodys[i].gameObject.activeSelf)
            {
                return i;
            }
        }
        return 0;
    }

    public int GetActiveHeadIndex()
    {
        for (int i = 0; i < m_heads.Length; i++)
        {
            if (m_heads[i].gameObject.activeSelf)
            {
                return i;
            }
        }
        return 0;
    }

    public int GetActiveWeaponIndex()
    {
        for (int i = 0; i < m_weapons.Length; i++)
        {
            if (m_weapons[i].gameObject.activeSelf)
            {
                return i;
            }
        }
        return 0;
    }
}
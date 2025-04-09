using UnityEngine;

public enum ePlayerType
{
    Man = 0,
    Gril = 1,
}

public class ModelController : MonoBehaviour
{
    public GameObject[] m_heads;
    public GameObject[] m_bodys;

    private readonly int MAN = 0;
    private readonly int GIRL = 1;

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


}

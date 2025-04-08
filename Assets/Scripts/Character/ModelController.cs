using UnityEngine;

public class ModelController : MonoBehaviour
{
    private readonly int MAN = 0;
    private readonly int GIRL = 1;

    public SOGameAttributeData[] models;

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

    }

    public void SelectGirl()
    {
        var ge = new GameEffect();
        ge.modifierOp = eModifier.Equal;
        //�̰� ��¥ �������� ������ ��Ʈ����Ʈ�� GE�� �������ִ°� �´ٰ� ����
        ge.ApplyGameplayEffectToSelf(character, models[GIRL]);
    }


}

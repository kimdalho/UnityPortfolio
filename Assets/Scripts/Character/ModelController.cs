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
        //이건 진짜 않좋은듯 정적인 어트리뷰트는 GE가 가지고있는게 맞다고 생각
        ge.ApplyGameplayEffectToSelf(character, models[MAN]);

    }

    public void SelectGirl()
    {
        var ge = new GameEffect();
        ge.modifierOp = eModifier.Equal;
        //이건 진짜 않좋은듯 정적인 어트리뷰트는 GE가 가지고있는게 맞다고 생각
        ge.ApplyGameplayEffectToSelf(character, models[GIRL]);
    }


}

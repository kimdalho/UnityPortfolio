using UnityEngine;
using UnityEngine.UI;

public class HeartIcon : MonoBehaviour
{
    [SerializeField]
    private Image maxhart;
    [SerializeField]
    private Image curhart;

    public void ShowMaxHeart(bool show)
    {
        maxhart.gameObject.SetActive(show);
    }

    public void ShowCurrentHeart(bool show)
    {
        curhart.gameObject.SetActive(show);
    }


}

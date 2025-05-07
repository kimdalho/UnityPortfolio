using UnityEngine;
using System.Text;

public class TagDebugger : MonoBehaviour
{
    // ����� �� �±׸� �������� �����;���
    public GameplayTagSystem tagSystem;
    public bool onTest;

    private void OnGUI()
    {
        if(onTest is false)
            return;
              
        if (tagSystem == null)
        {
            tagSystem = GameManager.instance.GetPlayer().GetGameplayTagSystem();
        }

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 34; // ���ϴ� ũ��� ���� (��: 24, 32, 48 ��)
        style.normal.textColor = Color.red;

        var tags = tagSystem.GetAllTags();
        StringBuilder sb = new StringBuilder("Active Tags:\n");

        foreach (var tag in tags)
        {
            sb.AppendLine($"- {tag}");
        }

        GUI.Label(new Rect(20, 20, 1000, 1000), sb.ToString(), style);
    }
}
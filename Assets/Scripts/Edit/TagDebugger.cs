using UnityEngine;
using System.Text;

public class TagDebugger : MonoBehaviour
{
    // ����� �� �±׸� �������� �����;���
    public GameplayTagSystem tagSystem;

    private void OnGUI()
    {
              
        if (tagSystem == null)
        {
            tagSystem = GameManager.instance.GetPlayer().gameplayTagSystem;
        }

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 34; // ���ϴ� ũ��� ���� (��: 24, 32, 48 ��)
        style.normal.textColor = Color.red;

        var tags = GameManager.instance.GetPlayer().gameplayTagSystem.GetAllTags();
        StringBuilder sb = new StringBuilder("Active Tags:\n");

        foreach (var tag in tags)
        {
            sb.AppendLine($"- {tag}");
        }

        GUI.Label(new Rect(20, 20, 1000, 1000), sb.ToString(), style);
    }
}
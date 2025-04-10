using UnityEngine;

using System.Collections.Generic;

public class GameplayTagSystem
{
    private HashSet<string> tags = new HashSet<string>();

    public void AddTag(string tag) => tags.Add(tag);
    public void RemoveTag(string tag) => tags.Remove(tag);
    public bool HasTag(string tag) => tags.Contains(tag);
}


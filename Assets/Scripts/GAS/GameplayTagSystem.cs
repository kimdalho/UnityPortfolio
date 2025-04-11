using UnityEngine;

using System.Collections.Generic;

[System.Serializable]
public class GameplayTagSystem
{
    private HashSet<eTagType> tags = new HashSet<eTagType>();

    public void AddTag(eTagType tag) => tags.Add(tag);
    public void RemoveTag(eTagType tag) => tags.Remove(tag);
    public bool HasTag(eTagType tag) => tags.Contains(tag);

    public IEnumerable<eTagType> GetAllTags()
    {
        return tags;
    }
}


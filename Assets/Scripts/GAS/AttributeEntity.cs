
using UnityEngine;

public class AttributeEntity : MonoBehaviour
{
    public delegate void OnHitdelegate();
    
    public OnHitdelegate onHit;

    public GameAttribute attribute;
}
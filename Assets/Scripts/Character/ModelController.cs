using System.Linq;
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
    public GameObject[] m_weapons;

    public void DisableAllParts()
    {
        m_heads.ToList().ForEach(obj => obj.SetActive(false));
        m_bodys.ToList().ForEach(obj => obj.SetActive(false));
        m_weapons.ToList().ForEach(obj => obj.SetActive(false));
    }

   
}

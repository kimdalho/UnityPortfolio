using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : MonoBehaviour, IFactory<MonsterDataSO, Monster>
{
    public List<MonsterDataSO> monsterDatas;

    private Dictionary<int, List<MonsterDataSO>> monsterLvDatas;


    public Monster CreateMonsterToLevel(int level ,Transform parent)
    {  
        if(monsterLvDatas == null)
        {
            monsterLvDatas = new();
            foreach (var monsterData in monsterDatas)
            {
                if (!monsterLvDatas.TryGetValue(monsterData.level, out var list))
                {
                    list = new List<MonsterDataSO>();
                    monsterLvDatas[monsterData.level] = list;
                }
                list.Add(monsterData);
            }
        }

        System.Random rnd = new System.Random();
        var data = monsterLvDatas[level][rnd.Next(monsterLvDatas[level].Count)];
        return Create(data, parent);
    }

    public Monster CreateMonsterToIndex(int index, Transform parent)
    {
        var data = monsterDatas[index];       
        return Create(data, parent);
    }

    public Monster CreateRandomMonster(Transform parent)
    {
        System.Random rnd = new System.Random();
        var prefab = monsterDatas[rnd.Next(monsterDatas.Count)];
        return Create(prefab, parent);     
    }

    public Monster Create(MonsterDataSO input, Transform parent)
    {
        var prefab = input.prefab;
        GameObject go = Instantiate(prefab, parent);
        Monster mon = go.GetComponent<Monster>();
        mon.SetData(input);
        return mon;
    }


}

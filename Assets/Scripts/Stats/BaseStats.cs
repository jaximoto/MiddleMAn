using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseStats", menuName = "Stats/BaseStats")]
public class BaseStats : ScriptableObject
{
    [Serializable]
    struct Relations
    {
        public string name;
        public int affinity;
    }

    
    public int money = 1000;
    public int workers = 10;
    public int productivity = 50;
    [SerializeField] List<Relations> relations;
   
   
}

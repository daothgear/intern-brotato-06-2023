using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterLevelData
{
    [System.Serializable]
    public class CharacterInfo
    {
        public int characterID;
        public int maxHP;
        public float moveSpeed;
    }

    public List<CharacterInfo> characterInfo = new List<CharacterInfo>();
}

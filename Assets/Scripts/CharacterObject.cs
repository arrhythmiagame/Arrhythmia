using System;
using UnityEngine;

[Serializable]
public class CharacterObject 
{
    public int saveIndex;
    public string characterName;
    public Color skinColor;
    public Color clothesColor;
    public Color weaponColor;
    public Vector3 startingPosition;
    public int maxHealth = 100;
    public int health;
    public string weaponName;
    public float speedMultiplier;
    public int buildType;
    public int hairType;
    public int faceType;

    public CharacterObject GetCharacterData()
    {
        return this;
    }
}

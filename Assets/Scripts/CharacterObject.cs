using System;
using UnityEngine;

[Serializable]
public class CharacterObject : MonoBehaviour
{
    public string characterName;
    public Color skinColor;
    public Color clothesColor;
    public Color weaponColor;
    public Vector3 startingPosition;
    public int maxHealth;
    public int health;
    public string weapon;
}

using UnityEngine;

[CreateAssetMenu(menuName = "Player/Stats")]
public class PlayerData : ScriptableObject
{
    public string playerName;
    public Sprite baseSprite;
    public MaskType startingMask;
    
    public int strength;
    public int intelligence;
    public int charisma;
}
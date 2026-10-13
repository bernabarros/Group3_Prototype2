using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    public string characterName;
    public int health;
    public int attackPower;
    public Attack[] attacks;
    public float[] attackChances;

    public Sprite characterSprite;
    public Sprite portraitSprite;
}

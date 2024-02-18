using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;


[CreateAssetMenu(fileName = "Bullet", menuName = "Bullets/BulletData")]
public class BulletData : ScriptableObject
{

    [Header("Info")]
    public new string name;

    [Header("Stats")]
    public int damage;
    public int size;

    [Header("UI & Animations")]
    public Sprite sprite;
    public AnimatorController AnimatorController;

    public Dictionary<string, int> UpdateStats()
    {
        return new Dictionary<string, int>
        {
            {"damage", damage},
            {"size", size},
        };
    }
}

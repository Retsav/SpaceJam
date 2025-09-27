using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public RarityEnum rarity;
    [FormerlySerializedAs("damageDice")] public string magazineSizeDice;
}

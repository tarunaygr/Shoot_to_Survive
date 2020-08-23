using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon",menuName ="Weapons")]
public class Weapon_Inventory : ScriptableObject {
    public float rof;
    public int MagSize;
    public float muzzle_vel;
}

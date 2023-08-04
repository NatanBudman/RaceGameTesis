using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Kart", menuName = "KartStats")]
public class KartStats : ScriptableObject
{
    public float MaxSpeed;
    public float MaxAcceleration;
    public float SteerDirSpeed;
}

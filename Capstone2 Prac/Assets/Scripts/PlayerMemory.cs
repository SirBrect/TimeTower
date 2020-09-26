using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMemory")]
public class PlayerMemory : ScriptableObject
{
    [SerializeField]
    private Transform respawn;

    public void SetRespawn(Transform newRespawn)
    {
        respawn = newRespawn;
    }

    public Vector3 GetRespawnPos()
    {
        return respawn.position;
    }
}

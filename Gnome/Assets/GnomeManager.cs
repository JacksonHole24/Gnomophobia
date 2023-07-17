using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeManager : MonoBehaviour
{
    public void ResetSpawns()
    {
        foreach(Transform child in transform)
        {
            if(child.TryGetComponent<GnomeSpawner>(out GnomeSpawner spawn))
            {
                spawn.SpawnGnomes();
            }
        }
    }
}

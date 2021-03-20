using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    /// <summary>
    /// Коллекция блоков дороги
    /// </summary>
    List<GameObject> currentParts;

    private void Start()
    {
        currentParts = RoadSpawnScript.currentRoad;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Уничтожение блока дороги
        if (other.tag.ToLower() == "player")
        {
            Destroy(currentParts.FirstOrDefault());
            currentParts.Remove(currentParts.FirstOrDefault());
        }
    }
}

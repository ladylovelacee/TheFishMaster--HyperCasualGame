using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<Fish> fishPrefabs = default;

    void Start()
    {
        for (int i = 0; i < fishPrefabs.Count; i++)
        {
            for (int j = 0; j < fishPrefabs[i].FishData.FishCount; j++)
            {
                Fish fish = (Fish)Instantiate(fishPrefabs[i]);
                fish.ResetFish();
            }
        }
    }
}

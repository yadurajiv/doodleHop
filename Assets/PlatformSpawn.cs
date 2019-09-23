using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour {

    // the platform prefab
    public GameObject platformPrefab;

    // how many platforms does our level need
    public int numberOfPlatforms = 120;

    // the width of the level, -3 to +3
    public float levelWidth = 3f;

    // what is the minimum distance between platforms
    public float minY = 1f;

    // what is the maximum distance between platforms
    public float maxY = 3f;

    void Start() {

        // our spawn position
        Vector3 spawnPosition = new Vector3();

        // loop through and instantiate numberOfPlatforms with given variables
        for (int i = 0; i < numberOfPlatforms; i++) {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }

        //TODO: Instantiate an end zone.. like when the player reaches the end - at SpawnPosition
    }

}

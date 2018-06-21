using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public float timeBtwnSpawn;
    public GameObject objectToSpawn;
    float spawntime;

	// Use this for initialization
	void Start () {
        spawntime = timeBtwnSpawn;
    }
	
	// Update is called once per frame
	void Update () {
        spawntime -= Time.deltaTime;
        if (spawntime <= 0)
        {
            //print("spawn");
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            spawntime = timeBtwnSpawn;
        }

	}
}

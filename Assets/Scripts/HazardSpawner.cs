using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    public GameObject hazardPrefab;
    public Vector2 spawnRateMinMax;

    public Vector2 spawnSizeMinMax;
    public float spawnAngleMax;
    public Material[] materials;

    float nextSpawnTime;
    Vector2 screenHalfSizeWorldUnits;

    // Start is called before the first frame update
    void Start()
    {
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + 1f / Mathf.Lerp(spawnRateMinMax.x, spawnRateMinMax.y, Difficulty.getDifficultyPercent());

            float size = Random.Range(spawnSizeMinMax.x, spawnSizeMinMax.y);
            float angle = Random.Range(-spawnAngleMax, spawnAngleMax);
            Vector2 position = new Vector2(Random.Range(-screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.x), screenHalfSizeWorldUnits.y + transform.localScale.y * size);

            var newObject = Instantiate(hazardPrefab, position, Quaternion.Euler(Vector3.forward * angle));
            newObject.transform.localScale = Vector2.one * size;

            var renderer = newObject.GetComponent<MeshRenderer>();
            renderer.material = materials[Random.Range(0, materials.Length)];
        }

        
    }
}

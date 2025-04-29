using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    [Header("Building Settings")]
    public GameObject[] buildingPrefabs;
    public float sideOffset = 15f;
    public float minZBuffer = 5f;
    public int buildingsPerSegment = 3;

    [Header("Road Segments")]
    public Transform[] roadSegments;

    private struct SpawnedBuilding
    {
        public Vector3 position;
        public Vector3 size; // Approximate bounds
    }

    void Start()
    {
        SpawnBuildings();
    }

    public void SpawnBuildings()
    {
        if (buildingPrefabs.Length == 0 || roadSegments.Length == 0)
        {
            Debug.LogWarning("BuildingPrefabs or RoadSegments are not assigned.");
            return;
        }

        List<SpawnedBuilding> spawnedBuildings = new List<SpawnedBuilding>();

        foreach (Transform road in roadSegments)
        {
            float segmentLength = road.localScale.z;
            float startZ = road.position.z - segmentLength / 2;
            float endZ = road.position.z + segmentLength / 2;

            int spawnedCount = 0;
            int attempts = 0;
            int maxAttempts = buildingsPerSegment * 10;

            while (spawnedCount < buildingsPerSegment && attempts < maxAttempts)
            {
                GameObject prefab = buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];

                // Get prefab bounds (approximate)
                Renderer prefabRenderer = prefab.GetComponentInChildren<Renderer>();
                if (prefabRenderer == null)
                {
                    Debug.LogWarning("Prefab missing Renderer: " + prefab.name);
                    continue;
                }

                Vector3 prefabSize = prefabRenderer.bounds.size;

                float spawnZ = Random.Range(startZ + minZBuffer, endZ - minZBuffer);
                float side = Random.value > 0.5f ? 1f : -1f;
                Vector3 spawnPos = new Vector3(road.position.x + side * sideOffset, 0, spawnZ);

                // Check for overlap using bounding boxes
                bool overlaps = false;
                foreach (var b in spawnedBuildings)
                {
                    float distanceZ = Mathf.Abs(spawnPos.z - b.position.z);
                    float combinedHalfZ = (prefabSize.z + b.size.z) / 2f;

                    if (Mathf.Abs(spawnPos.x - b.position.x) < 0.1f && distanceZ < combinedHalfZ)
                    {
                        overlaps = true;
                        break;
                    }
                }

                if (!overlaps)
                {
                    GameObject building = Instantiate(prefab, spawnPos, Quaternion.Euler(-90f, 0f, 0f));
                    building.transform.parent = road;

                    spawnedBuildings.Add(new SpawnedBuilding
                    {
                        position = spawnPos,
                        size = prefabSize
                    });

                    spawnedCount++;
                }

                attempts++;
            }
        }
    }
}

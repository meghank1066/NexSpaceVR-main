using UnityEngine;

public class EndlessRoadManager : MonoBehaviour
{
    public GameObject[] roadSegments; // Holds the three road segments
    public float roadLength = 50f; // Length of each road segment
    public Transform car; // Reference to the car

    void Update()
    {
        // Prevent errors if roadSegments isn't set correctly
        if (roadSegments == null || roadSegments.Length < 3 || car == null)
        {
            Debug.LogError("EndlessRoadManager: roadSegments or car is not set properly in the Inspector!");
            return;
        }

        // Check if the car has reached the end of Segment 2
        float segment2End = roadSegments[1].transform.position.z + roadLength / 2;

        if (car.position.z > segment2End)
        {
            MoveRoadForward();
        }
    }

    void MoveRoadForward()
    {
        GameObject oldSegment = roadSegments[0];
        float newZ = roadSegments[2].transform.position.z + roadLength;

        oldSegment.transform.position = new Vector3(0, 0, newZ);

        // Shift segments
        roadSegments[0] = roadSegments[1];
        roadSegments[1] = roadSegments[2];
        roadSegments[2] = oldSegment;

        // Remove old buildings and spawn new ones
        foreach (Transform child in oldSegment.transform)
        {
            Destroy(child.gameObject);
        }
        FindFirstObjectByType<BuildingSpawner>().SpawnBuildings();

        Debug.Log("Road moved forward, buildings spawned!");
    }

}

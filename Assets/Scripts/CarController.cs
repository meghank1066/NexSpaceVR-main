using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        // Move the car forward automatically
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : MonoBehaviour
{

    [Tooltip("A list of explosions to choose from")]
    public List<GameObject> explosions;
    [Tooltip("Radius of the randomly generated explosion area")]
    public float radius = 5f;
    [Tooltip("Amount of explosions generated per frame")]
    public int explosionsPerFrame = 1;

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < explosionsPerFrame; i++)
        {
            // Generate random explosion and rotation
            int explosionsPos = Random.Range(0, explosions.Count);
            float a = Random.Range(0f, 1f) * 2 * Mathf.PI;
            float r = radius * Mathf.Sqrt(Random.Range(0f, 1f));
            float randX = r * Mathf.Cos(a);
            float randY = r * Mathf.Sin(a);

            // Create explosion
            GameObject explosionPrefab = Instantiate(explosions[explosionsPos], 
                new Vector3(transform.position.x + randX, transform.position.y + randY, transform.position.z), 
                transform.rotation, 
                transform);
        }
    }
}

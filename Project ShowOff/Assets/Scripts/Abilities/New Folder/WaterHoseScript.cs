using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct point
{
    public Vector3 position;
    public Vector3 velocity;

    public point (Vector3 position, Vector3 velocity)
    {
        this.position = position;
        this.velocity = velocity;
    }
}

public class WaterHoseScript : MonoBehaviour
{

    List<Vector3> positions;
    List<Vector3> velocities;

    [SerializeField]
    float spawnTimer;
    [SerializeField]
    float spawnDelay;

    [SerializeField]
    float gravity;
    [SerializeField]
    float velocity;

    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector3>();
        velocities = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePoints();

        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnDelay)
        {
            //points.Add(new point(transform.position, transform.forward));
            positions.Add(new Vector3(transform.position.x, transform.position.y, transform.position.z));
            velocities.Add(new Vector3(transform.forward.x, transform.forward.y, transform.forward.z) * velocity + Vector3.down * gravity * Time.deltaTime);
            spawnTimer -= spawnDelay;
        }
    }

    private void OnDrawGizmos()
    {
        if (positions != null)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                Gizmos.DrawCube(positions[i], Vector3.one * 0.1f);
            }
        }
    }

    void UpdatePoints()
    {
        for(int i = positions.Count - 1; i >= 0; i--)
        {
            positions[i] += velocities[i] * Time.deltaTime;

            velocities[i] *= 0.99f;
            velocities[i] += Vector3.down * gravity * Time.deltaTime;
        }
    }
}

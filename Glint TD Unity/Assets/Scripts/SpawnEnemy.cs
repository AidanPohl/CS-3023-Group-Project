using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public float enemySpeed = 5f;
    public float enemyHealth = 10f;
    private Transform dest;
    private int initialIndex; 
    

    // Start is called before the first frame update
    void Start()
    {
        dest = WaypointBehavior.pointsArray[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = dest.position - transform.position; 
        //transform.Translate 
    }
}

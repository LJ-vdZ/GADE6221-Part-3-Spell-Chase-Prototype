using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject fireballPrefab;

    [SerializeField]
    private float shootSpeed = 10f;

    [SerializeField]
    private Transform player; //reference to player for LookAt

    [SerializeField]
    private float spawnInterval = 1f; //time between spawns

    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set player reference for LookAt
        if (player == null)
        {
            player = GameObject.Find("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //handle spawning with timer
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnFireball();
            timer = 0f;
        }
    }

    void SpawnFireball()
    {
        //instantiate fireball at spawner
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

        //make fireball face player
        fireball.transform.LookAt(player);

        //get rigidbody on fireball
        Rigidbody rb = fireball.GetComponent<Rigidbody>();

        //shoot towards player
        if (rb != null)
        {
            Vector3 direction = (player.position - fireball.transform.position).normalized;

            rb.AddForce(direction * shootSpeed, ForceMode.Impulse);
        }


        //destroy fireball after 5 seconds
        Destroy(fireball, 5f);
    }
}

//Unity Documentation, [s.a.]. Vector3.normalized. [online] Available at: <https://docs.unity3d.com/6000.1/Documentation/ScriptReference/Vector3-normalized.html>[Accessed 01 June 2025].
//Unity Discussions, [s.a.]. Creating a Repulsion System. [online] Available at: <https://discussions.unity.com/t/creating-a-repulsion-system/641373>[Accessed 01 June 2025].
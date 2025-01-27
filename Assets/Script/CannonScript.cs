using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public List<GameObject> bombPrefabs;
    public Vector2 timeInterval = new Vector2(1, 1);
    public GameObject spawnPoint;
    public GameObject target;
    public float rangeInDegress;
    public Vector2 force;
    public float arcDegres;
    private float cooldown;


    // Start is called before the first frame update
    void Start()
    {
        cooldown = Random.Range(timeInterval.x, timeInterval.y);  
    }

    // Update is called once per frame
    void Update()
    {
        // ignore if game is over
        if (GameManager.Instance.isGameOver) return;
        cooldown -= Time.deltaTime;

        // update cooldown
        if(cooldown < 0) {
            cooldown = Random.Range(timeInterval.x, timeInterval.y);

            Fire();
        }
    }
    private void Fire() {
        // Get prefab
        GameObject bombPrefab = bombPrefabs[Random.Range(0, bombPrefabs.Count)];
       // Create Bomb
        GameObject bomb =Instantiate(bombPrefab, spawnPoint.transform.position, bombPrefab.transform.rotation);
      
       // Apply force
        Rigidbody bombRigidbody = bomb.GetComponent<Rigidbody>();
        Vector3 impulseVector = target.transform.position - spawnPoint.transform.position;
        impulseVector.Scale(new Vector3(1, 0, 1));
        impulseVector.Normalize();
        impulseVector += new Vector3(0, arcDegres / 45f, 0);
        impulseVector.Normalize();
        impulseVector = Quaternion.AngleAxis(rangeInDegress * Random.Range(-1f, 1f), Vector3.up) * impulseVector;
        impulseVector *= Random.Range(force.x, force.y);
        bombRigidbody.AddForce(impulseVector, ForceMode.Impulse);
    }
}

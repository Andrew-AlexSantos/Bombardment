using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public float ExplosionDelay = 5f;

    public GameObject ExplosionPrefab;

    public GameObject WoodBreakingPrefab;

    public float BlastRadius = 5f;
    public int BlastDamage = 10;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplosionCourotine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator ExplosionCourotine(){
        // wait
        yield return new WaitForSeconds(ExplosionDelay);

        // explode
        Explode();
    }
    private void Explode() {
        // Create Explosion
        Instantiate(ExplosionPrefab, transform.position, ExplosionPrefab.transform.rotation);


        // Destroy plataforms
        Collider[] colliders = Physics.OverlapSphere(transform.position, BlastRadius);
        foreach(Collider collider in colliders) {
            GameObject hitObject = collider.gameObject;
            if(hitObject.CompareTag("Plataform")){
                LifeScript lifeScript = hitObject.GetComponent<LifeScript>();
                if(lifeScript != null) {
                    float distance = (hitObject.transform.position - transform.position).magnitude;
                    float distanceRate = Mathf.Clamp(distance / BlastRadius, 0, 1);
                    float damageRate = 1f - Mathf.Pow(distanceRate, 4);
                    int damage = (int) Mathf.Ceil(damageRate * BlastDamage);
                    lifeScript.health -= damage;
                    if(lifeScript.health <= 0) {
                        
                        Instantiate(WoodBreakingPrefab, hitObject.transform.position, WoodBreakingPrefab.transform.rotation);

                        Destroy(hitObject);
                    }
                }
            }
        }
        // Create SFX

        // Destroy bomb
        Destroy(gameObject);
    }
}

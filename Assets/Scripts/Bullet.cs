using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Floats")]
    public float speed;
    public float lifeTime;
    public float damage;
    [Header("Direction")]
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(direction);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(this.gameObject, lifeTime);
    }

    public void GetDirecction(Vector3 dir) {
        direction = dir;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Enemy")) {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.GetDamage(damage);
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject);

    }
}

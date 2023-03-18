using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Point")]
    public Transform targat;
    public int point;
    [Header("EnemyFinder")]
    public bool findEnemyes;
    public GameObject enemyFinder;
    public float radius;
    public List<GameObject> enemyes = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        targat = Points.points[point];
        findEnemyes = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindEnemy(bool value) {
        findEnemyes = value;
    }

    public void Detect() {
        if(findEnemyes) {
            FindEnemyes();
            findEnemyes = false;
        }

        if(enemyes.Count <= 0) {
            point++;
            targat = Points.points[point];
        }

        if(point >= Points.points.Length - 1) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void FindEnemyes() {
        findEnemyes = false;

        enemyFinder.transform.position = Points.points[point + 1].position;
        Collider[] colliders = Physics.OverlapSphere(enemyFinder.transform.position, radius);
        for(int i = 0; i < colliders.Length; i++) {
            Enemy enemy = colliders[i].GetComponent<Enemy>();

            if(enemy) {
                if(enemy.HP > 0) enemyes.Add(enemy.gameObject);
                else if(enemy.HP <= 0) enemyes.Remove(enemy.gameObject);
            }
        }
    }
}

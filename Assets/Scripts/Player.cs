using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Point")]
    public Transform targat;
    public int point;
    [Header("Shoot")]
    public GameObject weapon;
    public GameObject bullet;
    public Transform shotPos;
    public float bulletSpeed;

    GameObject manager;
    GameManager gameManager;
    NavMeshAgent navMesh;
    Animator anim;
    Vector3 oldPos;
    Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager");
        gameManager = manager.GetComponent<GameManager>();
        navMesh = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        targat = gameManager.targat;
        oldPos = this.transform.position;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        targat = gameManager.targat;
        
        if(oldPos != transform.position) {
            anim.Play("Run");
            oldPos = transform.position;
        }
        else if(oldPos == transform.position) anim.Play("Idle");

        if(Vector3.Distance(transform.position, targat.position) >= 1f) navMesh.SetDestination(targat.position);
        else if(Vector3.Distance(transform.position, targat.position) <= 1f) gameManager.Detect();

        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                anim.Play("Fire");
                Vector3 targetPos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                transform.LookAt(targetPos);

                GameObject bull = (GameObject) Instantiate(bullet, shotPos.position, transform.rotation);
                Bullet b = bull.GetComponent<Bullet>();
                b.GetDirecction(targetPos);
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Target")) {
            gameManager.FindEnemy(true);
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.collider.CompareTag("Target"))
            gameManager.FindEnemy(true);
    }
}

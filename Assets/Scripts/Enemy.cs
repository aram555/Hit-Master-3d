using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Float")]
    public float HP;
    private float startHP;
    [Header("Ragdoll")]
    public Rigidbody[] rbRagdoll;
    [Header("HealthBar")]
    public Slider healthBar;
    public GameObject HealthBar;

    NavMeshAgent navmesh;
    Animator anim;
    Vector3 oldPos;
    GameObject manager;
    // Start is called before the first frame update

    private void Awake() {
        for(int i = 0; i < rbRagdoll.Length; i++) {
            rbRagdoll[i].isKinematic = true;
        }
    }

    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        oldPos = this.transform.position;
        manager = GameObject.Find("GameManager");
        startHP = HP;
    }

    // Update is called once per frame
    void Update()
    {
        if(oldPos != transform.position) {
            anim.Play("Run");
            oldPos = transform.position;
        }
        else if(oldPos == transform.position) anim.Play("Idle");

        if(HP <= 0) {
            GameManager gameManager = manager.GetComponent<GameManager>();
            gameManager.enemyes.Remove(this.gameObject);
            HealthBar.SetActive(false);
            Ragdoll();
        }
        else {
            healthBar.maxValue = startHP;
            healthBar.value = HP;
        }
    }

    public void GetDamage(float damage) {
        HP -= damage;
    }

    public void Ragdoll() {
        anim.enabled = false;
        navmesh.enabled = false;
        for(int i = 0; i < rbRagdoll.Length; i++) {
            rbRagdoll[i].isKinematic = false;
        }
    }
}

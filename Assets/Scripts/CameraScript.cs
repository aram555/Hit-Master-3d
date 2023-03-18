using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform Player;
    public GameManager manager;

    Player player;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.GetComponent<Player>();
        gameManager = manager.GetComponent<GameManager>();
    }
    // Update is called once per frame
    private void LateUpdate() {
        transform.position = Player.position;
        transform.LookAt(gameManager.enemyFinder.transform.position);
    }
}

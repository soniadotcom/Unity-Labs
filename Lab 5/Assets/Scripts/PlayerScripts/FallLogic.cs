using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallLogic : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        playerMovement = player.GetComponent<PlayerMovement>();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerMovement.PlayerHealth = -1;
            Debug.Log(playerMovement.PlayerHealth);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystem : MonoBehaviour
{
    public GameObject[] hearts;
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

    // Update is called once per frame
    void Update()
    {
        if(playerMovement.PlayerHealth < 1)
        {
            Destroy(hearts[0].gameObject);
        }
        if (playerMovement.PlayerHealth < 2)
        {
            Destroy(hearts[1].gameObject);
        } 
        if (playerMovement.PlayerHealth < 3)
        {
            Destroy(hearts[2].gameObject);
        }
    }
}

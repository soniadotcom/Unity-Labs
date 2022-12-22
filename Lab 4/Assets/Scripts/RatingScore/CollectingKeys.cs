using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingKeys : MonoBehaviour
{

    public int keys;
    public int keysRequired = 1;
    [SerializeField] GameObject levelLoader; 
    
    public void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.gameObject.tag == "Key")
        {
            Debug.Log("Key collected!");
            keys += 1;
            Destroy(Col.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12 && keys == keysRequired)
        {
            levelLoader.GetComponent<LevelLoader>().LoadNextLevel();
        }
    }
}

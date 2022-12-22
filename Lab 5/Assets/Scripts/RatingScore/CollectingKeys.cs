using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingKeys : MonoBehaviour
{

    public int keys;
    public int keysRequired = 1;
    [SerializeField] GameObject levelLoader;

    private bool hasCollidedChest = false;
    
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
        if (!hasCollidedChest && collision.gameObject.layer == 12 && keys >= keysRequired)
        {
            hasCollidedChest = true;
            Keys.keys += keys;
            Debug.Log("All keys: " + Keys.keys);
            
            levelLoader.GetComponent<LevelLoader>().LoadNextLevel();
        }
    }
}

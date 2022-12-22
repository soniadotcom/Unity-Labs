using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public GameObject player;

	public PlayerMovement playerMovement;

    public enum gameStates 
    {
		Menu,
		Playing,
        Death,
        Victory
    };

    public gameStates gameState = gameStates.Menu;

	void OnPlayingListener(Scene scene, LoadSceneMode mode)
	{
		if (scene.buildIndex >= 2 && scene.buildIndex != 4)
		{
			if (player == null)
			{
				player = GameObject.FindWithTag("Player");
			}

			playerMovement = player.GetComponent<PlayerMovement>();

		}
	}

	private void Awake()
	{
		SceneManager.sceneLoaded -= OnPlayingListener;
		SceneManager.sceneLoaded += OnPlayingListener;
	}

	void Start()
    {
		if (gm == null)
		{
			gm = gameObject.GetComponent<GameManager>();
			Debug.Log("gm is found " + gm.gameState);
		}
	}

	void Update()
    {
		switch (gameState)
		{
			case gameStates.Menu:
				if (Input.GetKeyDown(KeyCode.Return))
                {
					Debug.Log("Exiting Menu");
					StartTheGame();
				}
				break;
			case gameStates.Playing:
				if (playerMovement is null)
					break;

				if (playerMovement.PlayerHealth <= 0)
				{
					Debug.Log("Death");
					Keys.keys = 0;
					// update gameState
					gameState = gameStates.Death;
					SceneManager.LoadScene(1);
				}
				//beatLevelScore...
				break;
			case gameStates.Death:
				if (Input.GetKeyDown(KeyCode.Return))
				{
					GoToMenu();
				}
				break;
			case gameStates.Victory:
				Debug.Log("Victory");
				if (Input.GetKeyDown(KeyCode.Return))
				{
					GoToMenu();
				}
				break;
		}
	}

	public void StartTheGame()
	{
		Debug.Log("Starting the game...");
		gameState = gameStates.Playing;
		SceneManager.LoadScene(2);
	}

	public void GoToMenu()
	{
		Debug.Log("Returning to Menu...");
		gameState = gameStates.Menu;
		SceneManager.LoadScene(0);
	}
}

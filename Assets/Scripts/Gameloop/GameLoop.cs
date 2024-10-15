using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


using static StatsManager;

public class GameLoop : MonoBehaviour
{

	public StatsManager statsManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }


	void StartDay()
	{
		// Spawn tasks, display them to player
	}


	public void EndDay()
	{
		// Check which tasks have been fulfilled, update affinities


		// Check which tasks expire, update affinities
		

		// Update player stats based on new affinities
		statsManager.UpdateStats();


		// If run out of money or affinity for anyone zero lose
		if (CheckLose())
		{
			SceneManager.LoadScene("GameOver");
		}
		else
		{
			StartDay();
		}

	}



	// Wrap your lips around me
	private bool CheckLose()
	{
		return statsManager.CheckLose();
	}
}

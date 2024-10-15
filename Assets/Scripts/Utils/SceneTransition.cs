using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils {
public class SceneTransition: MonoBehaviour
{

	public string sceneName;

	public void LoadScene()
	{
		SceneManager.LoadScene(sceneName);
	}

}
}

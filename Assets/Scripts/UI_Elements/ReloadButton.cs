using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadButton : MonoBehaviour
{
	[SerializeField] string scene_name;
	
    public void NewScene () {
		
		SceneManager.LoadScene(scene_name);
	}
}

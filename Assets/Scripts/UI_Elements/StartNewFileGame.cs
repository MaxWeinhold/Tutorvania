using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNewFileGame : MonoBehaviour
{
    public void NewScene()
    {
        SceneManager.LoadScene("Level1");
    }
}

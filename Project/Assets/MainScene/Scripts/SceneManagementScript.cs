using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerr : MonoBehaviour
{
    public void LoadScene(string HomeMenuScene)
    {
        SceneManager.LoadScene(HomeMenuScene);
    }
}

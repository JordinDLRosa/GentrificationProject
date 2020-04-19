using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChange : MonoBehaviour
{
    public Animator animator;
    private int sceneToLoad;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) ;
        {
            FadeToScene(1);
        }
    }
    //From the index in buildSettings, it will go up the next number = next Scene
    public void FadeToNextScene()
    {
        FadeToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //int levelIndex to change from one scene to the next using the numbers from buildSettings
    //Public function to access buildSettings and says that it will fade into another scene
    public void FadeToScene(int  sceneIndex) 
    {
        sceneToLoad = sceneIndex;
        animator.SetTrigger("Text");
        animator.SetTrigger("FadeOut");
    }
    //Loads the scene

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
 

    void Start()
    {
        // Listens for UI Events
        EventManager.instance.AddListener(EventIDs.ON_PLAY_TRIGGER, SceneLoad_Level);
        EventManager.instance.AddListener(EventIDs.ON_QUIT_TRIGGER, QuitApplication);
    }
    void OnDisable()
    {
        // Removes listeners when the UI is inactive
        EventManager.instance.RemoveListener(EventIDs.ON_PLAY_TRIGGER, SceneLoad_Level);
        EventManager.instance.RemoveListener(EventIDs.ON_QUIT_TRIGGER, QuitApplication);
    }

    /* Load the Main Level */
    void SceneLoad_Level()
    {
        SceneManager.LoadScene(EventIDs.SCENE_MAIN_LEVEL);
    }

    /* Quit Application */
    void QuitApplication()
    {
        Application.Quit();
    }

}

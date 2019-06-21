using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitTrigger : MonoBehaviour
{

    bool IsQuit = false;

    public void PlayGame()
    {
        if (!IsQuit)
        {
            EventManager.instance.SendEvent(EventIDs.ON_QUIT_TRIGGER);
            IsQuit = true;
        }
        else
        {
            IsQuit = false;
        }

    }
}

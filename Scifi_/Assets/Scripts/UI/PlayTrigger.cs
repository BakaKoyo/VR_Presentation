using UnityEngine;

public class PlayTrigger : MonoBehaviour
{

    bool IsPlay = false;

    public void PlayGame()
    {
        if (!IsPlay)
        {
            EventManager.instance.SendEvent(EventIDs.ON_PLAY_TRIGGER);
            IsPlay = true;
        }
        else
        {
            IsPlay = false;
        }

    }
}

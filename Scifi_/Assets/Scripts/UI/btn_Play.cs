using UnityEngine;
using UnityEngine.EventSystems;

public class btn_Play : MonoBehaviour
{

    [Header("Gaze Properties")]
    [Tooltip("Specifies how long the user has to look at the interactable")]
    [SerializeField] [Range(0f, 3f)] float gaze_Time;
    [Tooltip("Debugging purposes to show how long the user has " +
             "looked into the interactable")]
    [SerializeField] float gaze_Timer;
    [Tooltip("Trigger for the events")]
    [SerializeField] bool IsGazedAt;

    // Update is called once per frame
    void Update()
    {

        /* Increases the timer and if the timer exceeds the 
         * "x" amount of gaze needed for the interactable UI then send event */
        if (IsGazedAt)
        {
            gaze_Timer += Time.deltaTime;

            if (gaze_Timer >= gaze_Time)
            {
                // execute pointerdown handler
                ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current),
                                      ExecuteEvents.pointerDownHandler);
                gaze_Timer = 0f;
            }
        }
        /* Else just reset the timer */
        else
        {
            gaze_Timer = 0f;
        }

    }

    public void PointerEnter()
    {
        IsGazedAt = true;
    }

    public void PointerExit()
    {
        IsGazedAt = false;
    }

}

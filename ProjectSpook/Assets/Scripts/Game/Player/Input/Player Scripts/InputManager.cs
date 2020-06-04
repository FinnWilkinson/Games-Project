using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class InputManager : MonoBehaviour
{
    //These header fields allow for us to assign functions and inputs to their subfields within unity
    [Header("Trigger")]
    public SteamVR_Action_Boolean TriggerAction = null;
    public UnityEvent OnTriggerDown = new UnityEvent();
    public UnityEvent OnTriggerUp = new UnityEvent();

    [Header("Grip buttons")]
    public SteamVR_Action_Boolean GripAction = null;
    public UnityEvent OnGripDown = new UnityEvent();
    public UnityEvent OnGripUp = new UnityEvent();

    [Header("Trackpad button")]
    public SteamVR_Action_Boolean TrackpadAction = null;
    public UnityEvent OnTrackpadDown = new UnityEvent();
    public UnityEvent OnTrackpadUp = new UnityEvent();

    //Pose is the postion and location of the controller itself, it's essentially the physical controller
    private SteamVR_Behaviour_Pose Pose = null;

    private void Awake()
    {
        Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    //Every update, check the input
    private void Update()
    {
        //Want to check the state of button presses from the pose input source, i.e. has the assigned button on the controller been pressed
        if (TriggerAction.GetStateDown(Pose.inputSource))
            OnTriggerDown.Invoke();

        if (TriggerAction.GetStateUp(Pose.inputSource))
            OnTriggerUp.Invoke();

        if (GripAction.GetStateDown(Pose.inputSource))
            OnGripDown.Invoke();

        if (GripAction.GetStateUp(Pose.inputSource))
            OnGripUp.Invoke();

        if (TrackpadAction.GetStateDown(Pose.inputSource))
            OnTrackpadDown.Invoke();

        if (TrackpadAction.GetStateUp(Pose.inputSource))
            OnTrackpadUp.Invoke();

    }
    
}
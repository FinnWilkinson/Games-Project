using UnityEngine;
using Valve.VR;

public class Interactable : MonoBehaviour
{
    //Variable to track if item is socketed into a hand or not
    protected bool isAvailable = true;

    //Virtual function is one that can be overwritten from a class that inherits from it
    public virtual void Interact(Hand hand)
    {
        
    }

    public virtual void EndInteraction(Hand hand)
    {
        
    }

    public bool GetAvailability()
    {
        return isAvailable;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InteractableDectector
{
    //A generic class where the generic type is an interactable
    public static T GetNearestInteractable<T>(Vector3 origin, List<T> collection)
        where T : Interactable
    {
        T nearest = null;
        float minDistance = float.MaxValue;
        float distance;

        // For each object we are touching
        foreach (T entity in collection)
        {
            if (!entity)
                continue;

            //Only look for available objects
            if (!entity.GetAvailability())
                continue;
            
            //Calculate the distance from the object
            distance = (entity.gameObject.transform.position - origin).sqrMagnitude;

            //In theory we could be in contact with multiple interactables with one hand, so we only want to get the closest one

            //If the distance is less than the minDistance, update the minimum distance and set the nearest interactable to this entity
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = entity;
            }
        }

        return nearest;
    }
}
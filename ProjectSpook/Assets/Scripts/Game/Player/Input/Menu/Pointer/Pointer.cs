﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{

    public float m_DefaultLength = 5.0f;
    public GameObject m_Dot;
    public PointerInput m_InputModule;

    public Camera m_Camera { get; private set; } = null;

    private LineRenderer m_LineRenderer = null;

    private void Awake()
    {
        m_Camera = GetComponent<Camera>();
        m_Camera.enabled = false;
        m_LineRenderer = GetComponent<LineRenderer>();

    }   

    // Update is called once per frame
    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        // use default or dstance
        PointerEventData data = m_InputModule.GetData();

        // raycast
        RaycastHit hit = CreateRaycast();


        float colliderDistance = (hit.distance == 0) ? m_DefaultLength : hit.distance;
        float canvasDistance = (data.pointerCurrentRaycast.distance == 0) ? m_DefaultLength : data.pointerCurrentRaycast.distance;

        float targetLength = Mathf.Min(colliderDistance, canvasDistance);
        
        

        // default
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        // or based on hit
        if (hit.collider != null)
            endPosition = hit.point;

        // set position of dot
        m_Dot.transform.position = endPosition;

        // set position of line renderer
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);
        return hit;
    }
}

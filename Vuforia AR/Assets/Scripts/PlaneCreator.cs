using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EventHandler))]
public class PlaneCreator : MonoBehaviour
{

    private EventHandler m_EventHandler = null;
    private MeshRenderer m_Renderer = null;

    private void Awake()
    {
        Setup();
        m_EventHandler = GetComponent<EventHandler>();

        m_EventHandler.OnTrackingFound += Show;
        m_EventHandler.OnTrackingLost += Hide;
    }

    private void OnDestroy()
    {
        m_EventHandler.OnTrackingFound -= Show;
        m_EventHandler.OnTrackingLost -= Hide;
    }

    private void Setup()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Quad);
        plane.transform.parent = transform;
        plane.transform.localEulerAngles = new Vector3(90, 0, 0);

        m_Renderer = plane.GetComponent<MeshRenderer>();
        m_Renderer.material = Resources.Load<Material>("M_Rocks");

        Hide();
    }

    private void Show()
    {
        m_Renderer.enabled = true;
    }

    private void Hide()
    {
        m_Renderer.enabled = false;
    }
}

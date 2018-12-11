using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;

public class EventHandler : MonoBehaviour, ITrackableEventHandler
{

    public UnityAction OnTrackingFound;
    public UnityAction OnTrackingLost;

    private TrackableBehaviour m_TrackableBehavior = null;

    private readonly List<TrackableBehaviour.Status> m_TrackingFound = new List<TrackableBehaviour.Status>()
    {
        TrackableBehaviour.Status.DETECTED,
        TrackableBehaviour.Status.TRACKED,

        // Device positioning
        TrackableBehaviour.Status.EXTENDED_TRACKED
    };

    private readonly List<TrackableBehaviour.Status> m_TrackingLost = new List<TrackableBehaviour.Status>()
    {
        TrackableBehaviour.Status.TRACKED,
        TrackableBehaviour.Status.NO_POSE,
    };

    private void Awake()
    {
        m_TrackableBehavior = GetComponent<TrackableBehaviour>();

        m_TrackableBehavior.RegisterTrackableEventHandler(this);
    }

    private void OnDestroy()
    {
        m_TrackableBehavior.UnregisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        // if tracking found
        foreach(TrackableBehaviour.Status trackedStatus in m_TrackingFound)
        {
            if(newStatus == trackedStatus)
            {
                if (OnTrackingFound != null)
                    OnTrackingFound();
                print("Tracking Found");
                return;
            }
        }
        // if tracking lost
        foreach (TrackableBehaviour.Status trackedStatus in m_TrackingLost)
        {
            if (newStatus == trackedStatus)
            {
                if (OnTrackingLost != null)
                    OnTrackingLost();
                print("Tracking Lost");
                return;
            }
        }
    }
}

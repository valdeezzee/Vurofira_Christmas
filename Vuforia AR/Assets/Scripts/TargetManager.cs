using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.Linq;

public class TargetManager : MonoBehaviour
{


    public string m_DatabaseName = "";


    private List<TrackableBehaviour> m_AllTargets = new List<TrackableBehaviour>();

    private void Awake()
    {
        // Once vuforia has been initialized this function gets called
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
    }

    private void OnDestroy()
    {
        VuforiaARController.Instance.UnregisterVuforiaStartedCallback(OnVuforiaStarted);
    }

    private void OnVuforiaStarted()
    {
        // Load Database
        LoadDatabase(m_DatabaseName);
        // Get trackable targets
        m_AllTargets = GetTargets();
        // Setup targets
        SetupTargets(m_AllTargets);
    }

    private void LoadDatabase(string setName)
    {

        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        objectTracker.Stop();

        // Load new dataset
        if(DataSet.Exists(setName))
        {
            DataSet dataSet = objectTracker.CreateDataSet();

            dataSet.Load(setName);
            objectTracker.ActivateDataSet(dataSet);
        }

        // enables so it can look for the targets in the dataset
        objectTracker.Start();

    }

    private List<TrackableBehaviour> GetTargets()
    {
        List<TrackableBehaviour> allTrackables = new List<TrackableBehaviour>();
        // get all game objects with the trackable behaviors on them
        allTrackables = TrackerManager.Instance.GetStateManager().GetTrackableBehaviours().ToList();

        return allTrackables;
    }

    private void SetupTargets(List<TrackableBehaviour> allTargets)
    {
        foreach(TrackableBehaviour target in allTargets)
        {
            // Parent
            target.gameObject.transform.parent = transform;
            // Rename
            target.gameObject.name = target.TrackableName;
            // Add functionality
            target.gameObject.AddComponent<PlaneCreator>();
            // Done
            Debug.Log(target.TrackableName + " Created");
        }
    }
}

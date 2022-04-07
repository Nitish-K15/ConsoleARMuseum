using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{
    public RenderTexture rt;
    public GameObject Controller;
    public Button LeftRow, RightArrow;
    // public GameObject arObjectToSpawn;
    public GameObject placementIndicator;
    private GameObject spawnedObject;
    private Pose PlacementPose;
    public ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;
    public bool gen = false;

    public GameObject[] arModels;
    int modelIndex = 0;


    private void OnEnable()
    {
        UnityEngine.Events.UnityAction action1 = () => { this.ModelChangeLeft(); };
        LeftRow.onClick.AddListener(action1);
        UnityEngine.Events.UnityAction action2 = () => { this.ModelChangeRight(); };
        RightArrow.onClick.AddListener(action2);
    }

    private void OnDisable()
    {
        LeftRow.onClick.RemoveAllListeners();
        RightArrow.onClick.RemoveAllListeners();
        spawnedObject = null;
    }

    // need to update placement indicator, placement pose and spawn 
    void Update()
    {
        if (spawnedObject == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ARPlaceObject(modelIndex);
           Controller.SetActive(true);
            if(gen)
            {
                LeftRow.gameObject.SetActive(false);
                RightArrow.gameObject.SetActive(false);
            }
            else
            {
                LeftRow.gameObject.SetActive(true);
                RightArrow.gameObject.SetActive(true);
            }
        }


        UpdatePlacementPose();
        UpdatePlacementIndicator();


    }
    void UpdatePlacementIndicator()
    {
        if (spawnedObject == null && placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void UpdatePlacementPose()
    {

            var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

            placementPoseIsValid = hits.Count > 0;
            if (placementPoseIsValid && spawnedObject == null)
            {
                PlacementPose = hits[0].pose;
            }

    }

    void ARPlaceObject(int id)
    {
        for (int i = 0; i < arModels.Length; i++)
        {
            if (i == id)
            {
               /* GameObject clearUp = GameObject.FindGameObjectWithTag("ARMultiModel");
                Destroy(clearUp);*/
                spawnedObject = Instantiate(arModels[i], PlacementPose.position, PlacementPose.rotation);
            }
        }


    }


    public void ModelChangeRight()
    {
        Destroy(spawnedObject);
        rt.Release();
        if (modelIndex < arModels.Length - 1)
            modelIndex++;
        else
            modelIndex = 0;

        ARPlaceObject(modelIndex);
    }
    public void ModelChangeLeft()
    {
        Destroy(spawnedObject);
        rt.Release();
        if (modelIndex > 0)
            modelIndex--;
        else
            modelIndex = arModels.Length - 1;

        ARPlaceObject(modelIndex);
    }


}



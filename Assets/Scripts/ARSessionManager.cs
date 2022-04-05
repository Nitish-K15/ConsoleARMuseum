using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARSessionManager : MonoBehaviour
{
    public UIManager uiManager;

    private ARSession arSession;

    private void Awake()
    {
        arSession = GetComponent<ARSession>();
        arSession.enabled = false;
    }

    public bool CheckAR()
    {
        if(ARSession.state == ARSessionState.Unsupported)
        {
            uiManager.ShowMessage("Device Not Supported");
            return false;
        }
        else//if(ARSession.state == ARSessionState.Ready || ARSession.state == ARSessionState.SessionTracking)
        {
            //arSession.enabled = true;
            return true;
        }
    }

    public void StartAR()
    {
        arSession.enabled = true;
    }
    public void StopAR()
    {
        arSession.enabled = false;
    }
}
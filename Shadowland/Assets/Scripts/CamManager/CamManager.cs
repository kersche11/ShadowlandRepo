using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    [SerializeField] Camera[] myCams;
    [SerializeField] Camera activeCam = null;



    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < myCams.Length; i++)
        {
            myCams[i].enabled = false;
        }

        activeCam = myCams[0];
        
        activeCam.enabled = true;

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToThisCam(Camera nextCam)
    {
        activeCam.enabled = false;
        activeCam = nextCam;
        activeCam.enabled = true;
    }


}

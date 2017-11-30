using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyCameraManager : MonoBehaviour
{
    #region Public Members

    public float m_waitToRotate;
    public float m_investigateTime;
    public Transform m_playerTransform;

    public enum e_CameraState
    {
        INVALID = -1,
        UNAWARE,
        INVESTIGATE,
        ALERT,

        MAX
    }

    public e_CameraState m_cameraState = e_CameraState.UNAWARE;

    #endregion

    #region Public void

    public void PullTrigger(Collider other)
    {
        if (m_cameraState == e_CameraState.UNAWARE)
        {
            m_cameraState = e_CameraState.INVESTIGATE;
        }
        //Debug.Log("Trigger");
    }

    public void ReleaseTrigger(Collider other)
    {
        if(m_cameraState == e_CameraState.INVESTIGATE)
        {
            m_investigateTimeBuffer = 0;
            m_cameraState = e_CameraState.UNAWARE;
            Debug.Log("UnTrigger");
        }
    }


    #endregion

    #region System

    void Start()
    {
        m_transform = GetComponent<Transform>();
    }
	
	void Update()
    {
        switch (m_cameraState)
        {
            case e_CameraState.UNAWARE:
                m_rotateTimeBuffer += Time.deltaTime;
                if (m_rotateTimeBuffer > m_waitToRotate)
                {
                    Rotate();
                    m_rotateTimeBuffer = 0;
                }
                break;

            case e_CameraState.INVESTIGATE:
                m_investigateTimeBuffer += Time.deltaTime;
                Debug.Log(m_investigateTimeBuffer);
                if (m_investigateTimeBuffer > m_investigateTime)
                {
                    GeneralAlert();
                    m_investigateTimeBuffer = 0;
                }
                break;

            case e_CameraState.ALERT:
                Debug.Log("Alert");
                m_transform.GetChild(0).GetComponent<Transform>().LookAt(m_playerTransform);
                break;
        }
	}

    void Rotate()
    {
        m_transform.Rotate(new Vector3(0,90,0));
    }

    void GeneralAlert()
    {
        m_cameraState = e_CameraState.ALERT;
        Debug.Log("General Alert");
    }

    #endregion

    #region Tools Debug And Utility

    #endregion

    #region Private an Protected Members

    private float m_rotateTimeBuffer = 0f;
    private float m_investigateTimeBuffer = 0f;
    private Transform m_transform;


    #endregion
}
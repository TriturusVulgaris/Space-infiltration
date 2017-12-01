using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneManager : MonoBehaviour
{
    #region Public Members

    public Transform[] m_patrolPoints;

    /*public enum e_CameraState
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
        if (m_cameraState == e_CameraState.INVESTIGATE)
        {
            m_investigateTimeBuffer = 0;
            m_cameraState = e_CameraState.UNAWARE;
            Debug.Log("UnTrigger");
        }
    }
    */
    #endregion

    #region System

    void Start()
    {
        m_agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        GoToNextPoint();
    }
	
	void Update()
    {
        if (!m_agent.pathPending && m_agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }

    void GoToNextPoint()
    {
        if (m_patrolPoints.Length == 0)
            return;
        m_agent.destination = m_patrolPoints[destPoint].position;
        destPoint = (destPoint + 1) % m_patrolPoints.Length;
    }

    #endregion

    #region Tools Debug And Utility

    #endregion

    #region Private an Protected Members

    private NavMeshAgent m_agent;
    private int destPoint = 0;

    #endregion
}
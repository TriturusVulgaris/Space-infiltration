using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneManager : MonoBehaviour
{
    #region Public Members

    public Transform[] m_patrolPoints;

    #endregion

    #region Public void

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
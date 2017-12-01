using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    #region Public Members

    public enum e_state
    {
        INVALID = -1,
        IDLE,
        MOVE,
    }

    public enum e_movePattern
    {
        INVALID = -1,
        STATIC,
        FOLLOW_PLAYER,
        RANDOM,
    }

    public enum e_vigilance
    {
        INVALID = -1,
        UNAWARE,
        INVESTIGATE,
        COMBAT,
        RESEARCH
    }

    public e_state m_state;
    public e_state m_defaultState;

    public e_movePattern m_movePattern;
    public e_movePattern m_defaultMovePattern;

    public e_vigilance m_vigilance;
    public e_vigilance m_defaultVigilance;

    public Transform m_player;

    public float m_detectionRange;
    public float m_timeInvestigate;
    public float m_timeCombat;
    public float m_timeResearch;
    public float m_timeCalmDown;

    public NavMeshAgent m_agent;

    public bool m_isDebug;

    #endregion

    #region Public void

    void EvaluateLocomotion()
    {
        switch (m_state) {
            case e_state.IDLE:
                if (m_vigilance != e_vigilance.UNAWARE)
                {
                    m_state = e_state.MOVE;
                }
                break;
            case e_state.MOVE:
                EvaluateMovePattern();
                break;
            default:
                break;
        }

    }
    void EvaluateVigilance()
    {
        switch (m_vigilance)
        {
            case e_vigilance.UNAWARE:
                if (m_timePlayerBeenVisible > m_timeInvestigate)
                {
                    m_vigilance = e_vigilance.INVESTIGATE;
                }
                break;
            case e_vigilance.INVESTIGATE:
                if (m_timePlayerBeenVisible > m_timeResearch)
                {
                    m_vigilance = e_vigilance.RESEARCH;
                }
                break;
            case e_vigilance.RESEARCH:
                if (m_timePlayerBeenVisible > m_timeCombat)
                {
                    m_vigilance = e_vigilance.COMBAT;
                }
                break;
            case e_vigilance.COMBAT:
                if (m_timePlayerBeenInvisible > m_timeCalmDown)
                {
                    m_vigilance = e_vigilance.RESEARCH;
                }
                break;

            default:
                break;
        }
    }

    void EvaluateMovePattern()
    {
        switch (m_movePattern)
        {
            case e_movePattern.STATIC:
                break;
            case e_movePattern.FOLLOW_PLAYER:
                if(IsPlayerInRange() && IsPlayerVisible())
                {
                    m_agent.SetDestination(m_lastPositionSeen);
                }
                break;
            case e_movePattern.RANDOM:
                break;

        }
    }


    void CheckPlayerVisibility()
    {
        if(IsPlayerInRange() && IsPlayerVisible())
        {
            m_timePlayerBeenVisible += Time.deltaTime;
            m_timePlayerBeenInvisible = 0f;
            m_playerHaveBeenSeen = true;
            m_lastPositionSeen = m_player.transform.position;
        }
        else
        {
            m_timePlayerBeenInvisible += Time.deltaTime;
            m_timePlayerBeenVisible = 0f;
        }
    }

    bool IsPlayerInRange()
    {
        return Vector3.Distance(m_player.position, m_transform.position) < m_detectionRange;
    }

    bool IsPlayerVisible()
    {
        RaycastHit hit;
        // Soustraire 2 position pour avoir leur direction
        if (Physics.Raycast(m_transform.position, m_player.position - m_transform.position, out hit))
        {
            return hit.collider.tag == "Player";
        }

        return false;
    }

    #endregion

    #region System

    private void Awake()
    {
        m_transform = gameObject.GetComponent<Transform>();
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        m_state = m_defaultState;
        m_vigilance = m_defaultVigilance;
	}
	
	void Update()
    {
        //MoveHead();
        CheckPlayerVisibility();
        EvaluateLocomotion();
        EvaluateVigilance();

	}

    #endregion

    #region Tools Debug And Utility

    private void OnGUI()
    {
        if (m_isDebug)
        {
            GUILayout.Button("PlayerInRange: " + IsPlayerInRange());
            GUILayout.Button("PlayerInVisible: " + (IsPlayerInRange() && IsPlayerVisible()));
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying && m_isDebug)
        {
            Gizmos.DrawWireSphere(m_transform.position, m_detectionRange);
            if(!IsPlayerVisible() && m_playerHaveBeenSeen)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(m_lastPositionSeen, Vector3.one);
            }
        }
    }

    #endregion

    #region Private an Protected Members

    private Transform m_transform;
    private float m_timePlayerBeenVisible;
    private float m_timePlayerBeenInvisible;

    private bool m_playerHaveBeenSeen;
    private Vector3 m_lastPositionSeen;

    #endregion
}
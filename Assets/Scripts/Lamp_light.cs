using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp_light : MonoBehaviour
{
    #region Public Members

    public Material m_lamp;

    #endregion

    #region Public void

    #endregion

    #region System

    void Awake()
    {
        
    }
	
	void Update()
    {
        m_timer += Time.deltaTime;
        if (m_light == 0 && m_timer > 15f)
        {
            //Debug.Log("Light off");
            m_lamp.SetColor("_EmissionColor", Color.black);
            m_timer = 0;
            m_light = 1;
        }
        else if (m_light == 1 && m_timer > .3f)
        {
            //Debug.Log("Light on");
            m_lamp.SetColor("_EmissionColor", Color.white);
            m_timer = 0;
            m_light = 2;
        }
        else if (m_light == 2 && m_timer > 0.3f)
        {
            //Debug.Log("Light off");
            m_lamp.SetColor("_EmissionColor", Color.black);
            m_timer = 0;
            m_light = 3;
        }
        else if (m_light == 3 && m_timer > 0.1f)
        {
            //Debug.Log("Light on");
            m_lamp.SetColor("_EmissionColor", Color.white);
            m_timer = 0;
            m_light = 0;
        }
    }

    #endregion

    #region Tools Debug And Utility

    #endregion

    #region Private an Protected Members

    float m_timer = 0;
    int m_light = 0;

    #endregion
}
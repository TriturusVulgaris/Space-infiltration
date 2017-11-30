using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyVisionCameraManager : MonoBehaviour
{
    #region Public Members

    public Material m_investigationMaterial;
    
    #endregion

    #region Public void

    #endregion

    #region System

    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_unawareMaterial = GetComponent<Renderer>().material;
    }
	
	void Update()
    {
		
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            m_renderer.material = m_investigationMaterial;
            gameObject.GetComponentInParent<EnnemyCameraManager>().PullTrigger(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_renderer.material = m_unawareMaterial;
            gameObject.GetComponentInParent<EnnemyCameraManager>().ReleaseTrigger(other);
        }
    }

    #endregion

    #region Tools Debug And Utility

    #endregion

    #region Private an Protected Members

    Renderer m_renderer;
    Material m_unawareMaterial;

    #endregion
}
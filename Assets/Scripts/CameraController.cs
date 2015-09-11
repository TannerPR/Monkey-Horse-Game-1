using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Target;

    [SerializeField]
    private float m_LerpSpeed = 1.0f;

    private Vector3 m_LookLocation;

	// Use this for initialization
	void Start ()
    {
        if (m_Target != null)
        {
            m_LookLocation = m_Target.transform.position;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
	    if (m_Target !=null)
        {
            UpdateLookPosition();
        }
	}

    private void UpdateLookPosition()
    {
        m_LookLocation = Vector3.Lerp(m_LookLocation, m_Target.transform.position, Time.fixedDeltaTime * m_LerpSpeed);

        transform.LookAt(m_LookLocation);
    }
}

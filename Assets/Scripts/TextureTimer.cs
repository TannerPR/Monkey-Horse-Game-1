using UnityEngine;
using System.Collections;

public class TextureTimer : MonoBehaviour
{
    [SerializeField]
    private GameObject m_GameObj;
    private Renderer m_Renderer;

	void Start ()
    {
        m_Timer = 0;
        m_RandomOffset = Vector2.zero;
        m_Renderer = GetComponent<Renderer>();
        //m_PropertyName = m_Renderer.material.GetTexture(0);
	}

	void Update ()
    {
        m_Timer++;
        if (m_Timer > 45)
        {
            m_RandomOffset = new Vector2(Random.Range(0, 20), 
                                         Random.Range(0, 20));
            m_Renderer.material.SetTextureOffset("_MainTex", m_RandomOffset);
            //m_Timer = 0;
            Debug.Log(m_RandomOffset);
            Debug.Log(m_Timer);
        }
	}

    // private variables
    float m_Timer;
    Vector2 m_RandomOffset;
    string m_PropertyName;
}

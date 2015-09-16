using UnityEngine;
using System.Collections;

public class SoundGrass : MonoBehaviour
{

    // Use this for initialization
    public AudioClip[] m_Impact = new AudioClip[5];
    private AudioSource m_AudioSource;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        int rand = Random.Range(0, m_Impact.Length);

        PlayerControl m_PlayerControl = other.transform.root.GetComponent<PlayerControl>();

        if (m_PlayerControl == null)
        {
            return;
        }

        if (other.transform.root.GetComponent<PlayerControl>())
        {
            if (m_AudioSource != null)
            {
                if (!m_AudioSource.isPlaying)
                {
                    m_AudioSource.PlayOneShot(m_Impact[rand]);
                }
            }
        }
    }
}

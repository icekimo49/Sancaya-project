using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMusic : MonoBehaviour
{
    private static AudioMusic  bgMusic;
    void mulai()
    {
        if (bgMusic == null)
        {
            bgMusic = this;
            DontDestroyOnLoad(bgMusic);
        }
        else 
        {
            Destroy(gameObject); 
        }
    }
}

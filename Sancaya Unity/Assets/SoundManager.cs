using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image unmuteIcon;
    [SerializeField] Image muteIcon;
    private bool muted = false;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
    }

    private void tombolMute()
    {
        if (muted == false)
        {
            unmuteIcon.enabled = true;
            muteIcon.enabled = false;
        }
        else
        {
            unmuteIcon.enabled = false;
            muteIcon.enabled = true;
        }
    }

    public void diMute()
    {
        if (muted == true)
        {
            muted = true;
            AudioListener.pause = true;

        }
         else
        {
            muted = false;
            AudioListener.pause = false;
        }
        Save();
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }

}

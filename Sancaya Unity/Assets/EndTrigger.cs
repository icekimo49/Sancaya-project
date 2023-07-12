
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public int wait = 5;
    public GameObject completelevlUI;
    void OnTriggerEnter2D(Collider2D collision)
    {
        completelevlUI.SetActive(true);
        Dash();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }


    private IEnumerator Dash()
    {
        yield return new WaitForSeconds(wait);
      
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkriptaZaKrajIgra : MonoBehaviour
{
    public GameObject KrajIgraUI;

    void Start()
    {

    }

    void Update()
    {

    }

    public void krajIgra()
    {
        KrajIgraUI.SetActive(true);
    }

    public void restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void meni ()
    {
        SceneManager.LoadScene("Meni");
    }

    public void quit ()
    {
        Application.Quit();
    }

    public void biraj ()
    {
        SceneManager.LoadScene("BirajLevel");
    }

}

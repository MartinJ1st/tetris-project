using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlavnoMeni : MonoBehaviour
{
    public void IzgasiIgra ()
    {
        Application.Quit();
    }

    public void BirajLevel ()
    {
        SceneManager.LoadScene("BirajLevel");
    }

    public void ZaKreatorot ()
    {
        SceneManager.LoadScene("Informacii");
    }

    public void Instrukcii()
    {
        SceneManager.LoadScene("InfoKontroli");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BiracNaLevel : MonoBehaviour
{
        public void Nivo1 ()
    {
        SceneManager.LoadScene("Tetris - Nivo 1");
    }
     
       public void Nivo2 ()
    {
        SceneManager.LoadScene("Tetris - Nivo 2");
    }

    public void Nivo3 ()
    {
        SceneManager.LoadScene("Tetris - Nivo 3");
    }

        public void Nivo4 ()
    {
        SceneManager.LoadScene("Tetris - Nivo 4");
    }

        public void Nivo5 ()
    {
        SceneManager.LoadScene("Tetris - Nivo 5");
    }
    public void ZenNivo ()
    {
        SceneManager.LoadScene("Tetris - Nivo 6");
    }

    public void VratiKonMeni()
    {
    SceneManager.LoadScene("Meni");

    }
}
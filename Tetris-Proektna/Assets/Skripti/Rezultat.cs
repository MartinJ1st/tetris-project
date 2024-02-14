using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Rezultat : MonoBehaviour
{
    public static Rezultat instance;

    public TextMeshProUGUI MomentalenRezultatTekst;
    public TextMeshProUGUI NajdobarRezultatTekst;

    int rezultat = 0;
    int najdobarRezultat = 0;

    private void Awake ()
    {
        instance = this;
    }

    void Start()
    {
        najdobarRezultat = PlayerPrefs.GetInt("Најдобар резултат ", 0);
        MomentalenRezultatTekst.text = "Моментално: \n" + rezultat.ToString() + " поени";
        NajdobarRezultatTekst.text = "Најдобар резултат: \n" + najdobarRezultat.ToString() + " поени";
    }

    // Update is called once per frame
    public void DodajPoeni()
    {
        rezultat += 100;
        MomentalenRezultatTekst.text = "Моментално: " + rezultat.ToString() + " поени";
        if(najdobarRezultat < rezultat)
            PlayerPrefs.SetInt("Најдобар резултат: ", rezultat);
    }

}

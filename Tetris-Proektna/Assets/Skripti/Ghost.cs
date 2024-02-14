using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour
{
    public Tile tile;
    public Teren glavenTeren;
    public Kvadratce kavdratceZaSledenje;

    public Tilemap tilemap { get; private set; }
    public Vector3Int[] kelii { get; private set; }
    public Vector3Int pozicija { get; private set; }

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        kelii = new Vector3Int[4];
    }

    private void LateUpdate()
    {
        Clear();
        Copy();
        Drop();
        Set();
    }
    
    private void Clear()
    {
        for (int i = 0; i < this.kelii.Length; i++)
        {
            Vector3Int tilePozicija = this.kelii[i] + this.pozicija;
            this.tilemap.SetTile(tilePozicija, null);
        }
    }

    private void Copy()
    {
        for (int i = 0; i < this.kelii.Length; i++)
        {
            this.kelii[i] = this.kavdratceZaSledenje.kelii[i];
        }
    }

    private void Drop()
    {
        Vector3Int pozicija = this.kavdratceZaSledenje.pozicija;

        int momentalenRed = pozicija.y;
        int dolenRed = -this.glavenTeren.GoleminaNaTerenot.y / 2 - 1;

        this.glavenTeren.Clear(this.kavdratceZaSledenje);

        for (int red = momentalenRed; red >= dolenRed; red--)
        {
            pozicija.y = red;

            if (this.glavenTeren.ValidnaPozicija(this.kavdratceZaSledenje, pozicija)){
                this.pozicija = pozicija;
            } else {
                break;
            }
        }
        this.glavenTeren.Set(this.kavdratceZaSledenje);
    }

    private void Set()
    {
        for (int i = 0; i < this.kelii.Length; i++)
        {
            Vector3Int tilePozicija = this.kelii[i] + this.pozicija;
            this.tilemap.SetTile(tilePozicija, this.tile);
        }
    }
}
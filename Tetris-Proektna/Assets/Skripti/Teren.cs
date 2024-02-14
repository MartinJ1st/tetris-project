using UnityEngine;
using UnityEngine.Tilemaps;

public class Teren : MonoBehaviour
{
    public Tilemap tilemap {get; private set;}
    public Kvadratce aktivnoKvadratce {get; private set; }
    public TetrominoPodatoci[] Tetromini;
    public Vector3Int PozicijaNaSozdavanje = new Vector3Int (0,8,0);
    public Vector2Int GoleminaNaTerenot = new Vector2Int(10,20);

    public SkriptaZaKrajIgra gameManager;
    public RectInt Granica
    {
        get
        {
            Vector2Int pozicija = new Vector2Int(-this.GoleminaNaTerenot.x / 2, -this.GoleminaNaTerenot.y /2);
            return new RectInt(pozicija, this.GoleminaNaTerenot);
        }
    }

    private void Awake()
    {
        Time.timeScale = 1;
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.aktivnoKvadratce = GetComponentInChildren<Kvadratce>();
        for (int i = 0; i< this.Tetromini.Length; i++)
        {
            this.Tetromini[i].Inicijaliziraj();
        }
    }

    private void Start ()
    {
        SozdajKvadratce();
    }

    public void SozdajKvadratce()
    {
        int random = Random.Range(0, this.Tetromini.Length);
        TetrominoPodatoci podatoci = this.Tetromini[random];

        this.aktivnoKvadratce.Inicijaliziraj(this, this.PozicijaNaSozdavanje, podatoci);

        if (ValidnaPozicija(aktivnoKvadratce, PozicijaNaSozdavanje)){
            Set(aktivnoKvadratce);}
        else{
            Time.timeScale = 0;
            gameManager.krajIgra();
            Kraj();
            }       
    }

    private void Kraj()
    {
        this.tilemap.ClearAllTiles();
    }

    public void Set(Kvadratce kvadratce)
    {
        for (int i = 0; i < kvadratce.kelii.Length; i++)
        {
            Vector3Int tilePozicija = kvadratce.kelii[i] + kvadratce.pozicija;
            this.tilemap.SetTile(tilePozicija, kvadratce.podatoci.tile);
        }
    }

        public void Clear(Kvadratce kvadratce)
    {
        for (int i = 0; i < kvadratce.kelii.Length; i++)
        {
            Vector3Int tilePozicija = kvadratce.kelii[i] + kvadratce.pozicija;
            this.tilemap.SetTile(tilePozicija, null);
        }
    }

    public bool ValidnaPozicija(Kvadratce kvadratce, Vector3Int pozicija)
    {
        RectInt granica = this.Granica;
        
        for (int i = 0; i < kvadratce.kelii.Length; i++)
        {
            Vector3Int tilePozicija = kvadratce.kelii[i] + pozicija;

            if(!granica.Contains((Vector2Int)tilePozicija)){
                return false;
            }

            if(this.tilemap.HasTile(tilePozicija)){
                return false;
            }
        }
        return true;
    }

    public void BrisenjeRedovi()
    {
        RectInt granica = Granica;
        int red = granica.yMin;

        while(red < granica.yMax)
        {
            if(ifCelRed(red)){
                CistenjeRed(red);
                Rezultat.instance.DodajPoeni();
            } 
            else {red++;}
        }
    }

    public bool ifCelRed(int red)
    {
        RectInt granica = Granica;
        for (int kolona = granica.xMin; kolona < granica.xMax; kolona++)
        {
            Vector3Int pozicija = new Vector3Int(kolona, red, 0);

            if(!tilemap.HasTile(pozicija)){
                return false;
            }
        }
        return true;
    }

    public void CistenjeRed(int red)
    {
        RectInt granica = Granica;
        for (int kolona = granica.xMin; kolona < granica.xMax; kolona++)
        {
            Vector3Int pozicija = new Vector3Int(kolona, red, 0);
            tilemap.SetTile(pozicija, null);
        }

        while (red < granica.yMax)
        {
            for (int kolona = granica.xMin; kolona < granica.xMax; kolona++)
            {
                Vector3Int pozicija = new Vector3Int(kolona, red + 1, 0);
                TileBase above = tilemap.GetTile(pozicija);

                pozicija = new Vector3Int(kolona, red, 0);
                tilemap.SetTile(pozicija, above);
            }
            red++;
        }
    }
}

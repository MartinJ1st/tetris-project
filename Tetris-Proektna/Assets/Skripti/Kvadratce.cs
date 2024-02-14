using UnityEngine;

public class Kvadratce : MonoBehaviour
{
    public Teren teren {get; private set; }
    public Vector3Int pozicija {get; private set; }
    public Vector3Int[] kelii {get; private set; }
    public TetrominoPodatoci podatoci {get; private set; }
    public int rotacionenIndex {get; private set; }

    public float OdlozuvanjeNaCekor = 1f;
    public float OdlozuvanjeNaZaklucuvanje = 0.5f;

    private float VremeNaCekor;
    private float VremeNaZaklucuvanje;

    public void Inicijaliziraj(Teren teren, Vector3Int pozicija, TetrominoPodatoci podatoci)
    {
        this.teren = teren;
        this.pozicija = pozicija;
        this.podatoci = podatoci;
        this.rotacionenIndex = 0;
        this.VremeNaCekor = Time.time + this.OdlozuvanjeNaCekor;
        this.VremeNaZaklucuvanje = 0f;

        if(this.kelii == null){
            this.kelii = new Vector3Int[podatoci.kelii.Length];
        }

        for (int i = 0; i < podatoci.kelii.Length; i++){
            this.kelii[i] = (Vector3Int)podatoci.kelii[i];
        }
    }

    private void Update()
    {
        this.teren.Clear(this);

        this.VremeNaZaklucuvanje += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q)){
            Rotacija(-1);}
        else if (Input.GetKeyDown(KeyCode.E)){
            Rotacija(1);}

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
            Dvizenje(Vector2Int.left);} 
        
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
            Dvizenje(Vector2Int.right);}

        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)){
            Dvizenje(Vector2Int.down);}

        if (Input.GetKeyDown(KeyCode.Space)){
            Drop();}

        if(Time.time >= this.VremeNaCekor){
            Cekor();}

        this.teren.Set(this);
    }


    private void Cekor()
    {
        this.VremeNaCekor = Time.time + this.OdlozuvanjeNaCekor;

        Dvizenje(Vector2Int.down);

        if (this.VremeNaZaklucuvanje >= this.OdlozuvanjeNaZaklucuvanje)
        {
            Zaklucuvanje();
        }
    }

    private void Zaklucuvanje()
    {
        this.teren.Set(this);
        this.teren.BrisenjeRedovi();
        this.teren.SozdajKvadratce();
    }

    private void Drop()
    {
        while(Dvizenje(Vector2Int.down)){
            continue;
        }

        Zaklucuvanje();
    }

    private bool Dvizenje(Vector2Int translacija)
    {
        Vector3Int novaPozicija = this.pozicija;
        novaPozicija.x += translacija.x;
        novaPozicija.y += translacija.y;

        bool validno = this.teren.ValidnaPozicija(this, novaPozicija);

        if(validno){
            this.pozicija = novaPozicija;
            this.VremeNaZaklucuvanje = 0f;
        }
        return validno;
    }


    private void Rotacija(int nasoka)
    {
        int originalnaRotacija = this.rotacionenIndex;
        rotacionenIndex = Wrap(rotacionenIndex + nasoka, 0, 4);

        PrimeniRotacionaMatrica(nasoka);
    
        if (!Test_SudiriSoZid(rotacionenIndex, nasoka))
        {
            rotacionenIndex = originalnaRotacija;
            PrimeniRotacionaMatrica(-nasoka);
        }
    }

    private void PrimeniRotacionaMatrica(int nasoka)
    {
        for (int i = 0; i < this.kelii.Length; i++)
        {
            Vector3 kelija = this.kelii[i];
            int x, y;

            switch (this.podatoci.tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                    kelija.x -= 0.5f;
                    kelija.y -= 0.5f;
                    x = Mathf.CeilToInt((kelija.x * Podatoci.RotacionaMatrica[0] * nasoka) + (kelija.y * Podatoci.RotacionaMatrica[1] * nasoka));
                    y = Mathf.CeilToInt((kelija.x * Podatoci.RotacionaMatrica[2] * nasoka) + (kelija.y * Podatoci.RotacionaMatrica[3] * nasoka));
                    break;

                default:
                    x = Mathf.RoundToInt((kelija.x * Podatoci.RotacionaMatrica[0] * nasoka) + (kelija.y * Podatoci.RotacionaMatrica[1] * nasoka));
                    y = Mathf.RoundToInt((kelija.x * Podatoci.RotacionaMatrica[2] * nasoka) + (kelija.y * Podatoci.RotacionaMatrica[3] * nasoka));
                break;
            }

            this.kelii[i] = new Vector3Int (x, y, 0);
        }
    }
    private bool Test_SudiriSoZid (int rotacionenIndex, int rotacionaNasoka)
    {  
        int index_SudiriSoZid = DobijIndex_SudiriSoZid(rotacionenIndex, rotacionaNasoka);

        for (int i = 0 ; i < this.podatoci.sudiriSoZid.GetLength(1); i++)
        {
            Vector2Int translacija = this.podatoci.sudiriSoZid[index_SudiriSoZid, i];

            if (Dvizenje(translacija)){
                return true;
            }
        }

        return false;
    }

    private int DobijIndex_SudiriSoZid(int rotacionenIndex, int rotacionaNasoka)
    {
        int index_SudiriSoZid = rotacionenIndex * 2;

        if(rotacionaNasoka < 0){
            index_SudiriSoZid--;
        }

        return Wrap(index_SudiriSoZid, 0, podatoci.sudiriSoZid.GetLength(0));
    }

    private int Wrap(int input, int min, int max)
    {
        if (input < min){
            return max - (min - input) % (max - min);
        }
        else{
            return min + (input - min) % (max - min);
        }
    }
}

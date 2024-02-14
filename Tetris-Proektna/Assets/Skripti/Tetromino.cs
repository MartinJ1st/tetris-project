using UnityEngine;
using UnityEngine.Tilemaps;

public enum Tetromino
{
    I,J,L,O,S,T,Z
}

[System.Serializable]
public struct TetrominoPodatoci
{
    public Tetromino tetromino;
    public Tile tile;
    public Vector2Int[] kelii {get; private set;}
    public Vector2Int[,] sudiriSoZid {get; private set; }

    public void Inicijaliziraj()
    {
        this.kelii = Podatoci.Kelii[this.tetromino];
        this.sudiriSoZid = Podatoci.SudiriSoZid[this.tetromino];
    }
}
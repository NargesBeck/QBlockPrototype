using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private List<TileHandler> Tiles;

    public void PlacePattern(string centerTileName)
    {
        TileHandler centerTileHandler = Tiles.Find(x => x.gameObject.name == centerTileName);

        centerTileHandler.FillIn();
    }
}

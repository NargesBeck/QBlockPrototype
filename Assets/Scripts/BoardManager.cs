using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private const int BOARD_SIZE = 10;
    [SerializeField] private List<TileHandler> Tiles;


    public void PlacePattern()
    {
        Debug.Log($"[BoardManager] place pattern");

        for (int i = 0; i < Tiles.Count; i++)
        {
            if (Tiles[i].CurrentState == TileHandler.TileStates.Hover)
                Tiles[i].FillIn();
        }
        GameManager.Instance.Profile.ScoreAdder(GameManager.Instance.CurrentlySelectedPattern.Score);
        CheckForFullLines();
    }

    public void PreviewPattern(string centerTileName)
    {
        MapPatternToTiles(GameManager.Instance.CurrentlySelectedPattern, centerTileName);
    }

    private void MapPatternToTiles(Pattern pattern, string centerTileName)
    {
        int centerTileIndexOnBoard = Tiles.FindIndex(x => x.gameObject.name == centerTileName);

        if (CanMapPattern(pattern, centerTileIndexOnBoard) == false) 
        {
            Debug.Log("Cannot map pattern.");
            return;
        }

        for (int i = 0; i < Tiles.Count; i++)
        {
            if (Tiles[i].CurrentState == TileHandler.TileStates.Hover)
                Tiles[i].UnHover();
        }

        int centerTileIndexOnPattern = pattern.CentralId;
        int indexDelta = centerTileIndexOnBoard - centerTileIndexOnPattern;
        List<TileHandler> tilesToHover = new List<TileHandler>();
        for (int i = 0; i < BOARD_SIZE * BOARD_SIZE; i++)
        {
            if (pattern.Value[i])
            {
                int ind = i + indexDelta;
                if (ind >= 0 && ind < Tiles.Count)
                {
                    if (Tiles[i + indexDelta].CurrentState == TileHandler.TileStates.Full)
                    {
                        Debug.Log("Cannot map pattern.");
                        return;
                    }
                    tilesToHover.Add(Tiles[i + indexDelta]);
                }
            }
        }

        for (int i = 0; i < Tiles.Count; i++)
        {
            if (Tiles[i].CurrentState == TileHandler.TileStates.Hover)
                Tiles[i].UnHover();
        }
        for (int i = 0; i < tilesToHover.Count; i++)
        {
            tilesToHover[i].Hover();
        }
    }

    private bool CanMapPattern(Pattern pattern, int centerIndexOnBoard)
    {
        int row = centerIndexOnBoard / 10;
        int col = centerIndexOnBoard % 10;

        if (col - pattern.LeftPadding < 0)
            return false;
        if (col + pattern.RightPadding > 9)
            return false;
        if (row - pattern.UpPadding < 0)
            return false;
        if (row + pattern.BottomPadding > 9)
            return false;
        return true;
    }

    private void CheckForFullLines()
    {
        List<TileHandler> tilesToEmpty = new List<TileHandler>();

        // check rows
        for (int row = 0; row < BOARD_SIZE; row++)
        {
            bool isthisRowFull = true;
            List<TileHandler> thisRow = new List<TileHandler>();
            for(int col = 0; col < BOARD_SIZE; col++)
            {
                thisRow.Add(Tiles[row * 10 + col]);
                if (Tiles[row * 10 + col].CurrentState != TileHandler.TileStates.Full)
                {
                    isthisRowFull = false;
                    break;
                }
            }
            if (isthisRowFull)
            {
                tilesToEmpty.AddRange(thisRow);
            }
        }

        // check columns
        for (int col = 0; col < BOARD_SIZE; col++)
        {
            bool isthisColumnFull = true;
            List<TileHandler> thisColumn = new List<TileHandler>();
            for (int row = 0; row < BOARD_SIZE; row++)
            {
                thisColumn.Add(Tiles[row * 10 + col]);
                if (Tiles[row * 10 + col].CurrentState != TileHandler.TileStates.Full)
                {
                    isthisColumnFull = false;
                    break;
                }
            }
            if (isthisColumnFull)
            {
                tilesToEmpty.AddRange(thisColumn);
            }
        }

        // empty
        foreach (var tile in tilesToEmpty) 
        {
            tile.Empty();
        }
        GameManager.Instance.Profile.ScoreAdder(tilesToEmpty.Count);
    }
}

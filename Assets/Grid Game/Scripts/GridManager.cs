using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //A static variable to make me easy to find
    public static GridManager Me;
    
    //How big is the map we'll build?
    public Vector2 MapSize;
    //How many enemies should we spawn?
    public int Enemies = 2;
    
    //Prefabs for spawning
    public GridPlayerController PlayerPrefab;
    public GridEnemyController EnemyPrefab;
    public TileController TilePrefab;

    //A set of coordinates for the map
    //Don't worry about the details of this too hard, it's very ugly
    public Dictionary<int, Dictionary<int, TileController>>
        Map = new Dictionary<int, Dictionary<int, TileController>>();

    //Track everything we spawn
    public List<TileController> AllTiles;
    public GridPlayerController Player;
    public List<GridEnemyController> AllEnemies;

    void Awake()
    {
        //Register myself to the static variable
        GridManager.Me = this;
    }
    
    void Start()
    {
        //Spawn every row of tiles
        for (int x = 0; x < MapSize.x; x++)
        {
            //Spawn each tile in the row
            for (int y = 0; y < MapSize.y; y++)
            {
                //Calculate where the tile should spawn
                //The one in the middle should be at 0,0
                Vector3 where = new Vector3(x - (MapSize.x / 2), y - (MapSize.y / 2), 10);
                //Spawn it
                TileController t = Instantiate(TilePrefab, where, Quaternion.identity);
                //Tell the tile that it exists and what its coordinate is
                t.Setup(x,y);
                //Add it to the list of tiles that exist in our tracker
                AllTiles.Add(t);
                //Add it to the map so we can find it later
                //Again, don't worry about this dictionary stuff too hard yet
                //This is an ugly use of it
                if(!Map.ContainsKey(x))
                    Map.Add(x,new Dictionary<int, TileController>());
                if(!Map[x].ContainsKey(y))
                    Map[x].Add(y,t);
            }
        }

        //Make a list of all the tiles that exist
        //We'll remove tiles from this list as we fill them with things
        //That means this list will be a list of all the non-filled tiles
        List<TileController> emptyTiles = new List<TileController>();
        emptyTiles.AddRange(AllTiles);

        //Choose a random tile
        TileController chosen = emptyTiles[Random.Range(0, emptyTiles.Count)];
        //Spawn the player and then put them in the chosen tile
        Player = Instantiate(PlayerPrefab);
        Player.SetTile(chosen);
        //Remove the filled tile from the list of tiles--it's no longer empty
        emptyTiles.Remove(chosen);

        //Do this code a number of times equal to our Enemies variable
        for (int n = 0; n < Enemies; n++)
        {
            //If we run out of free tiles, just end the loop
            if (emptyTiles.Count <= 0) break;
            //Choose a random tile
            chosen = emptyTiles[Random.Range(0, emptyTiles.Count)];
            //Spawn the enemy and then put them in the chosen tile
            GridEnemyController e = Instantiate(EnemyPrefab);
            e.SetTile(chosen);
            //Add the enemy to our tracker list
            AllEnemies.Add(e);
            //Remove the filled tile from the list of tiles--it's no longer empty
            emptyTiles.Remove(chosen);
        }
    }

    //This gets called by the GridPlayerScript whenever they take an action
    public void TakeTurn()
    {
        //For each enemy that exists. . .
        foreach (GridEnemyController e in AllEnemies)
        {
            //A safety check, just in case one ever gets destroyed without being removed
            //This should never happen, but safety checks like this are a good habit
            if (e == null) continue;
            //Tell the enemy to take its turn
            e.TakeTurn();
        }
    }

    //A shortcut to make it easy to get a specific tile
    //Feed it an X/Y coordinate and it'll tell you the tile there
    public TileController GetTile(int x, int y)
    {
        //If you ask for a tile that doesn't exist, return null
        if (!Map.ContainsKey(x) || !Map[x].ContainsKey(y))
            return null;
        //Return the asked for tile
        return Map[x][y];
        //The Map variable is annoying to interact with, so I just wrote a function that will have nicer grammar
    }
}

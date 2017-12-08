using UnityEngine;

public class World : MonoBehaviour
{

    public enum CellType
    {
        Wall,
        Floor,
    }

    public float ScaleFactor;
    [Range(0,100)]
    public int ShopChance;

    public int Columns = 100;                               // The number of columns on the board (how wide it will be).
    public int Rows = 100;                                  // The number of rows on the board (how tall it will be).
    public IntRange NumRooms = new IntRange(15, 20);        // The range of the number of rooms there can be.
    public IntRange RoomWidth = new IntRange(3, 10);        // The range of widths rooms can have.
    public IntRange RoomHeight = new IntRange(3, 10);       // The range of heights rooms can have.
    public IntRange CorridorLength = new IntRange(6, 10);   // The range of lengths corridors between rooms can have.
    public IntRange ShopWidht = new IntRange(6, 9);         // The range of width shops can have.
    public IntRange ShopHeight = new IntRange(5, 8);        // The range of height shops can have.
    public GameObject[] FloorTiles;                         // An array of floor tile prefabs.
    public GameObject[] WallTiles;                          // An array of wall tile prefabs.
    public GameObject[] OuterWallTiles;                     // An array of outer wall tile prefabs.
    public GameObject[] Shop;                               // Array of shop game objects.
    public GameObject Player;                               // Player game object.

    private int _shopCount;                                 // Number of shops.
    private CellType[][] _cells;                            // A jagged array of tile types representing the board, like a grid.
    private Room[] _rooms;                                  // All the rooms that are created for this board.
    private Room[] _shops;                                  // all the rooms that have one shop.
    private Corridor[] _corridors;                          // All the corridors that connect the rooms.
    private GameObject _boardHolder;                        // GameObject that acts as a container for all other tiles.

    private void Start()
    {
        _boardHolder = new GameObject("WorldHolder");

        SetupCellArray();

        CreateRoomsAndCorridors();

        SetTilesValuesForRooms();
        SetTilesValuesForCorridors();

        InstantiateTiles();
        InstantiateOuterWalls();
    }

    /// <summary>
    /// Initialize CellArrey.
    /// </summary>
    private void SetupCellArray()
    {
        _cells = new CellType[Columns][];
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i] = new CellType[Rows];
        }
    }

    /// <summary>
    /// Create rooms and corridors.
    /// </summary>
    private void CreateRoomsAndCorridors()
    {
        var nRooms = NumRooms.Random;
        _shopCount = (int) nRooms / 6;
        nRooms += _shopCount;

        _rooms = new Room[nRooms];
        _corridors = new Corridor[_rooms.Length - 1];

        _rooms[0] = new Room();
        _corridors[0] = new Corridor();

        _rooms[0].SetupRoom(RoomWidth, RoomHeight, Columns, Rows);
        _corridors[0].SetupCorridor(_rooms[0], CorridorLength, RoomWidth, RoomHeight, Columns, Rows, true);

        var rnd = new System.Random();
        var scount = 0;

        for (int i = 1; i < _rooms.Length; i++)
        {
            var temp = rnd.Next(0, 101);

            _rooms[i] = new Room();
            if (temp > ShopChance)
            {
                _rooms[i].SetupRoom(ShopWidht, ShopHeight, Columns, Rows, _corridors[i - 1]);
            }
            else
            {
                _rooms[i].SetupRoom(RoomWidth, RoomHeight, Columns, Rows, _corridors[i - 1]);
            }


            // If we haven't reached the end of the corridors array...
            if (i < _corridors.Length)
            {
                // ... create a corridor.
                _corridors[i] = new Corridor();
                // Setup the corridor based on the room that was just created.
                _corridors[i].SetupCorridor(_rooms[i], CorridorLength, RoomWidth, RoomHeight, Columns, Rows, false);
            }
            
            if (i == _rooms.Length/2)
            {
                var playerPosX = Random.Range(_rooms[i].XPos, _rooms[i].XPos + _rooms[i].RoomWidth);
                var playerPosY = Random.Range(_rooms[i].YPos, _rooms[i].YPos + _rooms[i].RoomHeight);
                Vector3 playerPos = new Vector3(playerPosX * ScaleFactor, 0, playerPosY * ScaleFactor);
                Instantiate(Player, playerPos, Quaternion.identity);
            }
            else if(temp > ShopChance && scount < _shopCount)
            {
                scount++;
                Vector3 ShopPos = new Vector3((_rooms[i].XPos+_rooms[i].RoomWidth/2f)*ScaleFactor,0,(_rooms[i].YPos+_rooms[i].RoomHeight/2f)*ScaleFactor);
                Instantiate(Shop[Random.Range(0, Shop.Length)], ShopPos, Quaternion.identity);
            }
        }

    }


    void SetTilesValuesForRooms()
    {
        // Go through all the rooms...
        for (int i = 0; i < _rooms.Length; i++)
        {
            Room currentRoom = _rooms[i];

            // ... and for each room go through it's width.
            for (int j = 0; j < currentRoom.RoomWidth; j++)
            {
                int xCoord = currentRoom.XPos + j;

                // For each horizontal tile, go up vertically through the room's height.
                for (int k = 0; k < currentRoom.RoomHeight; k++)
                {
                    int yCoord = currentRoom.YPos + k;

                    // The coordinates in the jagged array are based on the room's position and it's width and height.
                    _cells[xCoord][yCoord] = CellType.Floor;
                }
            }
        }
    }


    void SetTilesValuesForCorridors()
    {
        // Go through every corridor...
        for (int i = 0; i < _corridors.Length; i++)
        {
            Corridor currentCorridor = _corridors[i];

            // and go through it's length.
            for (int j = 0; j < currentCorridor.CorridorLength; j++)
            {
                // Start the coordinates at the start of the corridor.
                int xCoord = currentCorridor.StartXPos;
                int yCoord = currentCorridor.StartYPos;

                // Depending on the direction, add or subtract from the appropriate
                // coordinate based on how far through the length the loop is.
                switch (currentCorridor.Direction)
                {
                    case Direction.North:
                        yCoord += j;
                        break;
                    case Direction.East:
                        xCoord += j;
                        break;
                    case Direction.South:
                        yCoord -= j;
                        break;
                    case Direction.West:
                        xCoord -= j;
                        break;
                }

                // Set the tile at these coordinates to Floor.
                _cells[xCoord][yCoord] = CellType.Floor;
            }
        }
    }


    void InstantiateTiles()
    {
        // Go through all the tiles in the jagged array...
        for (int i = 0; i < _cells.Length; i++)
        {
            for (int j = 0; j < _cells[i].Length; j++)
            {
                // ... and instantiate a floor tile for it.
                InstantiateFromArray(FloorTiles, i, j);

                // If the tile type is Wall...
                if (_cells[i][j] == CellType.Wall)
                {
                    // ... instantiate a wall over the top.
                    InstantiateFromArray(WallTiles, i, j);
                }
            }
        }
    }


    void InstantiateOuterWalls()
    {
        // The outer walls are one unit left, right, up and down from the board.
        float leftEdgeX = -1f;
        float rightEdgeX = Columns + 0f;
        float bottomEdgeY = -1f;
        float topEdgeY = Rows + 0f;

        // Instantiate both vertical walls (one on each side).
        InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
        InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

        // Instantiate both horizontal walls, these are one in left and right from the outer walls.
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
    }


    void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY)
    {
        // Start the loop at the starting value for Y.
        float currentY = startingY;

        // While the value for Y is less than the end value...
        while (currentY <= endingY)
        {
            // ... instantiate an outer wall tile at the x coordinate and the current y coordinate.
            InstantiateFromArray(OuterWallTiles, xCoord, currentY);

            currentY++;
        }
    }


    void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
    {
        // Start the loop at the starting value for X.
        float currentX = startingX;

        // While the value for X is less than the end value...
        while (currentX <= endingX)
        {
            // ... instantiate an outer wall tile at the y coordinate and the current x coordinate.
            InstantiateFromArray(OuterWallTiles, currentX, yCoord);

            currentX++;
        }
    }


    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        // Create a random index for the array.
        int randomIndex = Random.Range(0, prefabs.Length);

        // The position to be instantiated at is based on the coordinates.
        Vector3 position = new Vector3(xCoord * ScaleFactor, 0f, yCoord * ScaleFactor);

        // Create an instance of the prefab from the random index of the array.
        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

        // Set the tile's parent to the board holder.
        tileInstance.transform.parent = _boardHolder.transform;
    }
}

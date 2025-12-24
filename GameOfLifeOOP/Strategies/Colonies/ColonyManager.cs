using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GameOfLifeOOP;

public class ColonyManager
{
    private readonly Terrain terrain;
    private readonly int width;
    private readonly int height;
    private readonly Random rnd = new();

    private readonly List<HashSet<(int x, int y)>> colonies = new();

    private readonly List<(int dx, int dy)> directions = new()
    {
        (1,0),(0,1),(-1,0),(0,-1),
        (1,1),(-1,1),(1,-1),(-1,-1)
    };

    private readonly Dictionary<HashSet<(int x,int y)>, (int dx,int dy)> colonyDirections
        = new();

    public ColonyManager(Terrain terrain)
    {
        this.terrain = terrain;
        width = terrain.GetCells().GetLength(0);
        height = terrain.GetCells().GetLength(1);
    }

    public void UpdateColonies()
    {
        IdentifyNewColonies();

        foreach (var colony in colonies)
        {
            var dir = colonyDirections[colony];
            MoveColony(colony, dir);
        }

        MergeColonies();
    }

    private void IdentifyNewColonies()
    {
        var cells = terrain.GetCells();
        for (int x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
        {
            var cell = cells[x, y];
            if (cell.Type == CellType.White)
            {
                int blackNeighbors = CountBlackNeighbors(x, y);
                if (blackNeighbors > 1)
                {
                    cell.Type = CellType.Black;
                    var newColony = new HashSet<(int,int)>{(x,y)};
                    colonies.Add(newColony);
                    colonyDirections[newColony] = directions[rnd.Next(directions.Count)];
                }
            }
        }
    }

    private int CountBlackNeighbors(int x, int y)
    {
        int count = 0;
        for (int dx=-1; dx<=1; dx++)
        for (int dy=-1; dy<=1; dy++)
        {
            if (dx==0 && dy==0) continue;
            int nx = x+dx, ny=y+dy;
            if (nx>=0 && ny>=0 && nx<width && ny<height)
                if (terrain.GetCells()[nx,ny].Type==CellType.Black)
                    count++;
        }
        return count;
    }

    private void MoveColony(HashSet<(int,int)> colony, (int dx,int dy) dir)
    {
        var cells = terrain.GetCells();
        var newPositions = new List<(int,int)>();
        foreach (var (x,y) in colony)
        {
            int nx = x + dir.dx;
            int ny = y + dir.dy;

            if (nx<0 || ny<0 || nx>=width || ny>=height)
            {
                colonyDirections[colony] = directions[rnd.Next(directions.Count)];
                return;
            }

            var target = cells[nx, ny];
            if (target.Type == CellType.White)
                target.Type = CellType.Black; 
            else if (target.Type == CellType.Black)
            {
                colonyDirections[colony] = directions[rnd.Next(directions.Count)];
                return;
            }

            newPositions.Add((nx, ny));
        }

        foreach (var (x,y) in colony)
            cells[x,y].Type = CellType.Empty;

        colony.Clear();
        foreach (var pos in newPositions)
        {
            cells[pos.Item1,pos.Item2].Type = CellType.Black;
            colony.Add(pos);
        }
    }

    private void MergeColonies()
    {
        for (int i = 0; i < colonies.Count; i++)
        for (int j = i+1; j < colonies.Count; j++)
        {
            if (colonies[i].Overlaps(colonies[j]))
            {
                colonies[i].UnionWith(colonies[j]);
                colonyDirections[colonies[i]] = directions[rnd.Next(directions.Count)];
                colonies.RemoveAt(j);
                j--;
            }
        }
    }
}

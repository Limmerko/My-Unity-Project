using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;

public class Level
{
    public List<InitialBrick> Bricks { get; }
    
    public String Name { get; set; } 
    
    public int CountTypes { get; } // Кол-во типов
    
    public int Complexity { get; set; } // Уровень сложности 
    
    public int Width { get; } // Ширина 

    public Level(String name, List<InitialBrick> bricks, int countTypes, int complexity)
    {
        Name = name;
        Bricks = bricks;
        CountTypes = countTypes;
        Complexity = complexity;
        Width = CalcWidth(bricks);
    }
    
    public Level(String name, List<InitialBrick> bricks, int countTypes, int complexity, int width)
    {
        Name = name;
        Bricks = bricks;
        CountTypes = countTypes;
        Complexity = complexity;
        Width = width;
    }

    private int CalcWidth(List<InitialBrick> bricks)
    {
        float maxX = bricks.Aggregate((max, next) => next.X > max.X ? next : max).X;
        float minX = bricks.Aggregate((min, next) => next.X < min.X ? next : min).X;
        float maxY = bricks.Aggregate((max, next) => next.Y > max.Y ? next : max).Y;
        float minY = bricks.Aggregate((min, next) => next.Y < min.Y ? next : min).Y;
        return (int) Math.Max((maxX - minX) + 1, (maxY - minY) + 1);
    }
}

﻿using System;



namespace Gade_POE
{
    public class Map
        {
            //CLASS VARIABLES
            public char[,] map;
            public int unitAmount;
            public int buildingAmount;
            public Unit[] units;
            public Building[] buildings;
            public int mapXSize, mapYSize;
            public Unit[] wizards;
            public int wizardAmount;

            //CLASS CONSTRUCTOR
            public Map(int _unitAmount, int _buildingAmount)
            {
                unitAmount = _unitAmount;
                buildingAmount = _buildingAmount;
            }

            //CLASS METHODS
            public void BattlefieldCreator(int _mapXSize, int _mapYSize)
            {
                //sets the map size
                mapXSize = _mapXSize;
                mapYSize = _mapYSize;
                map = new char[mapXSize, mapYSize];

                //fills battlefield with grass
                for (int i = 0; i < mapXSize; i++)
                {
                    for (int k = 0; k < mapYSize; k++)
                    {
                        map[i, k] = Convert.ToChar(".");
                    }
                }
                //randoms a wizard amount
                Random rnd = new Random();
                wizardAmount = rnd.Next(2, 6);
                units = new Unit[unitAmount];
                wizards = new Unit[wizardAmount];

                for (int i = 0; i < unitAmount; i++)
                {
                    //random values for the map
                    int x = rnd.Next(0, mapXSize);
                    int y = rnd.Next(0, mapYSize);
                    int team = rnd.Next(0, 2);
                    int unit = rnd.Next(0, 2);
                    
                    //checks that unit spawns in a spot with grass
                    if (map[x, y] != '.')
                    {
                        for (int k = 0; k < 1000; k++)
                        {
                            x = rnd.Next(0, mapXSize);
                            y = rnd.Next(0, mapYSize);

                            if (map[x, y] == '.')
                            {
                                k = 1000;
                            }
                        }
                    }

                    //spawns units
                    if (unit == 1 & team == 0)
                    {
                        Unit RangedUnit = new Unit(x, y, 10, 1, 3, 5, team, Convert.ToChar("R"), false, "RangedUnit");
                        map[x, y] = RangedUnit.symbol;
                        units[i] = RangedUnit;
                    }

                    if (unit == 1 & team == 1)
                    {
                        Unit RangedUnit = new Unit(x, y, 10, 1, 3, 5, team, Convert.ToChar("r"), false, "RangedUnit");
                        map[x, y] = RangedUnit.symbol;
                        units[i] = RangedUnit;
                    }

                    if (unit == 0 & team == 0)
                    {
                        Unit MeleeUnit = new Unit(x, y, 20, 1, 2, 1, team, Convert.ToChar("M"), false, "MeleeUnit");
                        map[x, y] = MeleeUnit.symbol;
                        units[i] = MeleeUnit;
                    }

                    if (unit == 0 & team == 1)
                    {
                        Unit MeleeUnit = new Unit(x, y, 20, 1, 2, 1, team, Convert.ToChar("m"), false, "MeleeUnit");
                        map[x, y] = MeleeUnit.symbol;
                        units[i] = MeleeUnit;

                    }
                }

                buildings = new Building[buildingAmount];
                
                //spawn buildings
                for (int j = 0; j < buildings.Length; j++)
                {
                    int x = rnd.Next(0, mapXSize);
                    int y = rnd.Next(0, mapYSize);
                    int team = rnd.Next(0, 2);
                    int buildingType = rnd.Next(0, 2);
                    

                    if (buildingType == 0)
                    {
                        ResourceBuilding B = new ResourceBuilding(x, y, 50, team, 'O');
                        map[x, y] = B.Symbol ;
                        buildings[j] = B;
                    }
                    else if (buildingType == 1)
                    {
                        FactoryBuilding B = new FactoryBuilding(x, y, 50, team, 'F');
                        map[x, y] = B.Symbol;
                        buildings[j] = B;
                    }
                    
                }

                //spawn wizards
                for (int j = 0; j < wizards.Length ; j++)
                {

                    int x = rnd.Next(0, mapXSize);
                    int y = rnd.Next(0, mapYSize);
                    int team = rnd.Next(0, 2);
                    int unit = rnd.Next(0, 2);

                    if (map[x, y] != '.')
                    {
                        for (int k = 0; k < 1000; k++)
                        {
                            x = rnd.Next(0, mapXSize);
                            y = rnd.Next(0, mapYSize);

                            if (map[x, y] == '.')
                            {
                                k = 1000;
                            }
                        }
                    }
                    //creates wizard and puts it in wizard array
                    Unit WizardUnit = new Unit(x, y, 10, 1, 2, 1, 3, 'W', false, "WizardUnit");
                    map[x, y] = WizardUnit.symbol;
                    wizards[j] = WizardUnit;
                }
            }

            public string PopulateMap(Unit[] units, Building[] buildings)
            {
                string mapLayout = "";
                //populates the map with the units
                for (int k = 0; k < units.Length; k++)
                {

                    if (units[k].Health > 0)
                    {
                        map[units[k].xPos, units[k].yPos] = units[k].symbol;
                    }
                    else
                    {
                        map[units[k].xPos, units[k].yPos] = '.';
                    }
                }
                //creates a map string
                for (int j = 0; j < mapXSize; j++)
                {
                    for (int l = 0; l < mapYSize; l++)
                    {
                        mapLayout += map[j, l];
                    }
                    mapLayout = mapLayout + "\n";
                }
                //return map string
                return mapLayout;
            }

            public void UpdatePosition(Unit u, int oldX, int oldY)
            {
                //updates a spesific unit on the map
                map[u.xPos, u.yPos] =   u.symbol;
                map[oldX, oldY] = '.';
            }


        }
    
}

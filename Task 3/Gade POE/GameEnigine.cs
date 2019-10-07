using System;



namespace Gade_POE
{
    class GameEnigine
        {
            public int roundCheck;
            public string info;
            Unit closestUnit;
            public int x, y, i;
            public Map map;
            public int temp;
            int mapXSize, mapYSize;
            public bool buildingCheck;
            Building closestBuilding;
            public string buildingInfo;



            public void GameLogic(Unit[] units, Building [] buildings, int _mapXSize, int _mapYSize)
            {
                mapXSize = _mapXSize;
                mapYSize = _mapYSize;

                info = "";
                buildingInfo = "";
                if (roundCheck > 0)
                {
                    for (i = 0; i < units.Length; i++)
                    {
                        Unit u = (Unit)units[i];

                        for (int k = 0; k < buildings.Length; k++)
                        {
                            if (u.team != buildings[k].team)
                            {
                                buildingCheck = true;
                            }
                        }

                        x = u.xPos;
                        y = u.yPos;

                        if (u.Health <= 0)
                        {
                            //u.Death(units, i);
                        }
                        else if (buildingCheck == true)
                        {
                            closestBuilding = u.ClosestBuilding(u, buildings);
                            if (u.BuildingRangeCheck(closestBuilding, u) == true)
                            {
                                u.combatCheck = true;
                                u.BuildingCombat(closestBuilding, u);
                                buildingCheck = false;
                            }
                            else
                            {
                                u.MoveToBuilding(u, closestBuilding);
                                map.UpdatePosition(u, x, y);
                                buildingCheck = false;
                                u.combatCheck = false;
                            }
                        }
                        else
                        {
                            if (u.Health > 0)
                            {
                                closestUnit = u.ClosestUnit(units, units.Length, u, buildings);

                                if (closestUnit == u)
                                {
                                    u.combatCheck = false;
                                }
                                else
                                if (u.RangeCheck(closestUnit, u) == true)
                                {
                                    u.combatCheck = true;
                                    u.CombatHandler(closestUnit, u, map.unitAmount, units);
                                }
                                else
                                {
                                    u.combatCheck = false;
                                    u.MoveUnit(u, closestUnit, mapXSize, mapYSize);
                                    map.UpdatePosition(u, x, y);

                                }

                                info += u.ToString(u, units, i);
                            }
                            else
                            {

                            }
                        }

                        info += u.ToString(u, units, i);
                    }

                    for (int k = 0; k < buildings.Length; k++)
                    {
                        Building b = buildings[k];
                        string buildingType = b.GetType().ToString();
                        string[] buildArr = buildingType.Split('.');
                        buildingType = buildArr[buildArr.Length - 1];

                        if (buildingType == "ResourceBuilding")
                        {
                            ResourceBuilding B = (ResourceBuilding)b;
                            if (B.HP > 0)
                            {
                                buildingInfo += B.ToString(buildings, B);
                                if (temp > 4)
                                {
                                    temp = 4;
                                    B.GenerateResources();
                                }
                            }
                            else
                            {
                                B.Death(B, k,buildings);
                            }
                            
                        }
                        if (buildingType == "FactoryBuilding")
                        {
                            FactoryBuilding B = (FactoryBuilding)b;
                            B.productionSpeed = 5;
                            if (B.HP > 0)
                            {
                                decimal d = roundCheck;
                                if ((d / B.productionSpeed) % 1 == 0)
                                {

                                    Array.Resize(ref units, units.Length + 1);
                                    units[units.Length - 1] = B.SpawnUnit(B);
                                }

                                buildingInfo += B.ToString(buildings, B);
                                
                            }else
                            {
                                B.Death(B, k, buildings);
                            }
                        }
                    }

                    for (int h = 0; h < map.wizards.Length; h++)
                    {
                        Unit u = (Unit)map.wizards[h];

                        x = u.xPos;
                        y = u.yPos;

                        if ((u.Health *100/ u.maxHealth) > 50)
                        {
                            closestUnit = u.ClosestUnit(units, units.Length, u, buildings);

                            if (closestUnit == u)
                            {

                            }else
                            if (u.RangeCheck(closestUnit, u) == true)
                            {
                                u.combatCheck = true;
                                u.CombatHandler(closestUnit, u, map.unitAmount, units);
                            }
                            else
                            {
                                u.combatCheck = false;
                                u.MoveUnit(u, closestUnit, mapXSize, mapYSize);
                                map.UpdatePosition(u, x, y);
                            }
                        }
                    }
                }
                else
                {
                    for (i = 0; i < units.Length; i++)
                    {
                        Unit u = (Unit)units[i];
                        info += u.ToString(u, units, i);
                    }

                    for (int k = 0; k < buildings.Length; k++)
                    {
                        Building b = buildings[k];
                        string buildingType = b.GetType().ToString();
                        string[] buildArr = buildingType.Split('.');
                        buildingType = buildArr[buildArr.Length - 1];

                        if (buildingType == "ResourceBuilding")
                        {
                            ResourceBuilding B = (ResourceBuilding)b;
                            buildingInfo += B.ToString(buildings, B);
                        }

                        if (buildingType == "FactoryBuilding")
                        {
                            FactoryBuilding B = (FactoryBuilding)b;
                            buildingInfo += B.ToString(buildings, B);
                        }
                    }
                }
            }
        }
    
}

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
                //max map x and y size
                mapXSize = _mapXSize;
                mapYSize = _mapYSize;

                info = "";
                buildingInfo = "";
                //check so its not the first round
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
                            //check for closest building
                            closestBuilding = u.ClosestBuilding(u, buildings);
                            //if in range then attack
                            if (u.BuildingRangeCheck(closestBuilding, u) == true)
                            {
                                u.combatCheck = true;
                                u.BuildingCombat(closestBuilding, u);
                                buildingCheck = false;
                            }
                            else
                            {
                                //move unit to its target
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
                                closestUnit = u.ClosestUnit(units, units.Length, u, buildings, map.wizards);

                                if (closestUnit == u)
                                {
                                    u.combatCheck = false;
                                }
                                else
                                if (u.RangeCheck(closestUnit, u, map.wizards) == true)
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
                        //explicit cast building
                        Building b = buildings[k];
                        string buildingType = b.GetType().ToString();
                        string[] buildArr = buildingType.Split('.');
                        buildingType = buildArr[buildArr.Length - 1];

                        if (buildingType == "ResourceBuilding")
                        {
                            ResourceBuilding B = (ResourceBuilding)b;
                            if (B.HP > 0)
                            {
                                //building info
                                buildingInfo += B.ToString(buildings, B);
                                if (temp > 4)
                                {
                                    temp = 4;
                                    //generates rescources
                                    B.GenerateResources();
                                }
                            }
                            else
                            {
                                //if a building is destroyed
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
                                //check that the round is divisible
                                if ((d / B.productionSpeed) % 1 == 0)
                                {
                                    //resize the array and add a unit
                                    //Array.Resize(ref units, units.Length + 1);
                                    //units[units.Length - 1] = B.SpawnUnit(B);
                                }

                                buildingInfo += B.ToString(buildings, B);
                            }
                            else
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
                        //if wizard has more than 50% health
                        if ((u.Health *100/ u.maxHealth) > 50)
                        {
                            closestUnit = u.ClosestUnit(units, units.Length, u, buildings, map.wizards);

                            if (closestUnit == u)
                            {

                            }else
                            if (u.RangeCheck(closestUnit, u, map.wizards) == true)
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
                        else
                        {
                            u.MoveUnit(u, closestUnit, mapXSize, mapYSize);
                            map.UpdatePosition(u, x, y);
                        }
                        info += u.ToString(u, units, h);
                    }
                }
                else
                {
                    for (i = 0; i < units.Length; i++)
                    {
                        //show unit info
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

                    for (int h = 0; h < map.wizards.Length; h++)
                    {
                        //show wizard info
                        Unit u = (Unit)map.wizards[h];
                        info += u.ToString(u, units, i);
                    }
                }
            }
        }
    
}

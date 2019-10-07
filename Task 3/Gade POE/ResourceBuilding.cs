namespace Gade_POE
{
    class ResourceBuilding : Building
        {
            //CLASS VARIABLES
            public string resourceType = "Wood";
            public int resourcesGenerated = 0;
            public int generatedPerRound = 2;
            public int remainingResources = 100;

            //CLASS CONSTRUCTOR
            public ResourceBuilding(int _xPos, int _yPos, int _HP, int _team , char _symbol) : base(_xPos, _yPos, _HP, _team , _symbol)
            {
            
            }

            public int XPos { get => base.xPos; set => base.xPos = value; }
            public int YPos { get => base.yPos; set => base.yPos = value; }
            public int HP { get => base.HP; set => base.HP = value; }
            public int MaxHP { get => base.maxHP; }
            public int Team { get => base.team; set => base.team = value; }
            public char Symbol { get => base.symbol; set => base.symbol = value; }

            //CLASS METHODS
            public override void Death(Building B,int k, Building[] buildings)
            {
                for (int i = k; i < buildings.Length - 1; i++)
                {
                    buildings[i] = buildings[i + 1];
                }

            }

            public override void save()
            {

            }

            public void GenerateResources()
            {
                resourceType = "WOOD";
                resourcesGenerated += generatedPerRound;
                remainingResources -= generatedPerRound;
            }

            public override string ToString(Building [] buildings, Building b)
            {
                string info = "ResourceBuidling " + "\n" + "____________" + "\n" + "HP : " + b.HP + "\n" + "Team : " + b.team + "\n" + "Symbol : " + b.symbol + "\n" + "\n";
                return info;
            }
        }
    
}

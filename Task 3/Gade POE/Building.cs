namespace Gade_POE
{
    abstract public class Building
        {
            //CLASS VARIABLES 
            public int xPos;
            public int yPos;
            public int HP;
            public int maxHP;
            public int team;
            public char symbol;


            //CLASS CONSTRUCTOR
            public Building(int _xPos, int _yPos, int _HP, int _team, char _symbol)
            {
                xPos = _xPos;
                yPos = _yPos;
                HP = _HP;
                team = _team;
                symbol = _symbol;

            }

            //CLASS METHODS
            public abstract void save();
            public abstract void Death(Building b, int k, Building[] buildings);
            public abstract string ToString(Building[] buildings, Building b);     
        }
    
}

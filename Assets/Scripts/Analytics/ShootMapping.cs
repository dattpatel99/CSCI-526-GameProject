using System.Collections.Generic;
using Newtonsoft.Json;

namespace Analytics
{
    public enum ShootType
    {
        Give,
        Take
    }

    public enum ObjectInteractionType
    {
        Give,
        Take,
        Rewind,
        AgeSelf,
        AgeEnemy,
        Missed
    }

    /**
     * The class structure for mapping the areas where gun was shoot
     */
    public class ShootMapping
    {
        public string uniqueID;
        public int x;
        public int y;
        public int z;
        public int currentTimeBank;
        public int currentAge;
        public int currentHealth;
        public ShootType shotType;
        public ObjectInteractionType objInteraction;
        public string objInteractionCode;
        public float playTime;

        public ShootMapping(int x, int y, int z, int bank, int age, int health, string shotType, string interactedType, string objCode, float rt)
        {
            this.uniqueID = $"{x.ToString()}_{y.ToString()}_{z.ToString()}";
            this.x = x;
            this.y = y;
            this.z = z;
            this.currentTimeBank = bank;
            this.currentAge = age;
            this.currentHealth = health;
            this.shotType = getShotType(shotType);
            this.objInteraction = getInteractionType(interactedType);
            this.objInteractionCode = objCode;
            this.playTime = rt;
        }
        public ShootType getShotType(string shot)
        {
            if (shot == "Give")
            {
                return ShootType.Give;
            }
            else
            {
                return ShootType.Take;
            }
        }

        public ObjectInteractionType getInteractionType(string typeInteraction)
        {
            if (typeInteraction == "Give")
            {
                return ObjectInteractionType.Give;
            }
            else if (typeInteraction == "Take")
            {
                return ObjectInteractionType.Take;
            }
            else if (typeInteraction == "AgeSelf")
            {
                return ObjectInteractionType.AgeSelf;
            }
            else if (typeInteraction == "AgeEnemy")
            {
                return ObjectInteractionType.AgeEnemy;
            }
            else if (typeInteraction == "Missed")
            {
                return ObjectInteractionType.Missed;
            }
            else
            {
                return ObjectInteractionType.Rewind;
            }
        }
        
    }
}
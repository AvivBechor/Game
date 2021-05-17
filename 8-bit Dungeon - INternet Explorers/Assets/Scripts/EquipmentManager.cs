using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts;

public class EquipmentManager : MonoBehaviour
{
    public ArmorSlot armorContainer;
    public BootsSlot bootsContainer;
    public AccessorySlot accessorContainer;
    public HandSlot handOneContainer;
    public HandSlot handTwoContainer;
    public PotionSlot consumableContainer;
    public Player player;

    public void apply(Item New, Item Old)
    {
        if (New != null)
        {
            foreach (KeyValuePair<string, int> entry in ((Equipment)New).stats)
            {
                Debug.Log("entry"+entry);
                player.character.stats[entry.Key].mod += entry.Value;
            }
        }
        if (Old != null)
        {
            foreach (KeyValuePair<string, int> entry in ((Equipment)Old).stats)
            {
                player.character.stats[entry.Key].mod -= entry.Value;
            }
        }
    }
    
}

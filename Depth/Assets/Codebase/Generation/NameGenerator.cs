using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NameGenerator
{
    /*
     * Generate group names just picks a random available name. 
     * its not actually generation but i dont think it needs to be.
     */
    public static string GenerateGroupName(Party party)
    {
        if (GroupNames.AvailableGroupNames.Count > 0)
        {
            party.partyName = GroupNames.AvailableGroupNames[Random.Range(0, GroupNames.AvailableGroupNames.Count)];
            GroupNames.AvailableGroupNames.Remove(party.partyName);
            return party.partyName;
        }
        else
        {
            Debug.LogWarning("All AIGroupNames are taken. NPC generator could not find any available name for party.");
            return null;
        }
    }
}

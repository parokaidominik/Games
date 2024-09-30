using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityType : MonoBehaviour
{
    public enum EntityTypes {emerald, asteroid, rubin, alien, bigAsteorid, satellit, rapid_asteroid}
    public EntityTypes entityType;

    public virtual void StartEntity()
    {

    }
}

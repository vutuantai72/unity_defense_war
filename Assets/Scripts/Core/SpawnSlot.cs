using DefenseWar.Core;
using DefenseWar.Models;
using DefenseWar.Utils.Dragging;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSlot : MonoBehaviour, IDragContainer<CharacterModel>
{
    public void AddItems(CharacterModel item, int number)
    {
        throw new System.NotImplementedException();
    }

    public CharacterModel GetItem()
    {
        throw new System.NotImplementedException();
    }

    public Guid GetItemId()
    {
        var item = gameObject.GetComponentInChildren<CharacterDragItem>();

        return Guid.NewGuid();
    }

    public int GetNumber()
    {
        throw new System.NotImplementedException();
    }

    public int MaxAcceptable(CharacterModel item)
    {
        throw new System.NotImplementedException();
    }

    public void RemoveItems(int number)
    {
        throw new System.NotImplementedException();
    }
}

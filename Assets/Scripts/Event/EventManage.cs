using Assets.Scripts.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManage : SingletonService<EventManage>
{
    public UnityEvent<int> onUpdateTotalStar = new UnityEvent<int>();
}

using Assets.Scripts.Services;
using DefenseWar.Core;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    GameDataService gameDataService = GameDataService.Instance;
    CharacterData charatarData;
    // Start is called before the first frame update
    void Start()
    {
        charatarData = GetComponentInParent<CharacterData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        var arrow = gameDataService.CharactersUsed.FirstOrDefault(x => x.Id == charatarData.Id).Bullet;
        var arrowObject = Instantiate(arrow, transform);
        var dir = charatarData.enemyTransform.GetChild(0).position - arrowObject.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180 + 37;
        arrowObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        var target = charatarData.enemyTransform.GetChild(0).position;
        arrowObject.transform.DOMove(target, 1f).OnComplete(() =>
        {
            Destroy(arrowObject);
        });
    }
}

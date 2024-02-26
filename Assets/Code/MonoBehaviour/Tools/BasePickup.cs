using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour
{
    private void Start() => MoveUp();

    private void MoveUp() => transform.DOMoveY(transform.position.y + 0.25f, 1).OnComplete(MoveDown);
    
    private void MoveDown() => transform.DOMoveY(transform.position.y - 0.25f, 1).OnComplete(MoveUp);
}

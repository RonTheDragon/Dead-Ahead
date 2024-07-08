using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    private GameManager _gm;
    private Transform _player;
    // Start is called before the first frame update
    new protected void Start()
    {
        base.Start();
        _gm = GameManager.Instance;
        _player = _gm.GetPlayer().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void GotStuck()
    {
        gameObject.SetActive(false);
    }

    protected override void Movements()
    {
        
    }
}

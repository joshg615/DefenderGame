using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : PlayerAbility
{
    PlayerController _playerController;
    // Start is called before the first frame update
    protected override void Initialize()
    {
        base.Initialize();
        _playerController = GetComponent<PlayerController>();
        
    }



    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("Walk", _playerController.isWalking);
    }
}

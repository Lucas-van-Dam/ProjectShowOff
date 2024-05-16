using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggeredAbility
{
    bool isActive { get; }

    void Activate();
    void Disable();

}

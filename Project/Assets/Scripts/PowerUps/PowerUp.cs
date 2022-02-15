using System.Collections;
using UnityEngine;
using System;

public sealed class PowerUp
{
    private Action startAction;
    private Action endAction;
    private float powerUpTime;

    public PowerUp(Action start, Action end, float time)
    {
        startAction = start;
        endAction = end;
        powerUpTime = time;
    }

    public IEnumerator StartPowerUp()
    {
        startAction();

        yield return new WaitForSeconds(powerUpTime);

        endAction();
    }
}

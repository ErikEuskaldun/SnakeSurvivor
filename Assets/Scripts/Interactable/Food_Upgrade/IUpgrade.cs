using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgrade
{
    public void LevelUp();
    public UpgradeScriptable NextLevelScriptable();
    public void UpdateInfo();
    public void UpgradeLoop();
}

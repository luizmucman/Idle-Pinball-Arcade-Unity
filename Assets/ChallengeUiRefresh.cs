using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeUiRefresh : MonoBehaviour
{
    [SerializeField] private ContentFitterRefresh daily;
    [SerializeField] private ContentFitterRefresh global;

    public void RefreshContentFitters() {
        daily.RefreshContentFitters();
        global.RefreshContentFitters();
    }
}

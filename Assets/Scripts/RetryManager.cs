using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryManager : MonoBehaviour
{
    private static RetryManager instance;

    private static bool retried;

    public static void Retry() {
        retried = true;
    }

    public static bool hasRetried() {
        return retried;
    }

    private void Awake() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}

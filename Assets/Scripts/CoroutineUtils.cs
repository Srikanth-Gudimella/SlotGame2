using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Custom.Utils
{
    public class CoroutineUtils : MonoBehaviour
    {
        public static CoroutineUtils instance;

        private void Awake()
        {
            if (!instance)
                instance = this;
        }

        #region BASED ON CONDN
        public Coroutine WaitUntillCondnIsMet(Func<bool> condnFunc , Action callBackOnCondnIsMet)
        {
            return StartCoroutine(WaitUntillCondnIsMetCoroutine(condnFunc, callBackOnCondnIsMet));
        }
        private IEnumerator WaitUntillCondnIsMetCoroutine(Func<bool> condnFunc, Action callBackOnCondnIsMet)
        {
            yield return new WaitUntil(() => condnFunc.Invoke());
            callBackOnCondnIsMet?.Invoke();
        }
        #endregion

        #region BASED ON TIME
        public Coroutine WaitUntillGivenTime(float waitTime,Action callBackOnTimeCompletion)
        {
            return StartCoroutine(WaitUntillGivenTimeCoroutine(waitTime,callBackOnTimeCompletion));
        }

        private IEnumerator WaitUntillGivenTimeCoroutine(float waitTime,Action callBackOnTimeCompletion)
        {
            yield return new WaitForSeconds(waitTime);
            callBackOnTimeCompletion?.Invoke();
        }
        #endregion
    }
}

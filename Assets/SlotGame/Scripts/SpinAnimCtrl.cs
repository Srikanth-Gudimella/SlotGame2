using UnityEngine;
using Spine.Unity;
using Custom.Utils;
namespace SlotGame
{
    public enum e_SpineType
    {
        Medusa,Zeus,Jackpot
    }
    public class SpinAnimCtrl : MonoBehaviour
    {
        public e_SpineType m_SpineType;
        [Space(15)]
        public SkeletonAnimation SkeltonAnim;
        // Spine.AnimationState and Spine.Skeleton are not Unity-serialized objects. You will not see them as fields in the inspector.
        public Spine.AnimationState spineAnimationState;
        public AnimationReferenceAsset actionAnim, idleAnim;
        private AnimationReferenceAsset currentState;
        
        void Start()
        {
            spineAnimationState = SkeltonAnim.AnimationState;
        }

        public void PlayCharAnim(int val)
        {
            AnimationReferenceAsset anim = val == 0 ? idleAnim : actionAnim;

            if(m_SpineType==e_SpineType.Zeus && val==1)
            {
                CoroutineUtils.instance.WaitUntillGivenTime(0.5f, () =>
                {
                    GameManager.Instance.jackPot.gameObject.SetActive(true);
                    GameManager.Instance.jackPot.PlayJackpotAnim();
                });
                CoroutineUtils.instance.WaitUntillGivenTime(2.5f, () =>
                {
                    GameManager.Instance.jackPot.gameObject.SetActive(false);
                });
            }

            if (CheckIfAlreadyPlaying(anim))
            {
                //Debug.Log("---- PlayChar Anim = " + val + " | " + anim);
                spineAnimationState = SkeltonAnim.AnimationState;
                spineAnimationState.SetAnimation(1, anim, val==0);
                CoroutineUtils.instance.WaitUntillGivenTime(1.5f, () =>
                {
                    if (m_SpineType != e_SpineType.Jackpot)
                        PlayCharAnim(0);
                });
            }
        }

        public void PlayJackpotAnim()
        {
            if (!CheckIfAlreadyPlaying(actionAnim))
            {
                spineAnimationState.SetAnimation(1, actionAnim, false);
            }
        }

        private bool CheckIfAlreadyPlaying(AnimationReferenceAsset animation)
        {
            if (currentState != animation)
            {
                currentState = animation;
                return true;
            }

            else
                return false;
        }
    }
}

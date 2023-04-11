using UnityEngine;
using DG.Tweening;

namespace Mechanics
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private LineRenderer m_lineRenderer;
        [SerializeField] private Transform m_startPoint, m_finalPoint;

        private static GameObject obj;
        private static LineRenderer lr;
        private static Transform start, final;

        private static Tweener shootTween;
        private static Sequence delayedSequence;
        
        private const float duration = 0.5f, temporalOffset = 0.15f;

        private void Awake()
        {
            obj = gameObject;
            lr = m_lineRenderer;
            start = m_startPoint;
            final = m_finalPoint;

            obj.SetActive(false);
        }

        public static void Shoot(Battler shooter, Battler target, float damage)
        {
            obj.SetActive(true);
            
            lr.positionCount = 2;

            var shooterPos = shooter.transform.position;
            var targetPos = target.transform.position;
            
            start.position = shooterPos;
            final.position = shooterPos;

            shootTween?.Kill();
            
            shootTween = final.DOMove(targetPos, duration)
                .OnUpdate(() => lr.SetPosition(1, final.position));

            delayedSequence?.Kill();
            delayedSequence = DOTween.Sequence();
            
            delayedSequence.AppendInterval(temporalOffset).Append(start.DOMove(targetPos, duration))
                .OnUpdate(() => lr.SetPosition(0, start.position))
                .OnComplete(() =>
                {
                    target.GetDamage(damage);
                    obj.SetActive(false);
                });
        }
    }
}

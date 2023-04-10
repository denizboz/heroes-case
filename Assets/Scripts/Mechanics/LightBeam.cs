using UnityEngine;
using DG.Tweening;

namespace Mechanics
{
    public class LightBeam : MonoBehaviour
    {
        [SerializeField] private LineRenderer m_lineRenderer;
        [SerializeField] private Transform m_startPoint, m_finalPoint;

        private const float duration = 0.5f, endDelay = 0.15f;

        public void Shoot(Vector3 from, Vector3 to)
        {
            gameObject.SetActive(true);
            
            m_lineRenderer.positionCount = 2;

            m_startPoint.position = from;
            m_finalPoint.position = from;

            m_finalPoint.DOMove(to, duration)
                .OnUpdate(() => m_lineRenderer.SetPosition(1, m_finalPoint.position));

            DOTween.Sequence().AppendInterval(endDelay).Append(m_startPoint.DOMove(to, duration))
                .OnUpdate(() => m_lineRenderer.SetPosition(0, m_startPoint.position))
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
        }
    }
}

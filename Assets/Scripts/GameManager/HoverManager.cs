using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverManager : MonoBehaviour
{
    [SerializeField] private float _interactRange = 3f;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Camera _cam;
    [SerializeField] private float _lastOpenTime = 0.2f;
    private OutlineController currentTarget;
    private OutlineController candidate;

    //Her frame raycast at ve bir aday hedef bul "candidate"
    //E�er bu aday daha �nce saklad���n currentTarget ile ayn� ise hi�bir �ey yapma
    //E�er farkl� ise ve e�er currentTarget bo� de�ilse onun Close metodunu �a��r
    //Sonra yeni adaya Open �a��r
    //Current target de�i�kenini bu yeni OutlineController ile g�ncelle

    private void Start()
    {
        StartCoroutine(HoverCheck());
    }

    private IEnumerator HoverCheck()
    {
        while (true)
        {
            Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);
            RaycastHit hit;

            //E�er raycast layer mask i�inden bir nesneye �arpt�ysa ve onda OutlineController varsa onu currentTarget'a ata
            if (Physics.Raycast(ray, out hit, _interactRange, _layer) && hit.collider.TryGetComponent<OutlineController>(out OutlineController outline))
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.green, 1f);
                candidate = outline;
                CompareTargets();
            }
            else
            {
                candidate = null;
                CompareTargets();
            }
            yield return new WaitForSeconds(_lastOpenTime);
        }
    }

    private void CompareTargets()
    {
        if (candidate != currentTarget)
        {
            if (currentTarget != null)
            {
                currentTarget.TryClose();
            }
            if (candidate != null)
            {
                candidate.TryOpen();
                currentTarget = candidate;
            }
        }
        currentTarget = candidate;
    }
}

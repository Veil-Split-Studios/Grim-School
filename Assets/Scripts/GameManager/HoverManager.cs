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
    //Eðer bu aday daha önce sakladýðýn currentTarget ile ayný ise hiçbir þey yapma
    //Eðer farklý ise ve eðer currentTarget boþ deðilse onun Close metodunu çaðýr
    //Sonra yeni adaya Open çaðýr
    //Current target deðiþkenini bu yeni OutlineController ile güncelle

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

            //Eðer raycast layer mask içinden bir nesneye çarptýysa ve onda OutlineController varsa onu currentTarget'a ata
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

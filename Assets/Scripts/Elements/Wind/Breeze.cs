using UnityEngine;

public class Breeze : MonoBehaviour
{
    private GameObject m_ParentChilds;
    private Wind m_Wind;

    private void OnEnable()
    {
        m_Wind = GetComponentInParent<GetWind>().GetWindObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckCollision(other);
        Debug.Log(gameObject.activeInHierarchy);
    }

    private void CheckCollision(Collider other)
    {
        
        //Precisa checar quando os ventos estão colidindo um com o outro e possuem uma semente, ou o objeto é destruido ou volta pra posição original.
        other.gameObject.TryGetComponent(out Carry carry);

        if (!carry)
        {
            return;
        }

        if (carry.transform.parent != null)
        {
            return;
        }
        
        if (m_ParentChilds == null)
        {
            m_ParentChilds = CreateParent();
        }

        m_Wind.OnWindFinished.AddListener(RemoveFromParent);
        carry.transform.SetParent(m_ParentChilds.transform);
        
    }

    private GameObject CreateParent()
    {
        m_ParentChilds = new GameObject();
        m_ParentChilds.name = "CarryChildren";
        m_ParentChilds.transform.parent = gameObject.transform;
        return m_ParentChilds;
    }

    private void RemoveFromParent()
    {
        foreach (Transform child in m_ParentChilds.transform)
        {
            child.transform.SetParent(null);
        }
    }
}

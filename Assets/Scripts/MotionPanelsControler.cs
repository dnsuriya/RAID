using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum AnimationType { APPEND, JOIN };

public class MotionPanelsControler : MonoBehaviour
{
    [SerializeField]
    List<MotionPanleAnimation> m_motionPanelAnims;

    [SerializeField]
    AnimationType m_animation;

    [SerializeField] bool m_isBiDirectional = false;

    [SerializeField]
    Button m_executer;

    public delegate bool PreCondition();
    public PreCondition m_preCondition = null;

    private uint m_state = 0;

    public List<GameObject> toBeEnabled;
    public List<GameObject> toBeSidabled;
    void OnEnable()
    {
        if (null != m_executer)
        {
            m_executer.onClick.AddListener(CheckConditions);
        }
    }

    void OnDisable()
    {
        if (null != m_executer)
        {
            m_executer.onClick.RemoveListener(CheckConditions);
        }
    }

    //Tweener anim;
    public void MoveTo(RectTransform trans, Vector2 pos, float duration, Ease ease)
    {
        trans.DOAnchorPos(pos, duration).SetEase(ease);
    }

    public void CheckConditions()
    {
        if(null == m_preCondition)
        {
            Animate();
        }
        else
        {
            if(m_preCondition() == true)
            {
                Animate();
            }
        }
    }

    public void Animate()
    {
        if(m_isBiDirectional)
        {
            if (0 == m_state)
            {
                MovetoEnd();
            }
            else
            {
                MovetoStart();
            }

            m_state = ++m_state % 2;
        }
        else
        {
            MovetoEnd();
        }
    }

    public Sequence MovetoEnd()
    {
        Sequence anim = DOTween.Sequence();

        foreach (MotionPanleAnimation panel in m_motionPanelAnims)
        {
            if (m_animation == AnimationType.APPEND)
                anim.Append(panel.panel.GetComponent<RectTransform>().DOAnchorPos(panel.endPos, panel.duration).SetEase(panel.ease));
            else if (m_animation == AnimationType.JOIN)
                anim.Join(panel.panel.GetComponent<RectTransform>().DOAnchorPos(panel.endPos, panel.duration).SetEase(panel.ease));
        }

        foreach (var item in toBeEnabled)
        {
            anim.AppendCallback(() =>
            {
                item.SetActive(true);
            });
        }
        
        foreach (var item in toBeSidabled)
        {
            anim.AppendCallback(() =>
            {
                item.SetActive(false);
            });
        }
        return anim;
    }

    public Sequence MoveTo(bool toEnd)
    {
        if(toEnd)
        {
            return MovetoEnd();
        }
        else
        {
            return MovetoStart();
        }
    }

    public Sequence MovetoStart()
    {
        Sequence anim = DOTween.Sequence();

        foreach (MotionPanleAnimation panel in m_motionPanelAnims)
        {
            if (m_animation == AnimationType.APPEND)
                anim.Append(panel.panel.GetComponent<RectTransform>().DOAnchorPos(panel.beginPos, panel.duration).SetEase(panel.ease));
            else if (m_animation == AnimationType.JOIN)
                anim.Join(panel.panel.GetComponent<RectTransform>().DOAnchorPos(panel.beginPos, panel.duration).SetEase(panel.ease));

        }
        return anim;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}


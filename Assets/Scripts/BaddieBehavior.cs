﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TamkRunner
{
    public class BaddieBehavior : FloorPart
    {
        private ObjectType _identity = ObjectType.Enemy;
        public Transform m_gcLerpPos1;
        public Transform m_gcLerpPos2;

        public float m_fLerpDuration;

        public float m_fEventTime;

        private float _lerpStartPos;
        private float _lerpEndPos;

        // Use this for initialization
        new protected void Start()
        {
            base.Start();
            m_fEventTime = Time.time;

            _lerpStartPos = -3;
            _lerpEndPos = 3;
        }

        // Update is called once per frame
        void Update()
        {
            Move();

            float fRatio = (Time.time - m_fEventTime) / m_fLerpDuration;

            //m_gcTransform.position = Vector3.Lerp(m_gcLerpPos1.position, m_gcLerpPos2.position, Easing.EaseInOut(fRatio, EasingType.Cubic, EasingType.Quadratic));
            float Progress = Mathf.Lerp(_lerpStartPos, _lerpEndPos, Easing.EaseInOut(fRatio, EasingType.Cubic, EasingType.Quadratic));

            transform.position = new Vector3(Progress, 1.3f, transform.position.z);

            if (fRatio >= 1.0f)
            {
                //Transform tTemp = m_gcLerpPos1;
                //m_gcLerpPos1 = m_gcLerpPos2;
                //m_gcLerpPos2 = tTemp;
                float Temp = _lerpStartPos;
                _lerpStartPos = _lerpEndPos;
                _lerpEndPos = Temp;
                m_fEventTime = Time.time;
            }

            DeathCheck(_identity);
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.CompareTag("Player"))
            {
                coll.GetComponent<CharacterBehavior>().KillPlayer();
                Destroy(this.gameObject);
            }
        }
    }
}
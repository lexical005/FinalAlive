using FairyGUI;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace NUIInternal
{
    public class JoystickData
    {
        public GComponent m_touchArea;
        public GButton m_btnJoystickMove;
        public GImage m_imgJoystickMove;
        public GImage m_imgJoystickCenter;
        public int m_radius = 150;
    }

    public class Joystick : EventDispatcher
    {
        float _InitX;
        float _InitY;
        float _startStageX;
        float _startStageY;
        float _lastStageX;
        float _lastStageY;

        /// <summary>
        /// 触摸id
        /// </summary>
        int touchId;

        /// <summary>
        /// 
        /// </summary>
        Tweener _tweener;

        /// <summary>
        /// 外界透传的摇杆表现与参数
        /// </summary>
        private JoystickData m_dataJoystick;

        /// <summary>
        /// 避免创建临时变量
        /// </summary>
        private Vector2 m_vector2 = Vector2.zero;

        public EventListener OnMove { get; private set; }
        public EventListener OnEnd { get; private set; }

        public Joystick(JoystickData data)
        {
            this.m_dataJoystick = data;

            touchId = -1;

            OnMove = new EventListener(this, "onMove");
            OnEnd = new EventListener(this, "onEnd");

            _InitX = m_dataJoystick.m_imgJoystickCenter.x + m_dataJoystick.m_imgJoystickCenter.width / 2;
            _InitY = m_dataJoystick.m_imgJoystickCenter.y + m_dataJoystick.m_imgJoystickCenter.height / 2;

            m_dataJoystick.m_btnJoystickMove.changeStateOnClick = false;

            m_dataJoystick.m_touchArea.onTouchBegin.Add(this.OnTouchDown);
        }

        /// <summary>
        /// 触摸按下
        /// </summary>
        /// <param name="context"></param>
        private void OnTouchDown(EventContext context)
        {
            if (touchId == -1)//First touch
            {
                InputEvent evt = (InputEvent)context.data;
                touchId = evt.touchId;

                if (_tweener != null)
                {
                    _tweener.Kill();
                    _tweener = null;
                }

                m_vector2.x = evt.x;
                m_vector2.y = evt.y;
                Vector2 pt = m_dataJoystick.m_touchArea.GlobalToLocal(m_vector2);
                float bx = pt.x;
                float by = pt.y;
                m_dataJoystick.m_btnJoystickMove.selected = true;

                if (bx < 0)
                {
                    bx = 0;
                }
                else if (bx > m_dataJoystick.m_touchArea.width)
                {
                    bx = m_dataJoystick.m_touchArea.width;
                }

                if (by > GRoot.inst.height)
                {
                    by = GRoot.inst.height;
                }
                else if (by < m_dataJoystick.m_touchArea.y)
                {
                    by = m_dataJoystick.m_touchArea.y;
                }

                _lastStageX = bx;
                _lastStageY = by;
                _startStageX = bx;
                _startStageY = by;

                m_dataJoystick.m_imgJoystickCenter.visible = true;
                m_dataJoystick.m_imgJoystickCenter.x = bx - m_dataJoystick.m_imgJoystickCenter.width / 2;
                m_dataJoystick.m_imgJoystickCenter.y = by - m_dataJoystick.m_imgJoystickCenter.height / 2;
                m_dataJoystick.m_btnJoystickMove.x = bx - m_dataJoystick.m_btnJoystickMove.width / 2;
                m_dataJoystick.m_btnJoystickMove.y = by - m_dataJoystick.m_btnJoystickMove.height / 2;

                float deltaX = bx - _InitX;
                float deltaY = by - _InitY;
                float degrees = Mathf.Atan2(deltaY, deltaX) * 180 / Mathf.PI;
                m_dataJoystick.m_imgJoystickMove.rotation = degrees + 90;

                Stage.inst.onTouchMove.Add(this.OnTouchMove);
                Stage.inst.onTouchEnd.Add(this.OnTouchUp);
            }
        }

        /// <summary>
        /// 触摸结束
        /// </summary>
        /// <param name="context"></param>
        private void OnTouchUp(EventContext context)
        {
            InputEvent inputEvt = (InputEvent)context.data;
            if (touchId != -1 && inputEvt.touchId == touchId)
            {
                touchId = -1;
                m_dataJoystick.m_imgJoystickMove.rotation = m_dataJoystick.m_imgJoystickMove.rotation + 180;
                m_dataJoystick.m_imgJoystickCenter.visible = false;

                m_vector2.x = _InitX - m_dataJoystick.m_btnJoystickMove.width / 2;
                m_vector2.y = _InitY - m_dataJoystick.m_btnJoystickMove.height / 2;
                _tweener = m_dataJoystick.m_btnJoystickMove.TweenMove(m_vector2, 0.3f).OnComplete(() =>
                {
                    _tweener = null;
                    m_dataJoystick.m_btnJoystickMove.selected = false;
                    m_dataJoystick.m_imgJoystickMove.rotation = 0;
                    m_dataJoystick.m_imgJoystickCenter.visible = true;
                    m_dataJoystick.m_imgJoystickCenter.x = _InitX - m_dataJoystick.m_imgJoystickCenter.width / 2;
                    m_dataJoystick.m_imgJoystickCenter.y = _InitY - m_dataJoystick.m_imgJoystickCenter.height / 2;
                }
                );

                Stage.inst.onTouchMove.Remove(this.OnTouchMove);
                Stage.inst.onTouchEnd.Remove(this.OnTouchUp);

                this.OnEnd.Call();
            }
        }

        /// <summary>
        /// 触摸移动
        /// </summary>
        /// <param name="context"></param>
        private void OnTouchMove(EventContext context)
        {
            InputEvent evt = (InputEvent)context.data;
            if (touchId != -1 && evt.touchId == touchId)
            {
                m_vector2.x = evt.x;
                m_vector2.y = evt.y;
                Vector2 pt = m_dataJoystick.m_touchArea.GlobalToLocal(m_vector2);
                float bx = pt.x;
                float by = pt.y;
                float moveX = bx - _lastStageX;
                float moveY = by - _lastStageY;
                _lastStageX = bx;
                _lastStageY = by;
                float buttonX = m_dataJoystick.m_btnJoystickMove.x + moveX;
                float buttonY = m_dataJoystick.m_btnJoystickMove.y + moveY;

                float offsetX = buttonX + m_dataJoystick.m_btnJoystickMove.width / 2 - _startStageX;
                float offsetY = buttonY + m_dataJoystick.m_btnJoystickMove.height / 2 - _startStageY;

                float rad = Mathf.Atan2(offsetY, offsetX);
                float degree = rad * 180 / Mathf.PI;
                m_dataJoystick.m_imgJoystickMove.rotation = degree + 90;

                float maxX = m_dataJoystick.m_radius * Mathf.Cos(rad);
                float maxY = m_dataJoystick.m_radius * Mathf.Sin(rad);
                if (Mathf.Abs(offsetX) > Mathf.Abs(maxX))
                {
                    offsetX = maxX;
                }
                if (Mathf.Abs(offsetY) > Mathf.Abs(maxY))
                {
                    offsetY = maxY;
                }

                buttonX = _startStageX + offsetX;
                buttonY = _startStageY + offsetY;
                if (buttonX < 0)
                {
                    buttonX = 0;
                }
                if (buttonY > GRoot.inst.height)
                {
                    buttonY = GRoot.inst.height;
                }

                m_dataJoystick.m_btnJoystickMove.x = buttonX - m_dataJoystick.m_btnJoystickMove.width / 2;
                m_dataJoystick.m_btnJoystickMove.y = buttonY - m_dataJoystick.m_btnJoystickMove.height / 2;

                this.OnMove.Call(degree);
            }
        }
    }
}

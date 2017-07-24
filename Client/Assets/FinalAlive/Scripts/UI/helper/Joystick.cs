using FairyGUI;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace NUIInternal
{
    public class JoystickData
    {
        public enum EJoystickMode
        {
            /// <summary>
            /// 方向系数 = (当前触摸位置 - 初始触摸位置) / 摇杆半径
            /// </summary>
            RelativeCenter,

            /// <summary>
            /// 方向系数 = 触摸位移 / 摇杆半径
            /// </summary>
            MoveDelta,
        }

        public GComponent m_touchArea;
        public GButton m_btnJoystickMove;
        public GImage m_imgJoystickMove;
        public GImage m_imgJoystickCenter;
        public int m_radius = 150;
        public EJoystickMode m_mode = EJoystickMode.RelativeCenter;
    }

    public class Joystick : EventDispatcher
    {
        float _InitX;
        float _InitY;
        float _startStageX;
        float _startStageY;
        float _lastStageX;
        float _lastStageY;
        float _lastOffsetCenterX;
        float _lastOffsetCenterY;

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

        /// <summary>
        /// 避免创建临时变量
        /// </summary>
        private Vector3 m_vector3 = Vector3.zero;

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
                _lastOffsetCenterX = 0;
                _lastOffsetCenterY = 0;

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

                float offsetCenterX = buttonX + m_dataJoystick.m_btnJoystickMove.width / 2 - _startStageX;
                float offsetCenterY = buttonY + m_dataJoystick.m_btnJoystickMove.height / 2 - _startStageY;

                float rad = Mathf.Atan2(offsetCenterY, offsetCenterX);
                float degree = rad * 180 / Mathf.PI;
                m_dataJoystick.m_imgJoystickMove.rotation = degree + 90;

                float maxX = m_dataJoystick.m_radius * Mathf.Cos(rad);
                float maxY = m_dataJoystick.m_radius * Mathf.Sin(rad);
                if (Mathf.Abs(offsetCenterX) > Mathf.Abs(maxX))
                {
                    offsetCenterX = maxX;
                }
                if (Mathf.Abs(offsetCenterY) > Mathf.Abs(maxY))
                {
                    offsetCenterY = maxY;
                }

                buttonX = _startStageX + offsetCenterX;
                buttonY = _startStageY + offsetCenterY;
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

                if (m_dataJoystick.m_mode == JoystickData.EJoystickMode.RelativeCenter)
                {
                    m_vector3.x = offsetCenterX / m_dataJoystick.m_radius;
                    m_vector3.z = -offsetCenterY / m_dataJoystick.m_radius;

                    this.OnMove.Call(m_vector3);
                }
                else if (m_dataJoystick.m_mode == JoystickData.EJoystickMode.MoveDelta)
                {
                    m_vector3.x = (offsetCenterX - _lastOffsetCenterX) / m_dataJoystick.m_radius;
                    m_vector3.z = -(offsetCenterY - _lastOffsetCenterY) / m_dataJoystick.m_radius;

                    this.OnMove.Call(m_vector3);
                }

                _lastOffsetCenterX = offsetCenterX;
                _lastOffsetCenterY = offsetCenterY;
            }
        }
    }
}

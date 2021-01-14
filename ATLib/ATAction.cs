using System;
using System.Threading;
using System.Windows;
using System.Windows.Automation;
using static ATLib.Input.HWSimulator.HWSend;

namespace ATLib
{
    public class ATAction : ATElement
    {
        protected AutomationElement AutomationElement { get; set; }

        protected ATAction()
        {
        }
        protected ATAction(AutomationElement elePara)
        {
            AutomationElement = elePara;
        }

        public void DoByMode(DoMode doMode, double waitTime = 0.1)
        {
            try
            {
                switch (doMode)
                {
                    case DoMode.Invoke:
                        DoClick(waitTime);
                        break;
                    case DoMode.Select:
                        DoSelect(waitTime);
                        break;
                    case DoMode.Point:
                        DoClickPoint(0, 0, waitTime);
                        break;
                    default:
                        throw new Exception("DoMode " + doMode + " does not exist.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Scrollwhat(double waitTime = 0.2)
        {
            try
            {
                var t = AutomationElement.GetCurrentPattern(ScrollItemPattern.Pattern) as ScrollItemPattern;
                t.ScrollIntoView();
                Thread.Sleep((int)(waitTime * 1000));
            }
            catch (Exception ex)
            {
                throw new Exception("DoExpand error. " + ex);
            }
        }
        public void DoExpand(double waitTime = 0.2)
        {
            try
            {
                var t = AutomationElement.GetCurrentPattern(ExpandCollapsePattern.Pattern) as ExpandCollapsePattern;
                t.Expand();
                Thread.Sleep((int)(waitTime * 1000));
            }
            catch (Exception ex)
            {
                throw new Exception("DoExpand error. " + ex);
            }
        }
        //For Document, Slider .....
        public string DoGetValue(double waitTime = 0.1)
        {
            try
            {
                var t = (ValuePattern)AutomationElement.GetCurrentPattern(ValuePattern.Pattern);
                Thread.Sleep((int)(waitTime * 1000));
                return t.Current.Value;
            }
            catch (Exception ex)
            {
                throw new Exception("DoSelect error. " + ex);
            }
        }
        public void DoSelect(double waitTime = 0.1)
        {
            try
            {
                var t = (SelectionItemPattern)AutomationElement.GetCurrentPattern(SelectionItemPattern.Pattern);
                t.Select();
                Thread.Sleep((int)(waitTime * 1000));
            }
            catch (Exception ex)
            {
                throw new Exception("DoSelect error. " + ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="waitTime"></param>
        public void DoClickPoint(double waitTime = 0.1, double x = 0, double y = 0, MouseKeys mk = MouseKeys.LEFT)
        {
            var ptClick = new System.Windows.Point();
            if (x == 0 && y == 0)
            {
                try
                {
                    ptClick = AutomationElement.GetClickablePoint();
                }
                catch
                {
                    try
                    {
                        ptClick = new Point((AutomationElement.Current.BoundingRectangle.Left + 2), (AutomationElement.Current.BoundingRectangle.Top + 2));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to GetClickablePoint. " + ex);
                    }
                }
            }
            else
            {
                try
                {
                    ptClick = new Point((AutomationElement.Current.BoundingRectangle.Left), (AutomationElement.Current.BoundingRectangle.Top));
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to GetBoundingRectangle. " + ex);
                }
            }
            try
            {
                MoveCursorAndDo((int)ptClick.X + (int)x, (int)ptClick.Y + (int)y, mk);
            }
            catch (Exception ex)
            {
                throw new Exception("Click point error. " + ex);
            }
            Thread.Sleep((int)(waitTime * 1000));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ScrollEvents DoScrollEvents()
        {
            try
            {
                return new ScrollEvents(new AT(AutomationElement));
            }
            catch (Exception ex)
            {
                throw new Exception("DoScroll error. " + ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public class ScrollEvents
        {
            /// <summary>
            /// 
            /// </summary>
            private AT _elePara;
            /// <summary>
            /// 
            /// </summary>
            private readonly ScrollPattern _scrollPattern = null;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="atObj"></param>
            public ScrollEvents(AT atObj)
            {
                _elePara = atObj;
                try
                {
                    _scrollPattern = (ScrollPattern)atObj.AutomationElement.GetCurrentPattern(ScrollPattern.Pattern);
                }
                catch (Exception)
                {
                    //throw new Exception("Failed to get scrollable item.");
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="waitTime"></param>
            public void ScrollVerticalTop(double waitTime = 0.1)
            {
                try
                {
                    _scrollPattern?.ScrollVertical(ScrollAmount.LargeDecrement);
                    Thread.Sleep((int)(waitTime * 1000));
                }
                catch (Exception ex)
                {
                    throw new Exception("ScrollVerticalTop error. " + ex);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="waitTime"></param>
        public void DoToggle(double waitTime = 0.1)
        {
            //try { DoSetFocus(ele); }catch (Exception) { }
            try
            {
                var t = (TogglePattern)AutomationElement.GetCurrentPattern(TogglePattern.Pattern);
                t.Toggle();
            }
            catch (Exception ex)
            {
                throw new Exception("DoToggle error. " + ex);
            }
            Thread.Sleep((int)(waitTime * 1000));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="waitTime"></param>
        public void DoClick(double waitTime = 0.1)
        {
            //try { DoSetFocus(ele); }catch (Exception) { }
            try
            {
                var t = (InvokePattern)AutomationElement.GetCurrentPattern(InvokePattern.Pattern);
                t.Invoke();
            }
            catch (Exception ex)
            {
                throw new Exception("DoClick error. " + ex);
            }
            Thread.Sleep((int)(waitTime * 1000));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="waitTime"></param>
        public void DoClickWithNewThread(double waitTime = 0.1)
        {
            try
            {
                var t = (InvokePattern)AutomationElement.GetCurrentPattern(InvokePattern.Pattern);
                var invokeThread = new Thread(t.Invoke);
                invokeThread.Start();
                invokeThread.Join();
                invokeThread.Abort();
            }
            catch (Exception ex)
            {
                throw new Exception("DoClickWithNewThread error. " + ex);
            }
            Thread.Sleep((int)(waitTime * 1000));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="waitTime"></param>
        public void DoSetFocus(double waitTime = 0.1)
        {
            try
            {
                AutomationElement.SetFocus();
                Thread.Sleep((int)(waitTime * 1000));
            }
            catch (Exception ex)
            {
                throw new Exception("DoSetFocus error. " + ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IAccessible GetIAccessible()
        {
            try
            {
                return new IAccessible(new AT(AutomationElement));
            }
            catch (Exception ex)
            {
                throw new Exception("GetIAccessible error." + ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public class IAccessible
        {
            /// <summary>
            /// 
            /// </summary>
            private readonly AT _elePara;
            /// <summary>
            /// 
            /// </summary>
            private LegacyIAccessiblePattern.LegacyIAccessiblePatternInformation _legacyIAccessiblePatternInformation;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="elePara"></param>
            public IAccessible(AT elePara)
            {
                this._elePara = elePara;
                try
                {
                    var t = (LegacyIAccessiblePattern)elePara.AutomationElement.GetCurrentPattern(LegacyIAccessiblePattern.Pattern);
                    _legacyIAccessiblePatternInformation = t.Current;
                }
                catch (Exception)
                {
                    throw new Exception("Failed to get IAccessible.");
                }
            }
            public string Description(double waitTime = 0.1)
            {
                try
                {
                    Thread.Sleep((int)(waitTime * 1000));
                    return _legacyIAccessiblePatternInformation.Description;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to get Description. " + ex);
                }
            }
            public string Name(double waitTime = 0.1)
            {
                try
                {
                    Thread.Sleep((int)(waitTime * 1000));
                    return _legacyIAccessiblePatternInformation.Name;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to get Description. " + ex);
                }
            }
            public void DoDefaultAction(double waitTime = 0.1)
            {
                try
                {

                    var legacyIAccessiblePattern = (LegacyIAccessiblePattern)_elePara.AutomationElement.GetCurrentPattern(LegacyIAccessiblePattern.Pattern);
                    legacyIAccessiblePattern.DoDefaultAction();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to DoDefaultAction. " + ex);
                }
                Thread.Sleep((int)(waitTime * 1000));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public WindowEvents DoWindowEvents()
        {
            try
            {
                return new WindowEvents(new AT(AutomationElement));
            }
            catch (Exception ex)
            {
                throw new Exception("DoWindowEvents error." + ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public class WindowEvents
        {
            /// <summary>
            /// 
            /// </summary>
            AT elePara;
            /// <summary>
            /// 
            /// </summary>
            WindowPattern WindowPattern;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="elePara"></param>
            public WindowEvents(AT elePara)
            {
                this.elePara = elePara;
                try
                {
                    WindowPattern = (WindowPattern)elePara.AutomationElement.GetCurrentPattern(WindowPattern.Pattern);
                }
                catch (Exception)
                {
                    throw new Exception("Failed to get WindowEvents.");
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="waitTime"></param>
            public void Normal(double waitTime = 0.1)
            {
                try
                {
                    WindowPattern.SetWindowVisualState(WindowVisualState.Normal);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to set WindowVisualState to Normal. " + ex);
                }
                Thread.Sleep((int)(waitTime * 1000));
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="waitTime"></param>
            public void Maximized(double waitTime = 0.1)
            {
                try
                {
                    WindowPattern.SetWindowVisualState(WindowVisualState.Maximized);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to set WindowVisualState to Maximized. " + ex);
                }
                Thread.Sleep((int)(waitTime * 1000));
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="waitTime"></param>
            public void Minimized(double waitTime = 0.1)
            {
                try
                {
                    WindowPattern.SetWindowVisualState(WindowVisualState.Minimized);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to set WindowVisualState to Minimized. " + ex);
                }
                Thread.Sleep((int)(waitTime * 1000));
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="waitTime"></param>
            public void Close(double waitTime = 0.1)
            {
                try
                {
                    WindowPattern.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to close the window. " + ex);
                }
                Thread.Sleep((int)(waitTime * 1000));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="waitTime"></param>
        public void DoSetValue(string strValue, double waitTime = 0.1)
        {
            try { DoSetFocus(); }
            catch (Exception) { }
            try
            {
                ValuePattern tbTestBox = AutomationElement.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                tbTestBox.SetValue("");
                Thread.Sleep((int)(0.3 * 1000));
                //PublicClass.Sendkeys(strValue);
                Thread.Sleep((int)(waitTime * 1000));
            }
            catch (Exception ex)
            {
                throw new Exception("DoSetValue error. " + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public class CurrentElement
        {
            AutomationElement.AutomationElementInformation current;
            /// <summary>
            /// 
            /// </summary>
            AT elePara;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="elePara"></param>
            public CurrentElement(AT elePara)
            {
                this.elePara = elePara;
                current = elePara.AutomationElement.Current;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ProcessId()
            {
                try
                {
                    return current.ProcessId.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get ProcessId. {ex}");
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public bool Exists()
            {
                try
                {
                    var t = ProcessId();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public bool IsEnabled()
            {
                try
                {
                    return current.IsEnabled;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get IsEnabled status. {ex}");
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public bool IsOffscreen()
            {
                try
                {
                    return current.IsOffscreen;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get IsOffscreen status. {ex}");
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public bool IsSelected()
            {
                try
                {
                    var t = (SelectionItemPattern)elePara.AutomationElement.GetCurrentPattern(SelectionItemPattern.Pattern);
                    return t.Current.IsSelected;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get IsSelected status. {ex}");
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string GetSelectionName()
            {
                try
                {
                    var t = (SelectionPattern)elePara.AutomationElement.GetCurrentPattern(SelectionPattern.Pattern);
                    return t.Current.GetSelection()[0].Current.Name;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to GetSelection. {ex}");
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string Value()
            {
                try
                {
                    var t = (ValuePattern)elePara.AutomationElement.GetCurrentPattern(ValuePattern.Pattern);
                    return t.Current.Value;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get Value. {ex}");
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string IsChecked()
            {
                try
                {
                    var t = (TogglePattern)elePara.AutomationElement.GetCurrentPattern(TogglePattern.Pattern);
                    return t.Cached.ToggleState.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get isSelected status. {ex}");
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string FullDescription()
            {
                try
                {
                    return current.FullDescription;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get IsEnabled status. {ex}");
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public IntPtr GetHwnd()
            {
                try
                {
                    return new IntPtr(elePara.AutomationElement.Current.NativeWindowHandle);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get isSelected status. {ex}");
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ToggleState()
            {
                try
                {
                    var t = (TogglePattern)elePara.AutomationElement.GetCurrentPattern(TogglePattern.Pattern);
                    return t.Current.ToggleState.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get isSelected status. {ex}");
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string Name()
            {
                try
                {
                    return current.Name;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get Name. {ex}");
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ClassName()
            {
                try
                {
                    return current.ClassName;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get ClassName. {ex}");
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string AutomationId()
            {
                try
                {
                    return current.AutomationId;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get AutomationId. {ex}");
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string ControlType()
            {
                try
                {
                    return current.ControlType.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get ControlType. {ex}");
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string FrameworkId()
            {
                try
                {
                    return current.FrameworkId;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get FrameworkId. {ex}");
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public Rect BoundingRectangle()
            {
                try
                {
                    return current.BoundingRectangle;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get BoundingRectangle. {ex}");
                }
            }
            public double RectangleLeft()
            {
                try
                {
                    return current.BoundingRectangle.Left;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get RectangleLeft. {ex}");
                }
            }
            public double RectangleTop()
            {
                try
                {
                    return current.BoundingRectangle.Top;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get RectangleTop. {ex}");
                }
            }
            public double RectangleRight()
            {
                try
                {
                    return current.BoundingRectangle.Right;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get RectangleRight. {ex}");
                }
            }
            public double RectangleBottom()
            {
                try
                {
                    return current.BoundingRectangle.Bottom;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get RectangleBottom. {ex}");
                }
            }
        }
    }
}

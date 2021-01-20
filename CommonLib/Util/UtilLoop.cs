using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Util
{
    public class UtilLoop
    {
        public static void testC()
        {
            var listTop = new List<string> {
                "a", "b", "c"
            };
            var listSecond = new List<List<string>> {
                new List<string> {
                "1"
                },
                new List<string> {
                "4"
                },
            };
            ControlLoop(listTop, listSecond, (x, y, z) =>
            {
                Console.WriteLine($"{x}   {z}");
            }, (x) =>
            {
                Console.WriteLine($"{x}   A");
            }, false);
        }
        public static void ControlLoop<T1, T2>(IEnumerable<T1> listTop, IEnumerable<IEnumerable<T2>> listSecond, Action<T1, IEnumerable<T2>, T2> listSecondSubItemsAction, Action<T1> whenTopItemsMoreThanSecondItems = null, bool blReturnWhenLoopedAllSecondItems = false)
        {
            bool _blBreak = false;
            for (var i = 0; i < listTop.Count(); i++)
            {
                if (!listSecond.Any()) // All items in listSecond are less than the items in listTop
                {
                    if (whenTopItemsMoreThanSecondItems != null)
                    {
                        whenTopItemsMoreThanSecondItems.Invoke(listTop.ElementAt(i));
                        continue;
                    }
                    else
                    {
                        return;
                    }
                }
                if (_blBreak)// add this here for when the count of listSecond is 1
                {
                    _blBreak = false;
                }
                foreach (var listSecondSubItems in listSecond) // since some one removed
                {
                    if (_blBreak)
                    {
                        _blBreak = false;
                        break;
                    }
                    foreach (var subItem in listSecondSubItems)
                    {
                        listSecondSubItemsAction.Invoke(listTop.ElementAt(i), listSecondSubItems, subItem);
                        listSecondSubItems.Remove(subItem);
                        if (!listSecondSubItems.Any())
                        {
                            listSecond = listSecond.Where(x => x.Count() != 0);
                        }
                        if (i == (listTop.Count() - 1))
                        {
                            if (listSecond.Any() || listSecondSubItems.Any())
                            {
                                i = -1; // All items in listSecond are more than keys, to reassign from Esc
                                if (blReturnWhenLoopedAllSecondItems)
                                {
                                    return;
                                }
                            }
                        }
                        _blBreak = true;
                        break;
                    }
                }
            }
        }
        //public static void ControlLoop<T1, T2>(IEnumerable<T1> listTop, IEnumerable<IEnumerable<T2>> listSecond, Action<IEnumerable<T2>, T2> listSecondSubItemsAction, Action whenTopItemsMoreThanSecondItems = null, Action whenLoopingAllSecondItems = null)
        //{
        //    bool _blBreak = false;
        //    IEnumerable<T2> recordListThird = null;
        //    for (var i = 0; i < listTop.Count(); i++)
        //    {
        //        if (recordListThird != null)
        //        {
        //            listSecond.Remove(recordListThird); //remove null list  
        //            recordListThird = null;
        //        }
        //        if (!listSecond.Any()) // All items in listSecond are less than the items in listTop
        //        {
        //            if (whenTopItemsMoreThanSecondItems != null)
        //            {
        //                whenTopItemsMoreThanSecondItems.Invoke();
        //                continue;
        //            }
        //            else
        //            {
        //                return;
        //            }
        //        }
        //        if (_blBreak)// add this here for when the count of listSecond is 1
        //        {
        //            _blBreak = false;
        //        }
        //        foreach (var listSecondSubItems in listSecond)
        //        {
        //            if (_blBreak)
        //            {
        //                _blBreak = false;
        //                break;
        //            }
        //            foreach (var subItem in listSecondSubItems)
        //            {
        //                listSecondSubItemsAction.Invoke(listSecondSubItems, subItem);
        //                listSecondSubItems.Remove(subItem);
        //                if (!listSecondSubItems.Any())
        //                {
        //                    recordListThird = listSecondSubItems; //record null listSecondSubItems ; //remove null list  ]
        //                }
        //                if (i == (listTop.Count() - 1))
        //                {
        //                    if (listSecond.Any() || listSecondSubItems.Any())
        //                    {
        //                        i = -1; // All items in listSecond are more than keys, to reassign from Esc
        //                        if (whenLoopingAllSecondItems != null)
        //                        {
        //                            whenLoopingAllSecondItems.Invoke();
        //                        }
        //                    }
        //                }
        //                _blBreak = true;
        //                break;
        //            }
        //        }
        //    }
        //}
    }
}

using LR2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LR2;
using System.Collections;

namespace LR2.Controllers
{
    public class HomeController : Controller
    {
        //frist task
        private int _quantity = 0;
        private List<Rectanlge> Rectangles = new List<Rectanlge>();
        private float _min = 0;

        //third task
        private List<float> Num = new List<float>();
        private float _max;
        private int count;
        

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //First
        public IActionResult TaskFirst()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TaskFirst(string StrWithRect, string Width, string NumOfRect ,string action)
        {
            if (int.TryParse(NumOfRect, out int quantity))
            {
                AddQuantity(quantity);
                LookStr(StrWithRect);
                if (_quantity == Rectangles.Count)
                {
                    @ViewBag.S = Calculate();
                    return View();
                }
                else
                    return RedirectToAction("Error");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //Second
        public IActionResult TaskSecond()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TaskSecond(string Text)
        {
            try
            {
                if (Text != null)
                {
                    string strWithNum = "";
                    foreach (var ch in Text)
                    {
                        if (ch >= 48 && ch <= 58)
                        {
                            strWithNum += ch;
                            strWithNum += ',';
                        }
                    }
                    strWithNum = strWithNum.TrimEnd(',');
                    ViewBag.N = strWithNum;
                    return View();
                }
                else
                    return RedirectToAction("Error");
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }

        //Third
        public IActionResult TaskThird()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TaskThird(string Numbers)
        {
            try
            {
                if (Numbers != null)
                {
                    if (InArray(Numbers))
                    {
                        Sort();
                        string str = "";
                        foreach (var item in Num)
                        {
                            str += item.ToString() + ',';
                        }
                        str = str.TrimEnd(',');
                        ViewBag.Mas = str;
                        ViewBag.Max = _max;
                        return View();
                    }
                    else
                        return RedirectToAction("Error");
                }
                else
                    return RedirectToAction("Error");
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //methods
        private float Calculate()
        {
            if (_min == 0)
            {
                _min = Rectangles[0].Square;
            }
            for (int i = 0; i < Rectangles.Count; i++)
            {
                if (_min > Rectangles[i].Square)
                    _min = Rectangles[i].Square;
            }
            return _min;
        }

        private void LookStr(string _str)
        {
            string str = "";
            float _l = 0, _w = 0;


            for (int i = 0; i < _str.Length; i++)
            {
                str += _str[i];
                if (_str[i] == 44)
                {
                    float.TryParse(str, out float length);
                    _l = length;
                    str = "";
                    continue;
                }
                else if (_str[i] == 32)
                {
                    float.TryParse(str, out float width);
                    _w = width;
                    AddRectangle(_l, _w);
                    _l = 0;
                    _w = 0;
                    str = "";
                }
                else if(i + 1 == _str.Length)
                {
                    float.TryParse(str, out float width);
                    _w = width;
                    AddRectangle(_l, _w);
                }
            }
        }

        private void AddRectangle(float length, float width)
        {
            if (_quantity >= Rectangles.Count)
            {
                Rectanlge rectanlge = new Rectanlge(length, width);
                Rectangles.Add(rectanlge);
            }
        }

        private void AddQuantity(int NumOfRectangles)
        {
            if (NumOfRectangles > 0)
            {
                if (_quantity == 0)
                {
                    _quantity = NumOfRectangles;
                }
            }
        }

        private void Sort()
        {
            _max = Num[0];
            for (int i = 0; i < Num.Count; i++)
            {
                if (_max < Num[i])
                {
                    _max = Num[i];
                    count = i;
                }
            }
            for (int i = Num.Count - 1; count < i; i--)
            {
                Num[i] = 0;
            }
        }
        private bool InArray(string _numbers)
        {
            string str = "";
            string last = "";
            foreach (var ch in _numbers)
            {
                str += ch;
                last = str;
                if (ch == 44 || ch == 32)
                {
                    if (float.TryParse(str, out float n))
                    {
                        Num.Add(n);
                        str = "";
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            if (float.TryParse(last, out float ls))
            {
                Num.Add(ls);
                return true;
                
            }
            else
            {
                return false;
            }
        }

    }
}

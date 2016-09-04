using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartHouseMVCv3;

namespace SmartHouseMVCv3
{

    public class SmartHouseController : Controller
    {
        IDictionary<int, Device> devicesDictionary;
        private int id;
        public void DeviceControl(int id, IDictionary<int, Device> devicesDictionary)
        {
            this.id = id;
            this.devicesDictionary = devicesDictionary;
        }

        public ActionResult Index()
        {
            if (Session["Devices"] == null)
            {
                devicesDictionary = new SortedDictionary<int, Device>();
                devicesDictionary.Add(1, new Lamp(false, "Лампа", BrightnessLevel.High));
                devicesDictionary.Add(2, new Conditioner(false, "Кондиционер"));
                devicesDictionary.Add(3, new Fridge(false, "Холодильник"));
                devicesDictionary.Add(4, new TV(false, "Телевизор"));
                devicesDictionary.Add(5, new Radio(false, "Радио"));
                Session["Devices"] = devicesDictionary;
                Session["NextId"] = 6;
            }
            else
            {
                devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            }
            SelectListItem[] devicesList = new SelectListItem[5];
            devicesList[0] = new SelectListItem { Text = "Лампа", Value = "lamp", Selected = true };
            devicesList[1] = new SelectListItem { Text = "Кондиционер", Value = "conditioner" };
            devicesList[2] = new SelectListItem { Text = "Холодильник", Value = "fridge" };
            devicesList[3] = new SelectListItem { Text = "Телевизор", Value = "tv" };
            devicesList[4] = new SelectListItem { Text = "Радио", Value = "radio" };
            ViewBag.DevicesList = devicesList;
            return View(devicesDictionary);
        }

        public ActionResult Add(string deviceType)
        {
            Device newDevice;
            switch (deviceType)
            {
                default:
                    newDevice = new Lamp(false, "Лампа", BrightnessLevel.High);
                    break;
                case "conditioner":
                    newDevice = new Conditioner(false, "Кондиционер");
                    break;
                case "fridge":
                    newDevice = new Fridge(false, "Холодильник");
                    break;
                case "tv":
                    newDevice = new TV(false, "Телевизор");
                    break;
                case "radio":
                    newDevice = new Radio(false, "Радио");
                    break;
            }
            int id = (int)Session["NextId"];
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            devicesDictionary.Add(id, newDevice);
            Session["Devices"] = devicesDictionary;
            id++;
            Session["NextId"] = id;
            return RedirectToAction("Index");
        }

        public ActionResult SetLampMode(int id, string lampMode)
        {

            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ILampMode l = (ILampMode)devicesDictionary[id];
            Session["LampMode"] = lampMode;
            switch (lampMode)
            {
                case "High":
                    l.SetHighBrightness();
                    break;
                case "Medium":
                    l.SetMediumBrightness();
                    break;
                case "Low":
                    l.SetLowBrightness();
                    break;
            }
            Session["Devices"] = devicesDictionary;
            return RedirectToAction("Index");
        }

        public ActionResult SetFridgeMode(int id, string fridgeMode)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            IFridgeMode f = (IFridgeMode)devicesDictionary[id];
            Session["FridgeMode"] = fridgeMode;
            switch (fridgeMode)
            {
                case "Defrost":
                    f.SetDefrost();
                    break;
                case "Freezing":
                    f.SetFreezing();
                    break;
                case "Superfreezing":
                    f.SetSuperFreezing();
                    break;
            }
            Session["Devices"] = devicesDictionary;
            return RedirectToAction("Index");
        }

        public ActionResult SetConditionerMode(int id, string conditionerMode)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            IConditionerMode c = (IConditionerMode)devicesDictionary[id];
            Session["ConditionerMode"] = conditionerMode;
            switch (conditionerMode)
            {
                case "Cool":
                    c.SetCoolMode();
                    break;
                case "Dry":
                    c.SetDryMode();
                    break;
                case "Fan":
                    c.SetFanMode();
                    break;
                case "Heat":
                    c.SetHeatMode();
                    break;
            }
            Session["Devices"] = devicesDictionary;
            return RedirectToAction("Index");
        }

        public ActionResult SetTemp(int id, string temp)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ISetTemp t = (ISetTemp)devicesDictionary[id];
            int correntTemp;
            if (Int32.TryParse(temp, out correntTemp))
            {
                if (correntTemp >= 16 && correntTemp <= 30)
                {
                    t.SetTemp(correntTemp);
                }
                else
                {
                    TempData["Error"] = "Введите значение от 16 до 30!";
                }
            }
            else
            {
                TempData["Error"] = "Введите только цифры!";
            }
            Session["Devices"] = devicesDictionary;
            return RedirectToAction("Index");
        }

        public ActionResult SetVolume(int id, string volume)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ISetVolume v = (ISetVolume)devicesDictionary[id];
            int correntVol;
            if (Int32.TryParse(volume, out correntVol))
            {
                if (correntVol >= 0 && correntVol <= 100)
                {
                    v.SetVolume(correntVol);
                }
                else
                {
                    TempData["Error"] = "Введите значение от 0 до 100!";
                }
            }
            else
            {
                TempData["Error"] = "Введите только цифры!";
            }
            Session["Devices"] = devicesDictionary;
            return RedirectToAction("Index");
        }

        public ActionResult SetChannel(int id, string channel)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ISetChannel c = (ISetChannel)devicesDictionary[id];
            int currentChan;
            if (Int32.TryParse(channel, out currentChan))
            {
                if (currentChan >= 0 && currentChan <= 100)
                {
                    c.SetChannel(currentChan);
                }
                else
                {
                    TempData["Error"] = "Введите значение от 0 до 100!";
                }
            }
            else
            {
                TempData["Error"] = "Введите только цифры!";
            }
            Session["Devices"] = devicesDictionary;
            return RedirectToAction("Index");
        }

        public ActionResult SetWave(int id, string wave)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ISetWave w = (ISetWave)devicesDictionary[id];
            double currentWave;
            string temp = wave;
            temp = temp.Replace('.', ',');
            if (Double.TryParse(temp, out currentWave))
            {
                if (currentWave >= 87.5 && currentWave <= 108)
                {
                    w.SetWave(currentWave);
                }
                else
                {
                    TempData["Error"] = "Введите значение от 87.5 до 108!";
                }
            }
            else
            {
                TempData["Error"] = "Введите только цифры!";
            }
            Session["Devices"] = devicesDictionary;
            return RedirectToAction("Index");
        }

        public ActionResult OnMode(int id)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            devicesDictionary[id].OnDevice();
            return RedirectToAction("Index");
        }

        public ActionResult OffMode(int id)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            devicesDictionary[id].OffDevice();
            return RedirectToAction("Index");
        }

        public ActionResult UpWave(int id)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ISetWave w = (ISetWave)devicesDictionary[id];
            w.UpWave();
            return RedirectToAction("Index");
        }

        public ActionResult LessWave(int id)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ISetWave w = (ISetWave)devicesDictionary[id];
            w.LessWave();
            return RedirectToAction("Index");
        }

        public ActionResult NextChannel(int id)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ISetChannel c = (ISetChannel)devicesDictionary[id];
            c.NextChannel();
            return RedirectToAction("Index");
        }

        public ActionResult PreviousChannel(int id)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ISetChannel c = (ISetChannel)devicesDictionary[id];
            c.PreviousChannel();
            return RedirectToAction("Index");
        }

        public ActionResult UpVolume(int id)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ISetVolume v = (ISetVolume)devicesDictionary[id];
            v.UpVolume();
            return RedirectToAction("Index");
        }

        public ActionResult LessVolume(int id)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ISetVolume v = (ISetVolume)devicesDictionary[id];
            v.LessVolume();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            devicesDictionary.Remove(id);
            Session["Devices"] = devicesDictionary;
            return RedirectToAction("Index");
        }
    }
}
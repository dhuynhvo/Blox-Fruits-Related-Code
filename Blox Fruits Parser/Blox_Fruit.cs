//grabs the current stock from the blox fruits wiki and plays loud noise on timer intervals if fruit is in stock

using System.Net;
using System;
using System.Timers;
using System.Media;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.ApplicationServices;

namespace Blox_Fruit_Parser
{
    class Blox_Fruit
    {
        public static string[] WantedDF = { "Light" };
        public static string DF = "Light";
        public static void Main()
        {
            Console.WriteLine("Looking for: " + DF);
            System.Timers.Timer aTimer = new()
            {
                Interval = 900000  // 15 minute interval
                //Interval == 1800000 //30 minute interval
            };
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Enabled = true;
            aTimer.AutoReset = true;
            Console.WriteLine("Press Any Key To Exit:");
            Console.ReadLine();
        }

        public static async Task<string> Trawler(HttpClient c)
        {
            string[] GetRidOf = { " Last Stock", "<meta name=\"twitter:description\" ", "<meta name=\"description\" ",
                                "<meta property=\"og:description\" " };
            string content = await c.GetStringAsync("https://blox-fruits.fandom.com/wiki/Blox_Fruits_%22Stock%22");
            string newContent = content.Replace(",", "");
            string[] newContentArray = newContent.Split(GetRidOf, StringSplitOptions.None);
            
            foreach(string aStr in newContentArray)
            {
                if(aStr.Contains("content=\"Current Stock:"))
                {
                    string currentStock = aStr;
                    return currentStock;
                }
            }

            return "Nothing???";
        }

        public static async void OnTimedEvent(object? source, ElapsedEventArgs e)
        {
            using var client = new HttpClient();
            string stockString = await Trawler(client);
            string[] stock = stockString.Split(' ');
            SoundPlayer simpleSound = new("C:\\Users\\danvi\\lightning.wav");

            foreach (string stk in stock)
            {
                foreach (string wanted in WantedDF)
                {
                    if (stk.Contains(wanted))
                    {
                        Console.WriteLine(stk + ": is in stock.");
                        simpleSound.PlaySync();
                        simpleSound.PlaySync();
                        simpleSound.PlaySync();
                        simpleSound.PlaySync();
                        simpleSound.PlaySync();
                    }
                }
            }
        }
    }
}

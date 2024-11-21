using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using RR_Lib;
using System.Runtime.InteropServices;

namespace RussianRoulette
{
    class Participant
    {
        public string Name { get; set; }
        public int Lifes { get; set; }

        public Participant(string name, int lifes)
        {
            Name = name;
            Lifes = lifes;
        }
        public string GetName()
        {
            return Name;
        }
        public int GetLifes()
        {
            return Lifes;
        }
    }
    class ClassicMode
    {
        public TextDecoration td = new TextDecoration();
        public int RevolverLength { set; get; } = 6;
        public int[] Revolver {  set; get; }  = new int[6];    
        public int participantsAmount = 6;
        public int participantsLifes = 1;
        public int RoundAmount = 1;
        public List<Participant> participants { get; set; } = new List<Participant>();

        public void FillRevolver()
        {
            var rand = new Random();
            int randPos = rand.Next(0, 6);

            for(int i = 0; i < Revolver.Length; i++)
            {
                if(i == randPos)
                {
                    Revolver[i] = 1;
                }
                else
                {
                    Revolver[i] = 0;
                }
            }
        }
        public void RunGame()
        {
            Console.Clear();
            int roundCounter = 0;
            int revolverCounter = 0;
            int timeDelay = 1500;

            while (roundCounter < RoundAmount) {
                Console.WriteLine("Round " + roundCounter + 1);
                td.UnderscoreMinus();

                for (int i = 0; i < participants.Count; i++)
                {
                    Console.WriteLine();
                    Console.WriteLine();

                    if (participants[i].GetName() != "you")
                    {
                        Thread.Sleep(timeDelay);
                        Console.Write(participants[i].GetName() + "'s turn: ");
                        if (Revolver[revolverCounter] == 1)
                        {
                            Thread.Sleep(timeDelay);
                            Console.Write("PENG! " + participants[i].GetName() + " was  shot!");
                            Console.WriteLine("\nYou've won (for now...)");
                            GameOver();
                            Thread.Sleep(timeDelay);
                            break;
                        }
                        else if (Revolver[revolverCounter] == 0)
                        {
                            Thread.Sleep(timeDelay);
                            Console.Write("CLACK! " + participants[i].GetName() + " was  lucky!");
                        }
                    }
                    else if (participants[i].GetName() == "you")
                    {
                        Thread.Sleep(timeDelay);
                        Console.WriteLine("Your turn: [enter] to shoot");
                        Console.ReadLine();

                        if (Revolver[revolverCounter] == 1)
                        {
                            Thread.Sleep(timeDelay);
                            Console.Write("PENG! You were shot!");                           
                            Console.WriteLine("\nYou've lost!");
                            GameOver();
                            Thread.Sleep(timeDelay);
                            break;
                        }
                        else if (Revolver[revolverCounter] == 0)
                        {
                            Thread.Sleep(1000);
                            Console.Write("CLACK! You were lucky!");
                        }
                    }
                    revolverCounter++;
                }
                roundCounter++;
            }
            Console.WriteLine("\n[enter] back to Main Menu");
            Console.ReadLine();
        }
        public void CreateParticipants()
        {
            Participant pc1 = new Participant("DeadEye", participantsLifes);
            Participant pc2 = new Participant("Carlos Sanches", participantsLifes);
            Participant pc3 = new Participant("James Cook", participantsLifes);
            Participant pc4 = new Participant("The old man", participantsLifes);
            Participant pc5 = new Participant("DeathHunter", participantsLifes);
            Participant pc6 = new Participant("you", participantsLifes);


            participants.Add(pc1);
            participants.Add(pc2);
            participants.Add(pc3);
            participants.Add(pc4);
            participants.Add(pc5);
            participants.Add(pc6);

            Shuffle(participants);

        }
        public void PrintParticipants()
        {
            for(int i = 0; i < participants.Count; i++)
            {
                Console.WriteLine(i + 1 + ".participant: " + participants[i].GetName());
            }
        }
        private static Random rng = new Random();
        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public void GameOver()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("GAME OVER");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    class ExtendedClassicMode
    {
        public TextDecoration td = new TextDecoration();
        public int RevolverLength { set; get; } = 6;
        public int[] Revolver { set; get; } = new int[6];
        public int participantsAmount = 6;
        public int participantsLifes = 1;
        public int RoundAmount = 1;
        public List<Participant> participants { get; set; } = new List<Participant>();
    }
    internal class Program
    {
        public static TextDecoration td = new TextDecoration();
        public static string[] options = { "Classic Mode", "DeadEye Mode", "Hybrid Mode", "Extended Classic Mode" };
        public static int selectedIndex = 0;

        //Main
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            MainMenu();
            StartSpecificGameMode();

            Console.ReadLine();
        }
        static void StartSpecificGameMode()
        {
            if (selectedIndex == 0)
            {
                ClassicMode();
            }
        }
        //CLASSIC MODE
        static void ClassicMode()
        {
            ClassicMode cm = new ClassicMode();
            cm.FillRevolver();
            cm.CreateParticipants();
            cm.PrintParticipants();
            td.UnderscoreMinus();
            Console.WriteLine("[enter] to start");
            Console.ReadLine();
            cm.RunGame();
           
            //Ende des Spiels, zurück zum Main Menu
            MainMenu();
        }
        //MAIN MENU
        static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("RUSSIAN ROULETTE");
                Console.WriteLine("\nMain Menu");
                td.UnderscoreColon();
                Console.WriteLine("choose a mode by using arrow keys and [enter]\n");

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"  {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(options[i]);
                    }
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                    if (selectedIndex < 0) selectedIndex = options.Length - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedIndex++;
                    if (selectedIndex >= options.Length) selectedIndex = 0;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Console.WriteLine($"So you've chosen {options[selectedIndex]}? Good choice....\n[enter]");
                    Console.ReadLine();
                    Console.Clear();
                    break;                    
                }
            }
        }
    }
}

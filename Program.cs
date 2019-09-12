using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    class Program
    {
        static void Main(string[] args)
        {
            int qualifiedChanges = 0, sequenceChange = 0, rainChances = 0;
            float count;
            bool correctQualifiersOccured = false, incorrectQualifiersOcccured = false, correctSequenceOccured = false, incorrectSequenceOccured = false;

            Console.WriteLine("Enter Chances of Rain in %");
            rainChances = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Number of Trials");
            float.TryParse(Console.ReadLine(), out count);



           // int[,] pointsTable = new int[9, 5];
            Team[] teams = new Team[9];

            for (int tr = 0; tr < count; tr++)
            {



                for (int i = 0; i < 9; i++)
                    teams[i] = new Team(((i + 1) * 100) + (i + 1));





                simulateMatches(ref teams, rainChances, 5);

                Team[] t1 = new Team[9];
                Team[] t2 = new Team[9];
                calculatePoints(teams, 1, 1, 2, ref t1);
               // printPointsTable(t1);

               // Console.WriteLine("\n");
                calculatePoints(teams, 2, 1, 2, ref t2);

               // printPointsTable(t1);
               // Console.WriteLine();
               // printPointsTable(t2);

               // Console.WriteLine(compareQualifiedTeams(t1, t2));
                int qualifiedResult = compareQualifiedTeams(t1, t2);
                int sequenceResult = compareOrderOfTeams(t1, t2);

                if(qualifiedResult == 0 && !correctQualifiersOccured)
                {
                    correctQualifiersOccured = true;
                    correctSequenceOccured = true;
                    Console.WriteLine("TRIAL #" + (tr + 1));
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nRAIN POINT = 1\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    printPointsTable(t1, ConsoleColor.Green);


                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nRAIN POINT = 2\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    printPointsTable(t2, ConsoleColor.Green);
                    Console.WriteLine("\n\nPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }


                if (qualifiedResult == 1 && !incorrectQualifiersOcccured)
                {
                    incorrectQualifiersOcccured = true;
                    incorrectSequenceOccured = true;
                    Console.WriteLine("TRIAL #" + (tr + 1));
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nRAIN POINT = 1\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    printPointsTable(t1, ConsoleColor.Red);


                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nRAIN POINT = 2\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    printPointsTable(t2, ConsoleColor.Red);
                    Console.WriteLine("\n\nPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }

                /*
                if (sequenceResult == 0 && !correctSequenceOccured)
                {
                    correctSequenceOccured = true;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("RAIN POINT = 1\n\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    printPointsTable(t1,ConsoleColor.Green);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("RAIN POINT = 2\n\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    printPointsTable(t2, ConsoleColor.Green);
                    Console.WriteLine("\n\nPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }

                if (sequenceResult == 1 && !incorrectSequenceOccured)
                {
                    incorrectSequenceOccured = true;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("RAIN POINT = 1\n\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    printPointsTable(t1, ConsoleColor.Red);


                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("RAIN POINT = 2\n\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    printPointsTable(t2, ConsoleColor.Red);
                    Console.WriteLine("\n\nPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }

    */
                qualifiedChanges += qualifiedResult;
                sequenceChange += sequenceResult;
                
                
           
                //Console.Clear();
                // printPointsTable(pointsTable,9,5);

            }
            Console.WriteLine ((qualifiedChanges / count) * 100.0 + "% times Different teams qualified in both cases");
            Console.WriteLine((sequenceChange / count) * 100.0 + "% times Same teams qualified but, in different sequence");



        }

        static int compareOrderOfTeams(Team[] t1, Team[] t2)
        {
            int count = 0;
            for(int i = 0; i < 4; i++)
            {
                if (t1[i].getId() != t2[i].getId())

                {
                    count++;
                    break;
                }   
            }

            return count;
        }

        static int compareQualifiedTeams(Team[] t1, Team[] t2)
        {
            // this function takes two points table in input and compare that the qualified teams of both are similar or not
           // returns 0 if similar else 1
            int count = 0;
            
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    if(t1[i].getId() == t2[j].getId())
                    {
                        count++;
                        break;
                    }
                }
            }

            if (count < 4)
                return 1;
            else
                return 0;
        }

        static void calculatePoints(Team[] team, int rainPoints, int drawPoints, int winPoints, ref Team[] t)
        {
            
            int[] qualifiedTeams = new int[4];

            for(int i = 0; i < team.Length; i++)
            {
                team[i].calcPoints(winPoints, rainPoints, drawPoints);
            }

            Team temp;
            int smallest;
            for (int i = 0; i < team.Length; i++)
            {
                smallest = i;
                for (int j = i + 1; j < team.Length; j++)
                {
                    if (team[j].getPoints() > team[smallest].getPoints())
                    {
                        smallest = j;
                    }

                    else if(team[j].getPoints() == team[smallest].getPoints())
                    {
                        if (team[j].getWins() > team[smallest].getWins())
                            smallest = j;
                    }
                }
                temp = team[smallest];
                team[smallest] = team[i];
                team[i] = temp;
            }

            for(int i = 0; i < team.Length; i++)
            {
                t[i] = new Team(team[i]);
            }
            
            
        }

        static void simulateMatches(ref Team[] teams, int rainChances, int tieChances)
        {

            int iChances = (100-rainChances-tieChances)/2, jChances = (100 - rainChances - tieChances) / 2;
            Random r = new Random();
            for (int i = 0; i < teams.Length; i++)
            {
                for (int j = i + 1; j < teams.Length; j++)
                {

                    int result = r.Next(100); // 0 = loose, win, rain, draw

                    if (result <= iChances)
                        teams[i].teamsWins();

                    else if (result > iChances && result <= iChances * 2)
                        teams[j].teamsWins();

                    else if (result > iChances * 2 && result < 100 - tieChances)
                        teams[i].rainStoppedPlay();

                    else
                        teams[i].matchTied();
                    


                }
            }
        }

        static void printPointsTable(Team[] pTable, ConsoleColor c)
        {
            Console.ForegroundColor = c;
            for (int i = 0; i < pTable.Length; i++)
            {

                //  Console.Write(pTable[i].getId + " ");
                if (i == 4)
                {
                    
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(pTable[i].getTeamString());
            }
        }
    }
}

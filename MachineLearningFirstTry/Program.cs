using System;

namespace GeneticAlgorithm
{
    class Program
    {
        public static int Goal;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write("Welcome to LeanZo's Genetic Algorithm!\nThis program is inspired by the proccess of natural selection.\nGenetic algorithms are used to generate high-quality and optimized solution using principles as mutation and natural selection.\n");
            Console.Write("This program operates on the following manner: There is a 'population', filled with 'beings', each of these having traits represented by the old eight deadly sins.\nThese traits have a range of 0 to 100. The user sets a goal.");
            Console.Write("The program runs.\nFor each 'being' of the first generation it sets random traits values, then the program calculates the average of these traits.\nThe 'being' which average is closer to the goal lives on and a new generation is created, ");
            Console.Write("filled with clones of this 'being'.\nThe clones are mutated randomily. The once again the program search for the 'being' closer to the goal.\nThis proccess goes on until a 'being' with average equal to the goal is found.\n");

            Console.ForegroundColor = ConsoleColor.Red;

            Console.Write("Enter the goal(0 - 100): ");
            Goal = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the population size: ");
            Population population = new Population(Convert.ToUInt32(Console.ReadLine()));

            Console.ForegroundColor = ConsoleColor.Green;

            //Shows the best being of each generation while they do not reach the goal
            while (!population.SearchForFittest())
            {
                Console.WriteLine("\nGEN: {0}\nBeing: {1}/{2}\nID: {3}\n---\nLust: {4}\nGluttony: {5}\nGreed: {6}\nSloth: {7}\nWrath: {8}\nEnvy: {9}\nPride: {10}\nVainglory: {11}\n---\nAverage: {12}\n",
                    population.gen, population.bestBeing + 1, population.beings.Length, population.beings[population.bestBeing].id, population.beings[population.bestBeing].Lust, population.beings[population.bestBeing].Gluttony,
                    population.beings[population.bestBeing].Greed, population.beings[population.bestBeing].Sloth, population.beings[population.bestBeing].Wrath,
                    population.beings[population.bestBeing].Envy, population.beings[population.bestBeing].Pride, population.beings[population.bestBeing].Vainglory,
                    population.Average(population.beings[population.bestBeing]));
                
                population.NaturalSelection();
            }

            Console.WriteLine("\nPERFECT!\nGEN: {0}\nBeing: {1}/{2}\nID: {3}\n---\nLust: {4}\nGluttony: {5}\nGreed: {6}\nSloth: {7}\nWrath: {8}\nEnvy: {9}\nPride: {10}\nVainglory: {11}\n---\nAverage: {12}\n",
                    population.gen, population.bestBeing + 1, population.beings.Length, population.beings[population.bestBeing].id, population.beings[population.bestBeing].Lust, population.beings[population.bestBeing].Gluttony,
                    population.beings[population.bestBeing].Greed, population.beings[population.bestBeing].Sloth, population.beings[population.bestBeing].Wrath,
                    population.beings[population.bestBeing].Envy, population.beings[population.bestBeing].Pride, population.beings[population.bestBeing].Vainglory,
                    population.Average(population.beings[population.bestBeing]));
            Console.Beep();

            Console.ReadLine();
        }
    }

    class Being
    {
        //The traits
        public double Lust, Gluttony, Greed, Sloth, Wrath, Envy, Pride, Vainglory;

        public uint id;

        public bool isBest = false;

        public Population population;

        //Atributes an unique id for each being
        public Being(Population population)
        {
            id = population.id++;

            this.population = population;
        }

        //Randomizes each trait
        public void Randomize()
        {
            Random rnd = new Random();

            Lust = rnd.Next(101);
            Gluttony = rnd.Next(101);
            Greed = rnd.Next(101);
            Sloth = rnd.Next(101);
            Wrath = rnd.Next(101);
            Envy = rnd.Next(101);
            Pride = rnd.Next(101);
            Vainglory = rnd.Next(101);
        }

        //Mutates each trait
        public void Mutate()
        {
            double mutationRate = 0.04;

            Random rnd = new Random();

            if (rnd.NextDouble() < mutationRate)
                Lust = rnd.Next(101);

            if (rnd.NextDouble() < mutationRate)
                Gluttony = rnd.Next(101);

            if (rnd.NextDouble() < mutationRate)
                Greed = rnd.Next(101);

            if (rnd.NextDouble() < mutationRate)
                Sloth = rnd.Next(101);

            if (rnd.NextDouble() < mutationRate)
                Wrath = rnd.Next(101);

            if (rnd.NextDouble() < mutationRate)
                Envy = rnd.Next(101);

            if (rnd.NextDouble() < mutationRate)
                Pride = rnd.Next(101);

            if (rnd.NextDouble() < mutationRate)
                Vainglory = rnd.Next(101);
        }

        public Being Baby()
        {
            Being baby = new Being(population);
            CloneThisTo(baby);
            return baby;
        }

        void CloneThisTo(Being being)
        {
            being.Lust = Lust;
            being.Gluttony = Gluttony;
            being.Greed = Greed;
            being.Sloth = Sloth;
            being.Wrath = Wrath;
            being.Envy = Envy;
            being.Pride = Pride;
            being.Vainglory = Vainglory;
        }
    }

    class Population
    {
        public uint id = 1;

        public Being[] beings;

        public uint bestBeing = 0;

        public uint gen = 1;

        //Creates a population of a determined size and randomize the traits
        public Population(uint size)
        {
            beings = new Being[size];

            for (uint i = 0; i < size; i++)
            {
                beings[i] = new Being(this);
                beings[i].Randomize();
            }
        }

        public double Average(Being being)
        {
            return (being.Lust + being.Gluttony + being.Greed + being.Sloth + being.Wrath + being.Envy + being.Pride + being.Vainglory) / 8.00;
        }

        //Checks which of two beings' average is closer to the goal
        Being FittestOf(Being being1, Being being2)
        {
            double distance1, distance2;

            distance1 = Average(being1) > Program.Goal ? Average(being1) - Program.Goal : Program.Goal - Average(being1);

            distance2 = Average(being2) > Program.Goal ? Average(being2) - Program.Goal : Program.Goal - Average(being2);

            return distance1 <= distance2 ? being1 : being2;
        }

        //Searches for the being closer to the goal
        public bool SearchForFittest()
        {
            for(uint i = 0; i < beings.Length; i++)
            {
                if(Average(beings[i]) == Program.Goal)
                {
                    bestBeing = i;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    return true;//Returns true when a being reachs the goal
                }
                else if (FittestOf(beings[bestBeing], beings[i]) == beings[i])
                {
                    bestBeing = i;
                }
            }

            //Changes color when a new being is the best
            if (beings[bestBeing].id != beings[0].id)
            {
                if (Console.ForegroundColor == ConsoleColor.Blue)
                    Console.ForegroundColor = ConsoleColor.Magenta;
                else
                    Console.ForegroundColor = ConsoleColor.Blue;
            }

            return false;
        }

        //Creates a new generation, the fittest of the last lives on, then clones it and mutates the clones
        public void NaturalSelection()
        {
            Being[] newBeings = new Being[beings.Length];

            newBeings[0] = beings[bestBeing];
            newBeings[0].isBest = true;

            for (uint i = 1; i < newBeings.Length; i++)
            {
                newBeings[i] = beings[bestBeing].Baby();
                newBeings[i].Mutate();
            }

            beings = newBeings;
            gen++;
        }
    }


}

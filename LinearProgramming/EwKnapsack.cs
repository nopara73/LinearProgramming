using Google.OrTools.LinearSolver;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinearProgramming
{
    public class EwKnapsack
    {
        public void Run()
        {
            // https://ewinnington.github.io/posts/jupyter-lp-20

            //Array of items, weights and totals
            int nItems = 10;
            double maxWeight = 220;
            double[] weights = { 31, 27, 12, 39, 2, 69, 66, 29, 45, 58 };
            double[] values = { 24, 27, 26, 15, 19, 33, 30, 28, 65, 42 };

            using Solver milp_solver = Solver.CreateSolver("MILP", "CBC_MIXED_INTEGER_PROGRAMMING");

            var items = milp_solver.MakeBoolVarArray(nItems, "Items");

            using var objective = milp_solver.Objective();
            for (int i = 0; i < nItems; i++)
            {
                objective.SetCoefficient(items[i], values[i]);
            }
            objective.SetMaximization();

            using var c0 = milp_solver.MakeConstraint(0, maxWeight);
            for (int i = 0; i < nItems; i++)
            {
                objective.SetCoefficient(items[i], weights[i]);
            }

            var resultStatus = milp_solver.Solve();

            if (resultStatus != Solver.ResultStatus.OPTIMAL)
            {
                Console.WriteLine("The problem does not have an optimal solution!");
                return;
            }

            Console.WriteLine("Problem solved in " + milp_solver.WallTime() + " milliseconds");

            Console.WriteLine("Optimal objective value = " + milp_solver.Objective().Value());

            foreach (var v in milp_solver.variables())
            {
                Console.WriteLine($"{v.Name()} : {v.SolutionValue()} ");
            }
        }
    }
}

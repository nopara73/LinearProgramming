using Google.OrTools.LinearSolver;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinearProgramming
{
    public class EwIntro
    {
        public void Run()
        {
            // https://ewinnington.github.io/posts/jupyter-lp-10

            using var solver = Solver.CreateSolver("LinearProgramming", "CLP_LINEAR_PROGRAMMING");

            using var x = solver.MakeNumVar(0, double.PositiveInfinity, "x");
            using var y = solver.MakeNumVar(0, double.PositiveInfinity, "y");

            using var objective = solver.Objective();
            objective.SetCoefficient(x, 1);
            objective.SetCoefficient(y, 2);
            objective.SetMaximization();

            using var c0 = solver.MakeConstraint(0, 15);
            c0.SetCoefficient(x, 1);

            using var c1 = solver.MakeConstraint(0, 8);
            c1.SetCoefficient(y, 1);

            using var c2 = solver.MakeConstraint(0, 18);
            c2.SetCoefficient(x, 1);
            c2.SetCoefficient(y, 1);

            using var c3 = solver.MakeConstraint(0, 2);
            c3.SetCoefficient(x, -1 / 3);
            c3.SetCoefficient(y, 1);

            var resultStatus = solver.Solve();

            if (resultStatus != Solver.ResultStatus.OPTIMAL)
            {
                Console.WriteLine("The problem does not have an optimal solution!");
                return;
            }

            Console.WriteLine("Problem solved in " + solver.WallTime() + " milliseconds");

            Console.WriteLine("Optimal objective value = " + solver.Objective().Value());

            foreach (var v in solver.variables())
            {
                Console.WriteLine($"{v.Name()} : {v.SolutionValue()} ");
            }
        }
    }
}

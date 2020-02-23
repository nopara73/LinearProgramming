using Google.OrTools.LinearSolver;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinearProgramming
{
    public class EwWire
    {
        public void Run()
        {
            // https://ewinnington.github.io/posts/jupyter-lp-20

            // 1kg aluminium, 10kWh electricity, 1/2h labour
            // 1kg copper,    4kWh  electricity, 1h   labour
            // electricity        <= 450kWh/day
            // labour             <= 42.5h /day
            // aluminium + copper <= 56kg  /day
            // electricity     20$  /mWh
            // labour          11$  /h
            // aluminium cost  1.8$/kg
            // copper    cost  5.4$/kg
            // aluminium price 45$ /kg
            // copper    price 50$ /kg

            // What should be produced to maximise profit and what is the maximum profit?

            using Solver solver = Solver.CreateSolver("LinearProgramming", "CLP_LINEAR_PROGRAMMING");

            using var al = solver.MakeNumVar(0, double.PositiveInfinity, "al");
            using var cu = solver.MakeNumVar(0, double.PositiveInfinity, "cu");
            double electricityPrice = 20;
            double electricityMax = 450;
            double labourPrice = 11;
            double labourMax = 42.5;
            double weightMax = 56;
            double aluminiumPurchase = 1.8;
            double aluminiumSale = 45;
            double copperPurchase = 5.4;
            double copperSale = 50;

            using var objective = solver.Objective();
            objective.SetCoefficient(al, aluminiumSale - aluminiumPurchase - (labourPrice * 1 / 2.0) - (electricityPrice * 10 / 1000.0));
            objective.SetCoefficient(cu, copperSale - copperPurchase - (labourPrice * 1) - (electricityPrice * 4 / 1000.0));
            objective.SetMaximization();

            using var c0 = solver.MakeConstraint(0, electricityMax);
            c0.SetCoefficient(al, 1/2.0);
            c0.SetCoefficient(cu, 1);

            using var c1 = solver.MakeConstraint(0, labourMax);
            c1.SetCoefficient(al, 10);
            c1.SetCoefficient(cu, 4);

            using var c2 = solver.MakeConstraint(0, weightMax);
            c2.SetCoefficient(al, 1);
            c2.SetCoefficient(cu, 1);

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

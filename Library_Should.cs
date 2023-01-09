using System;
using NUnit.Framework;

namespace AreaLibrary
{
    [TestFixture]
    public class LibraryShould
    {
        [TestCase(1,Math.PI)]
        [TestCase(10,314.16)]
        [TestCase(4.4,60.82)]
        public void GetCircleArea_IsCorrect(double radius, double expected)
        {
            var actual = AreaCalculator.GetCircleArea(radius);
            Assert.AreEqual(expected, actual, .1);
        }

        [TestCase(1,1,1,0.433)]
        [TestCase(4,2,5,3.8)]
        [TestCase(10,10,12,48)]
        [TestCase(12,0,12,0)]
        public void GetTriangleArea_IsCorrect(double a, double b, double c, double expected)
        {
            var actual = AreaCalculator.GetTriangleArea(a, b, c);
            Assert.AreEqual(expected, actual, 0.1);
        }
        
        [TestCase(-1)]
        public void ThrowExceptionOnWrongCircleInput(double radius)
        {
            Assert.Catch<ArgumentException>(() => { AreaCalculator.GetCircleArea(radius); });
        }
        
        [TestCase(10,-1,10)]
        [TestCase(10,10,344)]
        public void ThrowExceptionOnWrongCircleInput(double a, double b, double c)
        {
            Assert.Catch<ArgumentException>(() => { AreaCalculator.GetTriangleArea(a,b,c); });
        }

        [TestCase(10,5)]
        public void GetRectangleArea_IsCorrect(double a, double b)
        {
            Assert.AreEqual(a*b, AreaCalculator.GetRectangleArea(a,b), .1);
        }

        [TestCase(Math.PI,1)]
        [TestCase(314.16,10)]
        [TestCase(60.82,4.4)]

        public void GetAreaIsCorrectWithCircles(double expected, params double[] parameters)
        {
            Assert.AreEqual(AreaCalculator.GetCircleArea(parameters[0]), 
                                AreaCalculator.GetArea(parameters), 0.1);
        }

        [TestCase(0.433,1, 1, 1)]
        [TestCase(3.8, 4, 2, 5)]
        [TestCase(48, 10, 10, 12)]
        [TestCase(0, 12, 0, 12)]
        public void GetAreaIsCorrectWithTriangles(double expected, params double[] parameters)
        {
            Assert.AreEqual(AreaCalculator.GetTriangleArea(parameters[0], parameters[1], parameters[2]), 
                                AreaCalculator.GetArea(parameters), 0.1);
        }

        [TestCase(3,4,5, true)]
        [TestCase(10,5,11.18034, true)]
        [TestCase(3,4,2, false)]
        [TestCase(3,4,0, false)]
        public void CheckOrthogonalTriangleCorrectly(double a, double b, double c, bool expected)
        {
            Assert.AreEqual(expected, AreaCalculator.IsTriangleOrthogonal(a,b,c));
        }
    }
}
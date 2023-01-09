using System;
using System.Collections.Generic;
using System.Linq;

namespace AreaLibrary
{
    // Решение было задумано без применения классов, т.к. в данном случае нет уточнения, необходимо ли это,
    // а у клиента может быть своя, иная реализация классов фигур.
    
    //Метод GetArea создан для доп. условия "Вычисление площади фигуры без знания типа фигуры в compile-time"

    public static class AreaCalculator
    {
        public static double GetArea(params double[] parameters)
        {
            switch (parameters.Length)
            {
                case 1:
                    return GetCircleArea(parameters[0]);
                case 3:
                    return GetTriangleArea(parameters[0],parameters[1], parameters[2]);
            }
            throw new Exception("No available method for current set of parameters");
        }
        public static double GetCircleArea(double radius)
        {
            AreParametersValid(radius);
            if(radius == 0)
                return 0;
            
            return Math.PI * radius * radius;
        }

        public static double GetTriangleArea(double a, double b, double c)
        {
            AreParametersValid(a,b,c);
            if (a+b < c || a+c < b || b+c < a)
                throw new ArgumentException("Incorrect parameters");
            
            var s = (a + b + c) / 2;
            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }

        // Пример добавления новой фигуры в библиотеку.
        public static double GetRectangleArea(double a, double b) 
        {
            AreParametersValid(a,b);

            if (a == 0 || b == 0)
                return 0;
            
            return a * b;
        }

        public static bool IsTriangleOrthogonal(double a, double b, double c)
        {
            AreParametersValid(a,b,c);
            
            var parameters = new List<double>{a, b, c};
            var hypotenuse = parameters.Max();
            parameters.Remove(parameters.Max());
            var side1 = parameters[0];
            var side2 = parameters[1];

            return Math.Abs(side1 * side1 + side2 * side2 - hypotenuse * hypotenuse) < 0.1;
        }

        private static void AreParametersValid(params double[] parameters)
        {
            if (parameters.Any(parameter => parameter < 0))
                throw new ArgumentException("Incorrect parameters");
        }
    }
}

// Запрос для для выбора всех пар «Имя продукта – Имя категории»
// SELECT `Имя продукта`, `Имя категории` FROM `Имя таблицы`;

// Я не был уверен, как именно представлены данные в потенциальной БД,
// предположил, что все продукты и соттветствующие категории находятся в одной таблице
// Если же данные представлены двумя связанными логически таблицами, то примерный запрос будет:
// SELECT `Имя продукта`, `Имя категории`
// FROM `Продукты`, `Категории`
// WHERE CONTAINS (`Категории`.`продукт`, `Имя продукта`);

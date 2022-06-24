using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinQSnippets
{
    public class Snippets
    {
        static public void BasicLinQ()
        {
            string[] cars =
            {
                "VW Golf",
                "VW Jetta",
                "BMW M4",
                "Audi A4"
            };

            //Consultas
            //1. SELECT * of cars
            var carList = from car in cars select car;
            foreach(var car in carList)
            {
                Console.WriteLine(car);
            }

            //2. SELECT WHERE car is Audi
            var audiList = from car in cars where car.Contains("Audi") select car;
            foreach(var car in audiList)
            {
                Console.WriteLine(car);
            }

            /*En la parte de WHERE se le puede aplicar gran número de funciones a la lista con el fin de 
             hacer variedad de llamados SQL
            
                ...where cars.Contains, .CompareTo, .Count ...
             */
        }

        //Ejemplos con Números
        static public void LinQNumbers()
        {
            List<int> numbers = new List<int> { 1,2,3,4,5,6,7,8,9 };

            //Each number multiplied by 3
            //todos menos 9
            //orden ascendente
            var processNumberList = 
                numbers
                // se puede hacer consultas de esta forma o la usada con los strings
                    .Select(num => num * 3)
                    .Where(num => num != 9)
                    .OrderBy(num => num);

        }

        static public void SearchExample()
        {
            List<string> textList = new List<string>
            {
                "a","bx","c","d","e","cj","f","c"
            };

            //1. first element
            var first = textList.First();

            //2.first element que sea "c"
            var ctext = textList.First(text => text.Equals("c"));

            //3. first element que contenga "j"
            var jtext = textList.First(text => text.Contains("j"));

            //4. first element que contenga la "z" o default
            //en caso de no encontrar un elemento no va a regresar vacio sino un elemento default
            var firstOrDefault = textList.FirstOrDefault(text => text.Contains("z"));

            //5. last element que contenga la "z" o default
            //en caso de no encontrar un elemento no va a regresar vacio sino un elemento default
            var lastOrDefault = textList.LastOrDefault(text => text.Contains("z"));

            //6. unique elements
            var uniqueText = textList.Single();
            var uniqueTextOrDefault = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 4 };

            //obtener {4,8}
            var myEvenNumbers = evenNumbers.Except(otherEvenNumbers);
        }

        static public void MultipleSelects()
        {
            //SELECT MANY
            string[] myOpinions =
            {
                "Opinion 1, text 1",
                "Opinion 2, text 2",
                "Opinion 3, text 3",
            };
            var myOpinionsSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id = 1,
                            Name= "Camilo",
                            Email= "camilo@mail.com",
                            Salary= 3000
                        },
                        new Employee()
                        {
                            Id = 2,
                            Name= "Maria",
                            Email= "maria@mail.com",
                            Salary= 8000
                        },
                        new Employee()
                        {
                            Id = 3,
                            Name= "Juan",
                            Email= "juan@mail.com",
                            Salary= 5000
                        },
                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id = 4,
                            Name= "Andres",
                            Email= "andres@mail.com",
                            Salary= 2000
                        },
                        new Employee()
                        {
                            Id = 5,
                            Name= "Mario",
                            Email= "maria@mail.com",
                            Salary= 3000
                        },
                        new Employee()
                        {
                            Id = 6,
                            Name= "Juana",
                            Email= "juana@mail.com",
                            Salary= 9000
                        },
                    }
                }
            };

            //Obtener todos los empleados de todas las empresas
            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            //saber si cualquier lista está vacía 
            bool hasEnterprises = enterprises.Any();

            //saber si hay empleados
            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            //todas las empresas con empleados de al menos 1000 of salary
            bool hasEmployeesWithMoreThan1k =
                enterprises.Any(enterprise => 
                    enterprise.Employees.Any(employee => employee.Salary > 1000)
                );
        }

        static public void LinQCollections()
        {
            var firstList = new List<string>() {"a","b","c" };
            var SecondList = new List<string>() { "a", "d", "c" };

            //INNER JOIN
            var commonResult = firstList.Join(
                    SecondList,
                    element => element,
                    secondElement => secondElement,
                    (element, secondElement) => new { element, secondElement}
                );
            //estos dos son iguales 
            var commonResult2 = from element in firstList
                                join secondElement in SecondList
                                on element equals secondElement
                                select new { element, secondElement };
        }

        static public void SkipTakeLinQ()
        {
            var myList = new[] {1,2,3,4,5,6,7,8,9 };

            var skipFirstTwoValues = myList.Skip(2); // {3,4,5,6,7,8,9}
            var skipLastTwoValues = myList.Skip(2);// {1,2,3,4,5,6,7}
            var skipWhile = myList.SkipWhile(num => num < 4); //{4,5,6,7,8,9}
        }

        //Ejemplo Paginación con Skip y Take
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            int startIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(startIndex).Take(resultsPerPage);
        }

        //Ejemplo sobre el manejo de variables
        static public void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquare = Math.Pow(number, 2)
                               where nSquare > average
                               select number;
            Console.WriteLine("Average: {0}", numbers.Average());

            foreach(int number in aboveAverage)
            {
                //{0} y {1} es para ingresar dentro del string "number" y " Math.Pow(number,2)" respectivamente
                Console.WriteLine("Query: {0} Square: {1}",number, Math.Pow(number,2));
            }
        }

        //ZIP de Linq
        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "one", "two", "three", "four", "five" };

            //{1-"one", 2-"two"}
            //.zip lo que hace es concatenar dos listas de igual longitud
            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + " = " + word);
        }

        //Repeat & Range
        static public void repeatRangeLinq()
        {
            //generate collection from 1 - 1000
            var first1000 = Enumerable.Range(1, 1000);

            //repeat a value n times
            var fiveX = Enumerable.Repeat("X", 5); // {XXXXX}
        }

        //All
        static public void AllLinq()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5 };

            bool areSmallerThanTen = numbers.All(num => num <= 10); //true
        }

        //Aggregate -> es el reduce de JS
        static public void AggregateQueries()
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //sum all numbers
            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);

            string[] words = { "hello", "world,", "name", "is" };
            string greeting = words.Aggregate((prevGreeting, current) => prevGreeting + current);
        }

        //Disctinct
        static public void DisctintQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 3, 4, 1 };
            //retorna los valores únicos 
            IEnumerable<int> disctintValues = numbers.Distinct(); 
        }

        //GroupBy
        static public void GroupByQueries()
        {
            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id = 1,
                            Name= "Camilo",
                            Email= "camilo@mail.com",
                            Salary= 3000
                        },
                        new Employee()
                        {
                            Id = 2,
                            Name= "Maria",
                            Email= "maria@mail.com",
                            Salary= 8000
                        },
                        new Employee()
                        {
                            Id = 3,
                            Name= "Juan",
                            Email= "juan@mail.com",
                            Salary= 5000
                        },
                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id = 4,
                            Name= "Andres",
                            Email= "andres@mail.com",
                            Salary= 2000
                        },
                        new Employee()
                        {
                            Id = 5,
                            Name= "Mario",
                            Email= "maria@mail.com",
                            Salary= 3000
                        },
                        new Employee()
                        {
                            Id = 6,
                            Name= "Juana",
                            Email= "juana@mail.com",
                            Salary= 9000
                        },
                    }
                }
            };

            var salaryGreaterThan2 = enterprises.GroupBy(enterprise => enterprise.Employees.GroupBy(employee => employee.Salary >2000));
        }


        //relationshipsLinq
        static public void relationshipsLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post
                {
                    Id=1,
                    Title= "My First Post",
                    Content = "My First Content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment
                        {
                            Id=1,
                            Created = DateTime.Now,
                            Title = "My First Comment",
                            Content = "My Content"
                        },
                        new Comment
                        {
                            Id=2,
                            Created = DateTime.Now,
                            Title = "My Second Comment",
                            Content = "My Content"
                        },
                    } 
                },
                new Post
                {
                    Id=2,
                    Title= "My Second Post",
                    Content = "My Second Content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment
                        {
                            Id=3,
                            Created = DateTime.Now,
                            Title = "My Third Comment",
                            Content = "My Content"
                        },
                        new Comment
                        {
                            Id=4,
                            Created = DateTime.Now,
                            Title = "My Fourth Comment",
                            Content = "My Content"
                        },
                    }
                }
            };
            //dentro de un SelectMany se le puede pasar de parámetro otra arrow function
            var commentsContent = posts.SelectMany(
                post => post.Comments, 
                    (post, comment) => new { PostId = post.Id, Comment = comment.Content});

        }
    }
}
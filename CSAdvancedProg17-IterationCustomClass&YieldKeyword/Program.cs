using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace CSAdvancedProg17_IterationCustomClass_YieldKeyword
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Iteration
            //In Iteration Loops, each step has a relationship with the one right before itself.
            //Therefore while and for loops can't be associated with iteration whereas foreach follows its concept.
            //This is exactly why if a collection's or array's element order or number change during the loop execution, an error would occur. 
            //this order or number change would't cause any errors in the same way in for&while loops even though the relevant array's||collection's element limit..
            //.. occupies in the condition part of these loops.
            /*List<int> expList1 = new List<int>() {1,2,3,4,5 };
            for (int i = 0; i < expList1.Count; i++) { expList1.Remove(3); }*/ //will not cause an error here
                                                                               //whereas in foreach, we will encounter an error.
                                                                               //foreach (int i in expList1) {expList1.Add(4);}

            // IEnumerable - IEnumerator
            //IEnumerable: is the interface bringing the GetEnumerator method which features iteration.
            //IEnumerator: is the interface allowing us to modify what will happen at each iteration step.

            //How to qualify a class as iterable?
            //From C#'s aspect, a class must have a method named GetEnumerator (its signature is right below) to be regarded iterable and to be able to used in..
            //.. foreach statements.
            //public IEnumerator GetEnumerator(){return *sthImplementingIEnumerator;}
            //A generic exp usage of IEnumerable Implementation
            /*Stock1 stock1 = new Stock1();
            foreach (string stock in stock1)
                Console.WriteLine(stock);
            foreach (object stock in stock1) //As you can see, the second obligatory method allows us to work with the elements as objects.
                Console.WriteLine(stock);*/

            //IEnumerator Implementation
            /*Stock2 strings = new Stock2();
            foreach (string s in strings) 
            Console.WriteLine(s);*/
            #endregion


            #region Yield Keyword
            //Before starting to cover yield keyword's role, I would like to revise a feature of foreach statement.
            //foreach statement can take a method as the second argument which returns sth IEnumerable.
            foreach (var VARIABLE in ProperLocalMethod1()) //As you can see, it takes the IEnumerable type the local method..
            //.. returns and iterate it.
                Console.WriteLine(VARIABLE);
            IEnumerable<string> ProperLocalMethod1()
            {
                List<string> list1 = new List<string>(){"First Element","Second Element","Third Element"};
                return list1;
            }
            //Yield keyword steps in exactly at this point
            foreach (var VARIABLE in ProperLocalMethod2())
                Console.WriteLine(VARIABLE);
            IEnumerable<string> ProperLocalMethod2()
            {
                //Instead of declaring a list here which will have an allocated space for its elements in the heap..
                //.. , we can utilize from yield keyword with the return keyword to indicate what it will return for..
                //.. the foreach statement's each step
                yield return "First Element"; //Compiler will know where it left at each step.
                //It is also possible to perform some operations between yield steps;
                Console.WriteLine("Intermediate operation.");
                yield return "Second Element";
                yield return "Third Element";
                //Since there is no allocated space in the RAM for anything, this is counted more efficient compared to the first local method.
                
                //In the iteration process, the data not provided via yield keyword is fast-executed (you can't perform intermediate operations between each..
                //.. step for an instance.) whereas yield keyword is associated with lazy execution. That's why the keyword is the preferred in asynchronous..
                //.. programming too.
                //Yield keyword is also used with the break keyword as well to stop iteration at a specific point you determine.
                //You need to simply type yield break;
                //Here is a great usage of yield keyword where the list elements returned is pretty crowded.
                IEnumerable<string> ProperLocalMethod3()
                {
                    String[] array1 = { "First Element", "Second Element", "Third Element", "Fourth Element", "Fifth Element", "Sixth Element" };
                    foreach (var VARIABLE in array1)
                    {
                        yield return VARIABLE;
                    }
                }
            }
            #endregion
        }
    }

    #region Iteration
    class Stock1 : IEnumerable<string> 
    {
        List<string> list1 = new() {"Efe", "Ozgur","KILIC" };
        public IEnumerator<string> GetEnumerator() 
        {
            return list1.GetEnumerator();  
        }

        IEnumerator IEnumerable.GetEnumerator() //The generic ver. makes it imperative to declare this method as well to allow devs to take the elements of the list..
                                                //as objects.
        {
            return list1.GetEnumerator();
        }
    }

    //IEnumerator Implementation
    class Stock2 : IEnumerable<string> 
    {
        List<string> list1 = new() { "Efe", "Ozgur", "KILIC" };
        public IEnumerator<string> GetEnumerator()
        {
            //return list1.GetEnumerator(); //Instead, now we can return our specific IEnumerator type
            return new StockEnumerator(list1);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new StockEnumerator(list1);
        }
    }

    class StockEnumerator : IEnumerator<string>
    {
        List<string> _source;
        int _currentIndex = -1;
        public StockEnumerator(List<string> source) { _source = source; }
        public string Current { get { return _source[_currentIndex]; } }
        object IEnumerator.Current { get { return _source[_currentIndex]; } }

        public void Dispose()
        {
            _source = null;
        }

        public bool MoveNext()
        {
            
            return ++_currentIndex < _source.Count;
        }

        public void Reset()
        {
            _currentIndex = -1;
        }
    }
    #endregion


    #region Yield Keyword
    #endregion
}

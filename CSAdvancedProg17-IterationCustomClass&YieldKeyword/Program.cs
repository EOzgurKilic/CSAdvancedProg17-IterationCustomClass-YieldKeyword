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

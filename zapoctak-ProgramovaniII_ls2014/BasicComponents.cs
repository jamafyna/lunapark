using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace zapoctak_ProgramovaniII_ls2014
{
    public class MyDebugException : Exception {
        public MyDebugException():base() { }
        public MyDebugException(string s) : base(s) { }
    }
    public interface MapObjects {
        /// <summary>
        /// Create an instance and show it in the game map
        /// </summary>
        /// <param name="x">the left coordinate</param>
        /// <param name="y">the top coordinate</param>
        void Create(int x, int y); //todo mozna system.drawing.point
        /// <summary>
        /// user action
        /// </summary>
        void Click();
        /// <summary>
        /// user action
        /// </summary>
        void Demolish();
     
    }

    /// <summary>
    /// represents a square in the game map
    /// </summary>
     #warning mozna uplne k nicemu!
    abstract class Square : MapObjects 
    {
        public abstract void Create(int x, int y);      
        public abstract void Click();
        public abstract void Demolish();
    }

    public struct Coordinates
    { //todo: Zamyslet se, zda nepouzit System.Drawing.Point
        public int x ;
        public int y ;
        public Coordinates(int x, int y) { this.x = x; this.y = y; }
    }
       
    public abstract class Amusements: MapObjects
    {
        public int id{get;private set;}
        public Coordinates entrance {get; protected set; } //todo: je opravdu potreba protected, nestaci private nebo dokonce readonly?
        public Coordinates exit { get; protected set; }
        public int capacity{get;protected set;}
        protected Queue<Clovek> queue;
        public int waitingPeopleCount {
            get { 
                if (queue != null) return queue.Count; 
                else throw new MyDebugException("null v Amusements-queue. Opravdu ma tak byt?");
                }
            private set{}
        }

        public abstract void Create(int x, int y);
        public virtual void Demolish() { }
        public virtual void Click() { }
#warning opravdu abstract Action? Nemela by byt virtual a rovnou naimplementovana?
        public abstract void Action();
        public void ChangeId(int id) { this.id = id; }
         

        /// <summary>
        /// create an Item in AtrakceForm and set it (e.g. set visible=false)
        /// </summary>
      //  public abstract static void Initialize();
        
        
    }
    public abstract class SquareAmusements : Amusements { }
   /// <summary>
   /// Class for rectangle, not square, amusements. It can have a different orientation.
   /// </summary>
    public abstract class RectangleAmusements : Amusements { }
    /// <summary>
    /// list of all amusements, at position i is an amusement with id==i
    /// </summary>
    public class ListOfAmusements { //todo: nejspis by mela byt thread-safe
        private List<Amusements> list;
#warning overit, ze v countOfActiveAmus je opravdu spravne
        public int count { get { return list.Count; } private set { } }
        public ListOfAmusements() {
            list = new List<Amusements>();       
        }
        public void Add(Amusements a) {
            if (a.id == list.Count) list.Add(a);
            else throw new Exception("nesedi id a count v ListOfAmusements-Add()"); //todo: nemelo by se stavat, protoze by vzdy melo jit vytvorit jen jednu atrakci
        }
        public int ReturnFreeID() {
            return list.Count;
        }
        public void Remove(Amusements a) { 
            Amusements b=list[list.Count-1];
            b.ChangeId(a.id);
            list[a.id] = b;
            list.RemoveAt(list.Count-1);
        }
        public void ForeachAction() {
            foreach (Amusements a in list)
            {
                a.Action();
            }
        }
          
    }
    public class Person : MapObjects { //todo: Mozna sealed a nebo naopak moznost rozsiritelnosti dal...
        public void Click() { 
            throw new NotImplementedException();
        }
        public void Create(int x, int y) {
            throw new NotImplementedException();
        }
        public void Demolish() {
            throw new NotImplementedException();
        }
        public void Action() {
            throw new NotImplementedException();
        }
    
    
    }


}

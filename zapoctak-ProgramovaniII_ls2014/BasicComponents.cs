using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace zapoctak_ProgramovaniII_ls2014
{
    public interface MapObjects {
        /// <summary>
        /// Create an instance and show it in the game map
        /// </summary>
        /// <param name="x">the left coordinate</param>
        /// <param name="y">the top coordinate</param>
        void Create(int x, int y);
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
        public Coordinates entrance {get; protected set; } //todo: je opravdu potreba protected, nestaci private nebo dokonce readonly?
        public Coordinates exit { get; protected set; }
        public int capacity{get;protected set;}
        protected Queue<Clovek> queue;
        
        
        public abstract void Create(int x, int y);
        public virtual void Demolish() { }
        public virtual void Click() { }
        public abstract void Action();
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

}

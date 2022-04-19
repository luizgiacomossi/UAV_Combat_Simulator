using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
   public abstract class BTree : MonoBehaviour
   {

       private Node _root = null; // root node of the tree
       public string currentNode; // current node of the tree

       // constructor 
       protected virtual void Start(){
           _root = SetupTree();
        }

       protected virtual void FixedUpdate(){
           if(_root != null){
               _root.Run();
           }
            

        }
        
        // setup a tree
        protected abstract Node SetupTree();

        protected void SetName(string name){
            currentNode = name;
        }
     
    }
}

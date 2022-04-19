using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    // behaves like a AND logic gate
    public class Selector : Node // derive from node class
    {
            public Selector() : base() { } // constructor
            public Selector(List<Node> children) : base(children) { } // constructor
        

            public override NodeState Run(){
                // for each child node in the Selector
                foreach (Node child in children){
                    // run the child node
                    NodeState childState = child.Run();
                    // if any child is running, return running
                    switch (child.Run()){
                        case NodeState.FAILURE:
                            continue;
                        case NodeState.SUCCESS:
                            state = NodeState.SUCCESS;
                            return state;
                        case NodeState.RUNNING:
                            state = NodeState.RUNNING;
                            return state;
                        default:
                            continue;
                    }
                }
                state = NodeState.FAILURE;
                return state;
            }


    }
}
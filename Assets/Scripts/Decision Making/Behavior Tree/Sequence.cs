using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    // behaves like a AND logic gate
    public class Sequence : Node // derive from node class
    {
            public Sequence() : base() { } // constructor
            public Sequence(List<Node> children) : base(children) { } // constructor
        

            public override NodeState Run(){
                bool anyChildRunning = false;
                // for each child node in the sequence
                foreach (Node child in children){
                    // run the child node
                    NodeState childState = child.Run();
                    // if any child is running, return running
                    switch (child.Run()){
                        case NodeState.FAILURE:
                            state = NodeState.FAILURE;
                            return state;
                        case NodeState.SUCCESS:
                            continue;
                        case NodeState.RUNNING:
                            anyChildRunning = true;
                            continue;
                        default:
                            state = NodeState.SUCCESS;
                            return state;
                    }
                }

                state = anyChildRunning ? NodeState.RUNNING : NodeState.SUCCESS;
                return state;
            }


    }

}




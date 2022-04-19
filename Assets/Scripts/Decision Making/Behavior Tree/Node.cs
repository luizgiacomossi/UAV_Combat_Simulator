using System.Collections;
using System.Collections.Generic;

namespace BehaviorTree
{

    public enum NodeState{
        SUCCESS,
        FAILURE,
        RUNNING
    }

    public class Node{

        // classes derived from Node can access and modify these variables
        protected NodeState state;

        public Node parent;
        public string nodeName;
        protected List<Node> children;

        private Dictionary<string, object> _dataContent = new Dictionary<string, object>();

        // constructor default, by default the node name is "Node"
        public Node( string name = "Node" ){
            parent = null;
            nodeName = name;
        }
    
        public Node(List<Node> children){
            foreach (Node child in children)
                AddChild(child);
        }

        private void AddChild(Node child){
            child.parent = this;
            children.Add(child);
        }

        // prototype of evaluate function - virtual: can be overridden by derived classes
        public virtual NodeState Run() =>  NodeState.FAILURE;

        public void SetName(string name){
            nodeName = name;
        }


        // useful functions for children to access data using dictionary
        public void setData(string key, object value){
            _dataContent.Add(key, value);
        }

        public object GetData(string key){

            object value = null;
            if(_dataContent.TryGetValue(key, out value)){
                return value;
            }

            Node node = parent;
            while(node != null)
            {
                value = node.GetData(key);
                if(value != null){
                    return value;
                }
                node = node.parent;
    
            }
            return null;
        }

        public bool ClearData(string key){
            if(_dataContent.ContainsKey(key)){
                _dataContent.Remove(key);
                return true;
            }

            Node node = parent;
            while(node != null){
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }

    }
    
}

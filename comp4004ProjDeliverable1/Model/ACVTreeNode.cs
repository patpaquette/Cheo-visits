using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace comp4004ProjDeliverable1.Model
{
    class ACVTreeNode
    {
        private Visit _visit;
        private ACVTreeNode _parent;
        private List<ACVTreeNode> _children;

        public List<ACVTreeNode> Children
        {
            get
            {
                return this._children;
            }
        }

        public Visit Visit
        {
            get
            {
                return this._visit;
            }
        }

        public ACVTreeNode(Visit visit, ACVTreeNode parent)
        {
            this._children = new List<ACVTreeNode>();
            this._parent = parent;
            this._visit = visit;
        }

        public void addChild(ACVTreeNode child)
        {
            this._children.Add(child);
        }

        public override string ToString()
        {
            try
            {
                return this._visit.ToString();
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}

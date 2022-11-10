using Login_Module;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace Login_Module
{
     public class BinaryTree
    {
        private Node Root { get; set; }
     
        public bool Add(Staff_Login temp)
        {
            Node before = null, after = this.Root;
            
            while (after != null)
            {
                before = after;
                if (temp.getUserName().CompareTo(after.Account.getUserName()) < 0) //Is new node in left tree?  
                    after = after.LeftNode;
                else if (temp.getUserName().CompareTo(after.Account.getUserName()) > 0) //Is new node in right tree?
                    after = after.RightNode;
                else
                {
                    //Exist same value
                    return false;
                }
            }

            Node newNode = new Node();
            newNode.Account.setUserName(temp.getUserName()) ;

            if (this.Root == null)//Tree ise empty
                this.Root = newNode;
            else
            {
                if (temp.getUserName().CompareTo(before.Account.getUserName()) < 0)
                    before.LeftNode = newNode;
                else
                    before.RightNode = newNode;
            }

            return true;
        }
       
            Node Find(string username)
        {
            return this.Find(username, this.Root);
        }

        public void Remove(string value)
        {
            this.Root = Remove(this.Root, value);
        }

        private Node Remove(Node parent, string key)
        {
            if (parent == null) return parent;

            if (key.CompareTo(parent.Account.getUserName()) < 0) parent.LeftNode = Remove(parent.LeftNode, key);
            else if (key.CompareTo(parent.Account.getUserName()) > 0)
                parent.RightNode = Remove(parent.RightNode, key);

            // if value is same as parent's value, then this is the node to be deleted  
            else
            {
                // node with only one child or no child  
                if (parent.LeftNode == null)
                    return parent.RightNode;
                else if (parent.RightNode == null)
                    return parent.LeftNode;

                // node with two children: Get the inorder successor (smallest in the right subtree)  
                parent.Account.setUserName(MinValue(parent.RightNode));

                // Delete the inorder successor  
                parent.RightNode = Remove(parent.RightNode, parent.Account.getUserName());
            }

            return parent;
        }

        private string MinValue(Node node)
        {
            string minv = node.Account.getUserName();

            while (node.LeftNode != null)
            {
                minv = node.LeftNode.Account.getUserName();
                node = node.LeftNode;
            }

            return minv;
        }

        private Node Find( string username, Node parent)
        {
            if (parent != null)
            {
                if (username.CompareTo(parent.Account.getUserName()) == 0) return parent;
                if (username.CompareTo(parent.Account.getUserName()) < 0)
                    return Find(username, parent.LeftNode);
                else
                    return Find(username, parent.RightNode);
            }

            return null;
        }

        public int GetTreeDepth()
        {
            return this.GetTreeDepth(this.Root);
        }

        private int GetTreeDepth(Node parent)
        {
            return parent == null ? 0 : Math.Max(GetTreeDepth(parent.LeftNode), GetTreeDepth(parent.RightNode)) + 1;
        }

        public void TraverseInOrder(Node parent)
        {
            if (parent != null)
            {
                TraverseInOrder(parent.LeftNode);
                Console.Write(parent.Account.getUserName() + " ");
                Console.Write(parent.Account.getPassword() + " ");
                TraverseInOrder(parent.RightNode);
            }
        }

        //public void TraversePostOrder(Node parent)
        //{
        //    if (parent != null)
        //    {
        //        TraversePostOrder(parent.LeftNode);
        //        TraversePostOrder(parent.RightNode);
        //        Console.Write(parent.Account.getUserName() + " ");
        //        Console.Write(parent.Account.getPassword() + " ");
        //    }
        //}

        //public Node FindNode(Node node, string s)
        //{
        //    node = this.Root;
        //    if (node == null)
        //        return n;
        //    else if (s.CompareTo(node.Account.getUserName()) < 0)
        //        return FindNode(node.LeftNode, s);
        //    else if (s.CompareTo(node.Account.getUserName()) > 0)
        //        return FindNode(node.RightNode, s);
        //    else
        //    {
        //        return node;
        //    }

        //public void TraversePreOrder(Node parent)
        //{
        //    if (parent != null)
        //    {
        //        Console.Write(parent.Account.getUserName() + " ");
        //        Console.Write(parent.Account.getPassword() + " ");
        //        TraversePreOrder(parent.LeftNode);
        //        TraversePreOrder(parent.RightNode);
        //    }
        //}

    }
}
#region Usings
using Novetus.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
#endregion

#region Tree Node Helper
public static class TreeNodeHelper
{
    public static void ListDirectory(TreeView treeView, string path, string[] filter)
    {
        treeView.Nodes.Clear();
        var rootDirectoryInfo = new DirectoryInfo(path);
        treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo, filter));
    }

    public static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo, string[] filter)
    {
        var directoryNode = new TreeNode(directoryInfo.Name);
        foreach (var directory in directoryInfo.GetDirectories())
            directoryNode.Nodes.Add(CreateDirectoryNode(directory, filter));
        foreach (var file in directoryInfo.GetFilesByExtensions(filter))
            directoryNode.Nodes.Add(new TreeNode(file.Name));
        return directoryNode;
    }

    //https://stackoverflow.com/questions/42295131/searching-a-treeview-for-a-specific-string
    public static TreeNode SearchTreeView(string p_sSearchTerm, TreeNodeCollection p_Nodes)
    {
        foreach (TreeNode node in p_Nodes)
        {
            if (node.Text == p_sSearchTerm)
                return node;

            if (node.Nodes.Count > 0)
            {
                TreeNode child = SearchTreeView(p_sSearchTerm, node.Nodes);
                if (child != null)
                {
                    return child;
                }
            }
        }

        return null;
    }

    public static TreeNode GetNodeByFullPath(string fullPath, TreeNodeCollection p_Nodes)
    {
        string[] pathStrings = Path.GetDirectoryName(fullPath).Split('\\');
        TreeNode node = null;

        foreach (string dir in pathStrings)
        {
            node = SearchTreeView(dir, p_Nodes);
        }

        if (node != null)
        {
            string fileName = new DirectoryInfo(fullPath).Name;
            return SearchTreeView(fileName, node.Nodes);
        }

        return null;
    }

    public static void CopyNodes(TreeNodeCollection oldcollection, TreeNodeCollection newcollection)
    {
        foreach (TreeNode node in oldcollection)
        {
            newcollection.Add((TreeNode)node.Clone());
        }
    }
}
#endregion

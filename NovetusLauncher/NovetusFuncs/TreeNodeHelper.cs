/*
 * Created by SharpDevelop.
 * User: Bitl
 * Date: 10/10/2019
 * Time: 7:03 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Collections;

public static class TreeNodeHelper
{
	public static void ListDirectory(TreeView treeView, string path, string filter = ".*")
	{
		treeView.Nodes.Clear();
		var rootDirectoryInfo = new DirectoryInfo(path);
		treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo, filter));
	}

    public static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo, string filter = ".*")
    {
        var directoryNode = new TreeNode(directoryInfo.Name);
        foreach (var directory in directoryInfo.GetDirectories())
            directoryNode.Nodes.Add(CreateDirectoryNode(directory, filter));
        foreach (var file in directoryInfo.GetFiles("*"+filter))
            directoryNode.Nodes.Add(new TreeNode(file.Name));
        return directoryNode;
	}

    //https://stackoverflow.com/questions/42295131/searching-a-treeview-for-a-specific-string
    public static TreeNode SearchTreeView(string p_sSearchTerm, TreeNodeCollection p_Nodes)
	{
		foreach (TreeNode node in p_Nodes) {
			if (node.Text == p_sSearchTerm)
				return node;

			if (node.Nodes.Count > 0) {
				TreeNode child = SearchTreeView(p_sSearchTerm, node.Nodes);
                if (child != null)
                {
                    return child;
                }
			}
		}

		return null;
	}

    public static string GetFolderNameFromPrefix(string source, string seperator = " -")
	{
		try {
			string result = source.Substring(0, source.IndexOf(seperator));
				
			if (Directory.Exists(GlobalVars.MapsDir + @"\\" + result)) {
				return result + @"\\";
			} else {
				return "";
			}
		} catch (Exception) when (!Env.Debugging) {
			return "";
		}
	}
		
	public static void CopyNodes(TreeNodeCollection oldcollection, TreeNodeCollection newcollection)
	{
		foreach (TreeNode node in oldcollection) {
			newcollection.Add((TreeNode)node.Clone());
		}
	}
		
	public static List<TreeNode> GetAllNodes(this TreeView _self)
	{
		List<TreeNode> result = new List<TreeNode>();
		foreach (TreeNode child in _self.Nodes) {
			result.AddRange(child.GetAllNodes());
		}
		return result;
	}

	public static List<TreeNode> GetAllNodes(this TreeNode _self)
	{
		List<TreeNode> result = new List<TreeNode>();
		result.Add(_self);
		foreach (TreeNode child in _self.Nodes) {
			result.AddRange(child.GetAllNodes());
		}
		return result;
	}

    public static List<TreeNode> Ancestors(this TreeNode node)
    {
        return AncestorsInternal(node).Reverse().ToList();
    }
    public static List<TreeNode> AncestorsAndSelf(this TreeNode node)
    {
        return AncestorsInternal(node, true).Reverse().ToList();
    }
    private static IEnumerable<TreeNode> AncestorsInternal(TreeNode node, bool self = false)
    {
        if (self)
            yield return node;
        while (node.Parent != null)
        {
            node = node.Parent;
            yield return node;
        }
    }
}
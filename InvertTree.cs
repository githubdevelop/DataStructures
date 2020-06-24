/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
// Assumptions: null can be passed. If so null will be returned.

// Idea is that given a node swap its left and right nodes and then traverse to left and then 
// to right recursively

public class Solution {
    public TreeNode InvertTree(TreeNode root) {
        // Check if head is null
        if(root == null)
            return root;
        invert(root);
        return root;
        
    }
    
    // Big (O) - Space complexity - We are not using new values except for temp variabel.
    //since we are swapping in place
    // Big (o) - Time complexity - O(n) for n nodes since we have to visit every nodes.
    
    public void invert(TreeNode node)
    {
        if(node == null)
            return;
        var tmp = node.left;
        node.left = node.right;
        node.right = tmp;
        invert(node.left);
        invert(node.right);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure_Algorithms
{
    public class SimilarString
    {

        public int NumSimilarGroups(string[] A)
        {

            List<List<string>> groups = new List<List<string>>();
            foreach (var x in A)
            {
                AddToCorrespondingGroup(x, ref groups);
            }
            
            return groups.Count;
        }

        private void AddToCorrespondingGroup(string toCheck, ref List<List<string>> groups)
        {
            foreach (var group in groups)
            {
                if (BelongsToGroup(toCheck, group))
                {
                    group.Add(toCheck);
                    return;
                }
            }
            // This means does not belong to any group. So add to a new group.
            groups.Add(new List<string> { toCheck });
        }

        private bool BelongsToGroup(string toCheck, List<string> group)
        {
            foreach (var val in group)
            {
                if (IsSimilar(val, toCheck))
                    return true;
            }
            return false;
        }

        private bool IsSimilar(string x, string y)
        {
            if (x.Equals(y))
                return true;

            for (int i = 0; i < x.Length; i++)
            {
                int newPos = i;
                while (++newPos < x.Length)
                {
                    var newString = GetNewCombination(x, i, newPos);
                    if (newString.Equals(y))
                        return true;
                }
            }
            return false;
        }

        private string GetNewCombination(string toChange, int startPos, int positionToSwap)
        {
            var tempHolder = toChange.ToCharArray();
            var temp = tempHolder[startPos];
            tempHolder[startPos] = tempHolder[positionToSwap];
            tempHolder[positionToSwap] = temp;
            string result = new string(tempHolder);
            return result;
        }
    }
}

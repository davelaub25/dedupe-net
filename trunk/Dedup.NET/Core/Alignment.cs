using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Enum;

namespace DedupeNET.Core
{
    public class Alignment
    {
        private List<EditOperation> alignment;

        private int matches;
        public int Matches
        {
            get
            {
                return matches;
            }
        }

        private int nonMatches;
        public int NonMatches
        {
            get
            {
                return nonMatches;
            }
        }
        
        private int insertions;
        public int Insertions
        {
            get
            {
                return insertions;
            }
        }

        private int deletions;
        public int Deletions
        {
            get
            {
                return deletions;
            }
        } 


        public int Length
        {
            get
            {
                return alignment.Count;
            }
        }
        
        public Alignment()
        {
            alignment = new List<EditOperation>();
        }

        public void Append(EditOperation operation)
        {
            alignment.Add(operation);
            IncreaseCounters(operation);
        }

        public void Prepend(EditOperation operation)
        {
            alignment.Insert(0, operation);
            IncreaseCounters(operation);
        }

        public void Insert(int index, EditOperation operation)
        {
            alignment.Insert(index, operation);
            IncreaseCounters(operation);
        }

        private void IncreaseCounters(EditOperation operation)
        {
            if (operation.A == (char)CharEnum.Empty)
            {
                deletions++;
            }
            else if (operation.B == (char)CharEnum.Empty)
            {
                insertions++;
            }
            else if (operation.A == operation.B)
            {
                matches++;
            }
            else
            {
                nonMatches++;
            }
        }
    }
}

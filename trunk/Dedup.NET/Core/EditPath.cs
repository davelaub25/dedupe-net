using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Enum;

namespace DedupeNET.Core
{
    public class EditPath
    {
        public List<EditOperation> editPath;

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
                return editPath.Count;
            }
        }
        
        public EditPath()
        {
            editPath = new List<EditOperation>();
        }

        public void Append(EditOperation operation)
        {
            editPath.Add(operation);
            IncreaseCounters(operation);
        }

        public void Prepend(EditOperation operation)
        {
            editPath.Insert(0, operation);
            IncreaseCounters(operation);
        }

        public void Insert(int index, EditOperation operation)
        {
            editPath.Insert(index, operation);
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

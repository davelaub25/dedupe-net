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

        private int _matches;
        public int Matches
        {
            get { return _matches; }
        }

        private int _nonMatches;
        public int NonMatches
        {
            get { return _nonMatches; }
        }

        private int _insertions;
        public int Insertions
        {
            get { return _insertions; }
        }

        private int _deletions;
        public int Deletions
        {
            get { return _deletions; }
        }


        public int Length
        {
            get { return alignment.Count; }
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
                _deletions++;
            }
            else if (operation.B == (char)CharEnum.Empty)
            {
                _insertions++;
            }
            else if (operation.A == operation.B)
            {
                _matches++;
            }
            else
            {
                _nonMatches++;
            }
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel;

namespace ClassSRM
{
    internal class DBAppointmentList : List<DBAppointment>, IBindingList
    {
        #region IBindingList Members

        public event ListChangedEventHandler ListChanged;

        public void AddIndex(PropertyDescriptor property)
        {
        }

        public object AddNew()
        {
            DBAppointment apt = new DBAppointment();
            this.Add(apt);
            return apt;
        }

        public bool AllowEdit
        {
            get { return true; }
        }

        public bool AllowNew
        {
            get { return true; }
        }

        public bool AllowRemove
        {
            get { return true; }
        }

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
        }

        public int Find(PropertyDescriptor property, object key)
        {
            return -1;
        }

        public bool IsSorted
        {
            get { return false; }
        }

        public void RemoveIndex(PropertyDescriptor property)
        {
        }

        public void RemoveSort()
        {
        }

        public ListSortDirection SortDirection
        {
            get { return ListSortDirection.Ascending; }
        }

        public PropertyDescriptor SortProperty
        {
            get { return null; }
        }

        public bool SupportsChangeNotification
        {
            get { return false; }
        }

        public bool SupportsSearching
        {
            get { return false; }
        }

        public bool SupportsSorting
        {
            get { return false; }
        }

        #endregion IBindingList Members
    }
}
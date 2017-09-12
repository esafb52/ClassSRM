using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;

namespace ClassSRM.Forms
{
    public partial class Shedule : DevExpress.XtraEditors.XtraForm
    {
        ClassSRMDataContext context;
        public Shedule()
        {
            InitializeComponent();
        }

        private void Shedule_Load(object sender, EventArgs e)
        {
            context = new ClassSRMDataContext();
            DBAppointmentList apts = new DBAppointmentList();
            apts.AddRange(context.DBAppointments.ToArray());

            List<DBResource> resources = new List<DBResource>();
            resources.AddRange(context.DBResources.ToArray());

            AppointmentStorage aptStorage = schedulerStorage1.Appointments;
            ResourceStorage resStorage = schedulerStorage1.Resources;

            aptStorage.Mappings.AllDay = "AllDay";
            aptStorage.Mappings.Description = "Description";
            aptStorage.Mappings.End = "EndTime";
            aptStorage.Mappings.Label = "Label";
            aptStorage.Mappings.Location = "Location";
            aptStorage.Mappings.RecurrenceInfo = "RecurrenceInfo";
            aptStorage.Mappings.ReminderInfo = "ReminderInfo";
            aptStorage.Mappings.ResourceId = "CarIdShared";
            aptStorage.Mappings.Start = "StartTime";
            aptStorage.Mappings.Status = "Status";
            aptStorage.Mappings.Subject = "Subject";
            aptStorage.Mappings.Type = "EventType";

            resStorage.Mappings.Id = "ID";
            resStorage.Mappings.Caption = "Model";
            resStorage.Mappings.Image = "Picture";

            resStorage.DataSource = resources;
            aptStorage.DataSource = apts;
        }
        private void schedulerStorage1_AppointmentsInserted(object sender, PersistentObjectsEventArgs e)
        {
            foreach (Appointment apt in e.Objects)
            {
                DBAppointment dbApt = (DBAppointment)apt.GetSourceObject(schedulerStorage1);
                context.DBAppointments.InsertOnSubmit(dbApt);
                context.SubmitChanges();
            }
        }

        private void schedulerStorage1_AppointmentsChanged(object sender, PersistentObjectsEventArgs e)
        {
            context.SubmitChanges();
        }

        private void schedulerStorage1_AppointmentDeleting(object sender, PersistentObjectCancelEventArgs e)
        {
            Appointment apt = (Appointment)e.Object;
            DBAppointment dbApt = (DBAppointment)apt.GetSourceObject(schedulerStorage1);
            context.DBAppointments.DeleteOnSubmit(dbApt);
            context.SubmitChanges();
        }
        class DBAppointmentList : List<DBAppointment>, IBindingList
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
            #endregion
        }

    }
}
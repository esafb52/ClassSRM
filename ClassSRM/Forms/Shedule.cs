
/****************************** ghost1372.github.io ******************************\
*	Module Name:	Shedule.cs
*	Project:		ClassSRM
*	Copyright (C) 2017 Mahdi Hosseini, All rights reserved.
*	This software may be modified and distributed under the terms of the MIT license.  See LICENSE file for details.
*
*	Written by Mahdi Hosseini <Mahdidvb72@gmail.com>,  2017, 9, 12, 02:17 ب.ظ
*	
***********************************************************************************/

using DevExpress.XtraScheduler;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassSRM.Forms
{
    public partial class Shedule : DevExpress.XtraEditors.XtraForm
    {
        private ClassSRMDataContext context;

        public Shedule()
        {
            InitializeComponent();
        }

        private void Shedule_Load(object sender, EventArgs e)
        {
            context = new ClassSRMDataContext(Config.connection);
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
            schedulerControl1.GoToToday();
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
    }
}
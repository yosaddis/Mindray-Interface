using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BasicClassLibrary
{
    public static class NotificationAccess
    {
        public const string LABORATORY = "ShowLaboratoryNotification";
        public const string IMAGING = "ShowImagingNotification";
        public const string VISIT = "ShowVisitNotification";
        public const string IMAGING_TEST_APPROVAL = "ShowImagingTestApprovalNotification";
        public const string LABORATORY_TEST_APPROVAL = "ShowLaboratoryTestApprovalNotification";
        public const string IMAGING_COLONOSCOPY_ORDER = "ShowColonoscopyNotification";
        public const string IMAGING_CT_SCAN_ORDER = "ShowCtscanNotification";
        public const string IMAGING_ECG_ORDER = "ShowEcgNotification";
        public const string IMAGING_ECHO_ORDER = "ShowEchoNotification";
        public const string IMAGING_ENDOSCOPY_ORDER = "ShowEndoscopyNotification";
        public const string IMAGING_ULTRASOUND_ORDER = "ShowUltrasoundNotification";
        public const string IMAGING_X_RAY_ORDER = "ShowXrayNotification";
        public const string IMAGING_MAMMOGRAPHY_ORDER = "ShowMamoNotification";
        public const string IMAGING_PROCEDURE_ORDER = "ShowProcedureNotification";
        public const string NEW_ADMISSION_REQUEST = "ShowNewAdmissionNotification";
        public const string NEW_PATIENT_REGISTERED = "ShowNewPatientRegisteredNotification";
        public const string NEW_PATIENT_DISCHARGED = "ShowNewPatientDischargedNotification";
        public const string NEW_PATIENT_ASSIGNED_FROM_TRIAGE = "ShowPatientAssignedFromTriageNotification";
        public const string NEW_LABORATORY_ORDER_CREATED = "ShowNewLaboraotryOrderNotification";
        public const string NEW_IMAGING_ORDER_CREATED = "ShowNewImagingOrderNotification";
        public const string NEW_PRESCRIPTION_CREATED = "ShowNewPrescriptionNotification";
        public const string NEW_PRESCRIPTION_SALES_CREATED = "ShowNewPrescriptionSalesNotification";
        public const string NEW_OTHER_SERVICE_ORDER_CREATED = "ShowNewOtherSerivceOrderNotification";
        public const string NEW_EXTRA_MEAL_ORDER_CREATED = "ShowExtraMealNotification";
        public const string NEW_CONSULTATION_ORDER_CREATED = "ShowConsultationNotification";
        public const string NEW_OXYGEN_ORDER_CREATED = "ShowOxygenNotification";
        public const string NEAR_EXPIRY = "ShowExpiredItemsNotification";
        public const string EXPIRED_ITEM = "ShowNearExpiryItemsNotification";
        public const string NEW_MEDICATION = "ShowNewMedicationNotification";
        public const string STOCK_OUT = "ShowStockoutNotification";
    }
    public static class NotificationType
    {
        public const string LABORATORY = "LABORATORY";
        public const string IMAGING = "IMAGING";
        public const string VISIT = "VISIT";
        public const string IMAGING_TEST_APPROVAL = "IMAGING_TEST_APPROVAL";
        public const string LABORATORY_TEST_APPROVAL = "LABORATORY_TEST_APPROVAL";

        public const string IMAGING_COLONOSCOPY_ORDER = "COLONOSCOPY_ORDER";
        public const string IMAGING_CT_SCAN_ORDER = "CT_SCAN_ORDER";
        public const string IMAGING_ECG_ORDER = "ECG_ORDER";
        public const string IMAGING_ECHO_ORDER = "ECHO_ORDER";
        public const string IMAGING_ENDOSCOPY_ORDER = "ENDOSCOPY_ORDER";
        public const string IMAGING_ULTRASOUND_ORDER = "ULTRASOUND_ORDER";
        public const string IMAGING_X_RAY_ORDER = "X_RAY_ORDER";
        public const string IMAGING_MAMMOGRAPHY_ORDER = "MAMMOGRAPHY_ORDER";
        public const string IMAGING_PROCEDURE_ORDER = "PROCEDURE_ORDER";
        public const string NEW_ADMISSION_REQUEST = "NEW_ADMISSION_REQUEST";
        public const string NEW_PATIENT_REGISTERED = "NEW_PATIENT_REGISTERED";
        public const string NEW_PATIENT_DISCHARGED = "NEW_PATIENT_DISCHARGED";
        public const string NEW_PATIENT_ASSIGNED_FROM_TRIAGE = "NEW_PATIENT_ASSIGNED_FROM_TRIAGE";
        public const string NEW_LABORATORY_ORDER_CREATED = "NEW_LABORATORY_ORDER_CREATED";
        public const string NEW_IMAGING_ORDER_CREATED = "NEW_IMAGING_ORDER_CREATED";
        public const string NEW_PRESCRIPTION_CREATED = "NEW_PRESCRIPTION_CREATED";
        public const string NEW_PRESCRIPTION_SALES_CREATED = "NEW_PRESCRIPTION_SALES_CREATED";
        public const string NEW_OTHER_SERVICE_ORDER_CREATED = "NEW_OTHER_SERVICE_ORDER_CREATED";
        public const string NEW_EXTRA_MEAL_ORDER_CREATED = "NEW_EXTRA_MEAL_ORDER_CREATED";
        public const string NEW_CONSULTATION_ORDER_CREATED = "NEW_CONSULTATION_ORDER_CREATED";
        public const string NEW_OXYGEN_ORDER_CREATED = "NEW_OXYGEN_ORDER_CREATED";


        public const string NEAR_EXPIRY = "NEAR_EXPIRY";
        public const string EXPIRED_ITEM = "EXPIRED_ITEM";
        public const string NEW_MEDICATION = "NEW_MEDICATION";
        public const string STOCK_OUT = "STOCK_OUT";


    }
    public static class ImaginCategories
    {
        public const string COLONOSCOPY = "COLONOSCOPY";
        public const string CT_SCAN = "CT-SCAN";
        public const string ECG = "ECG";
        public const string ECHO = "ECHO";
        public const string ENDOSCOPY = "ENDOSCOPY";
        public const string ULTRASOUND = "ULTRASOUND";
        public const string X_RAY = "X-RAY";
        public const string MAMMOGRAPHY = "MAMMOGRAPHY";
        public const string PROCEDURE = "PROCEDURE";

    }
    public static class NotificationApi
    {
        public static bool InsertNotification(Notification notification)
        {
            SqlCommand cmd = BasicClassLibrary.BasicClass.executeProcedureWithParameter("notification_insert_notification",
                new string[]{
                    "message",
                    "type",
                    "order_id",
                    "ready",
                    "created_by",
                    "medical_employee_id"
                }, new object[]{
                    notification.message,
                    notification.type,
                    notification.order_id,
                    notification.ready,
                    notification.created_by,
                    notification.medical_employee_id
                });
            return cmd != null;
        }
        public static bool UpdateNotification(Notification notification)
        {
            SqlCommand cmd = BasicClassLibrary.BasicClass.executeProcedureWithParameter("notification_update_notification",
                new string[]{
                    "id",
                    "message",
                    "type",
                    "order_id",
                    "ready",
                    "updated_by",
                    "medical_employee_id"
                }, new object[]{
                    notification.id,
                    notification.message,
                    notification.type,
                    notification.order_id,
                    notification.ready,
                    notification.updated_by,
                    notification.medical_employee_id
                });
            return cmd != null;
        }

        public static List<Notification> GetAllUnreadNotifications(string user_id)
        {
            //DataTable tbl = Basic.BasicClass.prepareDataTable(Basic.BasicClass.executeProcedureWithParameter("notification_get_unread_notifications", new string[] { "user_id" }, new object[] { user_id }));
            SqlDataReader reader = BasicClassLibrary.BasicClass.ExecuteProcedureReaderWithParameter("notification_get_unread_notifications", new string[] { "user_id" }, new object[] { user_id });
            List<Notification> notifications = new List<Notification>();
            notifications = DataReaderMapToList<Notification>(reader).ToList();
            return notifications;
        }

        public static List<Notification> GetAllUnreadNotificationsByAccess(string user_id, DataTable userAccess)
        {
            List<Notification> notifications = GetAllUnreadNotifications(user_id);
            List<Notification> filtered = new List<Notification>();
            foreach (Notification notification in notifications)
            {
                switch (notification.type)
                {
                    case NotificationType.LABORATORY:
                        if (BasicClass.userAccessContains(NotificationAccess.LABORATORY, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.IMAGING:
                        if (BasicClass.userAccessContains(NotificationAccess.IMAGING, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.VISIT:
                        if (BasicClass.userAccessContains(NotificationAccess.VISIT, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.IMAGING_TEST_APPROVAL:
                        if (BasicClass.userAccessContains(NotificationAccess.IMAGING_TEST_APPROVAL, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.LABORATORY_TEST_APPROVAL:
                        if (BasicClass.userAccessContains(NotificationAccess.LABORATORY_TEST_APPROVAL, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.IMAGING_COLONOSCOPY_ORDER:
                        if (BasicClass.userAccessContains(NotificationAccess.IMAGING_COLONOSCOPY_ORDER, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.IMAGING_CT_SCAN_ORDER:
                        if (BasicClass.userAccessContains(NotificationAccess.IMAGING_CT_SCAN_ORDER, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.IMAGING_ECG_ORDER:
                        if (BasicClass.userAccessContains(NotificationAccess.IMAGING_ECG_ORDER, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.IMAGING_ECHO_ORDER:
                        if (BasicClass.userAccessContains(NotificationAccess.IMAGING_ECHO_ORDER, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.IMAGING_ENDOSCOPY_ORDER:
                        if (BasicClass.userAccessContains(NotificationAccess.IMAGING_ENDOSCOPY_ORDER, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.IMAGING_ULTRASOUND_ORDER:
                        if (BasicClass.userAccessContains(NotificationAccess.IMAGING_ULTRASOUND_ORDER, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.IMAGING_X_RAY_ORDER:
                        if (BasicClass.userAccessContains(NotificationAccess.IMAGING_X_RAY_ORDER, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.IMAGING_MAMMOGRAPHY_ORDER:
                        if (BasicClass.userAccessContains(NotificationAccess.IMAGING_MAMMOGRAPHY_ORDER, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.IMAGING_PROCEDURE_ORDER:
                        if (BasicClass.userAccessContains(NotificationAccess.IMAGING_PROCEDURE_ORDER, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.NEW_ADMISSION_REQUEST:
                        if (BasicClass.userAccessContains(NotificationAccess.NEW_ADMISSION_REQUEST, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.NEW_PATIENT_REGISTERED:
                        if (BasicClass.userAccessContains(NotificationAccess.NEW_PATIENT_REGISTERED, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.NEW_PATIENT_DISCHARGED:
                        if (BasicClass.userAccessContains(NotificationAccess.NEW_PATIENT_DISCHARGED, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.NEW_PATIENT_ASSIGNED_FROM_TRIAGE:
                        if (BasicClass.userAccessContains(NotificationAccess.NEW_PATIENT_ASSIGNED_FROM_TRIAGE, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.NEW_LABORATORY_ORDER_CREATED:
                        if (BasicClass.userAccessContains(NotificationAccess.NEW_LABORATORY_ORDER_CREATED, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.NEW_IMAGING_ORDER_CREATED:
                        if (BasicClass.userAccessContains(NotificationAccess.NEW_IMAGING_ORDER_CREATED, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.NEW_PRESCRIPTION_CREATED:
                        if (BasicClass.userAccessContains(NotificationAccess.NEW_PRESCRIPTION_CREATED, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.NEW_PRESCRIPTION_SALES_CREATED:
                        if (BasicClass.userAccessContains(NotificationAccess.NEW_PRESCRIPTION_SALES_CREATED, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.NEW_OTHER_SERVICE_ORDER_CREATED:
                        if (BasicClass.userAccessContains(NotificationAccess.NEW_OTHER_SERVICE_ORDER_CREATED, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.NEW_EXTRA_MEAL_ORDER_CREATED:
                        if (BasicClass.userAccessContains(NotificationAccess.NEW_EXTRA_MEAL_ORDER_CREATED, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.NEW_CONSULTATION_ORDER_CREATED:
                        if (BasicClass.userAccessContains(NotificationAccess.NEW_CONSULTATION_ORDER_CREATED, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.NEW_OXYGEN_ORDER_CREATED:
                        if (BasicClass.userAccessContains(NotificationAccess.NEW_OXYGEN_ORDER_CREATED, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.NEAR_EXPIRY:
                        if (BasicClass.userAccessContains(NotificationAccess.NEAR_EXPIRY, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.EXPIRED_ITEM:
                        if (BasicClass.userAccessContains(NotificationAccess.EXPIRED_ITEM, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.NEW_MEDICATION:
                        if (BasicClass.userAccessContains(NotificationAccess.NEW_MEDICATION, userAccess))
                            filtered.Add(notification);
                        break;
                    case NotificationType.STOCK_OUT:
                        if (BasicClass.userAccessContains(NotificationAccess.STOCK_OUT, userAccess))
                            filtered.Add(notification);
                        break;
                }
            }
            return filtered;
        }
        public static List<Notification> GetAllReadyNotifications()
        {
            SqlDataReader reader = BasicClassLibrary.BasicClass.ExecuteProcedureReader("notification_get_all_ready_notifications");
            List<Notification> notifications = new List<Notification>();
            notifications = DataReaderMapToList<Notification>(reader).ToList();
            return notifications;
        }
        public static Notification GetNotification(int id)
        {
            SqlDataReader reader = BasicClassLibrary.BasicClass.ExecuteProcedureReaderWithParameter("notification_get__notification", new string[] { "id" }, new object[] { id });
            List<Notification> notifications = new List<Notification>();
            notifications = DataReaderMapToList<Notification>(reader).ToList();
            if (notifications.Count > 0)
                return notifications[0];
            else return null;
        }
        //notification_get_notifiaction_by_order_id_and_type
        public static Notification GetNotificationByTypeAndOrderId(string order_id, string type)
        {
            SqlDataReader reader = BasicClassLibrary.BasicClass.ExecuteProcedureReaderWithParameter("notification_get_notifiaction_by_order_id_and_type", new string[] { "order_id", "type" }, new object[] { order_id, type });
            List<Notification> notifications = new List<Notification>();
            notifications = DataReaderMapToList<Notification>(reader).ToList();
            if (notifications.Count > 0)
                return notifications[0];
            else return null;
        }
        private static List<T> DataReaderMapToList<T>(DbDataReader dr)
        {
            List<T> list = new List<T>();
            while (dr.Read())
            {
                var obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
        public static bool IsNotificationSeen(int notification_id, int user_id)
        {
            DataTable tbl = BasicClassLibrary.BasicClass.prepareDataTable(BasicClassLibrary.BasicClass.executeProcedureWithParameter("notification_get_notification_status_by_notification_id_and_user_id", new string[] { "user_id", "notification_id" }, new object[] { user_id, notification_id }));
            return tbl.Rows.Count > 0;
        }
        //public static bool InsertNotificationStatus(Notification notification)
        //{
        //    SqlCommand cmd = Basic.BasicClass.executeProcedureWithParameter("notification_insert_notificatino_status", new string[] { "user_id", "notification_id" }, new object[] { notification.created_by, notification.id });
        //    return cmd != null;
        //}
        public static bool InsertNotificationStatus(Notification notification, string user_id)
        {
            SqlCommand cmd = BasicClassLibrary.BasicClass.executeProcedureWithParameter("notification_insert_notificatino_status", new string[] { "user_id", "notification_id" }, new object[] { notification.created_by, notification.id });
            return cmd != null;
        }

        public static string GetImagingOrderType(string category)
        {
            switch (category)
            {
                case ImaginCategories.COLONOSCOPY:
                    return NotificationType.IMAGING_COLONOSCOPY_ORDER;
                case ImaginCategories.CT_SCAN:
                    return NotificationType.IMAGING_CT_SCAN_ORDER;
                case ImaginCategories.ECG:
                    return NotificationType.IMAGING_ECG_ORDER;
                case ImaginCategories.ECHO:
                    return NotificationType.IMAGING_ECHO_ORDER;
                case ImaginCategories.ENDOSCOPY:
                    return NotificationType.IMAGING_ENDOSCOPY_ORDER;
                case ImaginCategories.MAMMOGRAPHY:
                    return NotificationType.IMAGING_MAMMOGRAPHY_ORDER;
                case ImaginCategories.PROCEDURE:
                    return NotificationType.IMAGING_PROCEDURE_ORDER;
                case ImaginCategories.ULTRASOUND:
                    return NotificationType.IMAGING_ULTRASOUND_ORDER;
                case ImaginCategories.X_RAY:
                    return NotificationType.IMAGING_X_RAY_ORDER;
                default:
                    return null;
            }
        }

    }

    public class Notification
    {
        public int id { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        public bool ready { get; set; }
        public string order_id { get; set; }
        public string medical_employee_id { get; set; }
        public int created_by { get; set; }
        public DateTime date { get; set; }
        public int updated_by { get; set; }
    }


    public static class NotificationHandler
    {
        public static bool CreateLaboratoryTestNotification(int user_id, string order_id)
        {
            bool status = NotificationApi.InsertNotification(
                new Notification()
                {
                    message = "",
                    type = NotificationType.LABORATORY,
                    order_id = order_id,
                    ready = false,
                    created_by = user_id,
                    medical_employee_id = user_id.ToString()
                }
            );
            return status;
        }

        public static bool CreateImagingTestNotification(int user_id, string order_id)
        {
            bool status = NotificationApi.InsertNotification(
                new Notification()
                {
                    message = "",
                    type = NotificationType.IMAGING,
                    order_id = order_id,
                    ready = false,
                    created_by = user_id,
                    medical_employee_id = user_id.ToString()
                }
            );
            return status;
        }
        public static bool CreateImagingOrderCreationNotification(int user_id, string patient_name, string category)
        {
            string notification_type = NotificationApi.GetImagingOrderType(category);
            bool status = NotificationApi.InsertNotification(
                new Notification()
                {
                    message = category + " order has been created for patient " + patient_name,
                    type = notification_type,
                    order_id = "",
                    ready = true,
                    created_by = user_id,
                    medical_employee_id = ""
                }
            );
            return status;
        }
        public static bool CreateImagingTestApprovalNotification(int user_id, string order_id)
        {
            DataTable radOrderTbale = BasicClassLibrary.BasicClass.prepareDataTable(BasicClassLibrary.BasicClass.executeProcedureWithParameter("Radiology_SearchOrder", new string[] { "order" }, new object[] { order_id }));
            if (radOrderTbale.Rows.Count <= 0)
                return false;
            string paitentName = radOrderTbale.Rows[0]["FirstName"].ToString().Trim() + " " + radOrderTbale.Rows[0]["MiddleName"].ToString().Trim();
            bool status = NotificationApi.InsertNotification(
                new Notification()
                {
                    message = "The Imaging diagnosis result for " + paitentName + " with code " + order_id + " is ready for approval!",
                    type = NotificationType.IMAGING_TEST_APPROVAL,
                    order_id = order_id,
                    ready = true,
                    created_by = user_id,
                    medical_employee_id = ""
                }
            );
            return status;
        }
        public static bool CreateLaboratoryTestApprovalNotification(int user_id, string order_id)
        {
            DataTable labOrderTbale = BasicClassLibrary.BasicClass.prepareDataTable(BasicClassLibrary.BasicClass.executeProcedureWithParameter("sp_get_lab_cutomer_by_code", new string[] { "code" }, new object[] { order_id }));
            if (labOrderTbale.Rows.Count <= 0)
                return false;
            string paitentName = labOrderTbale.Rows[0]["first_name"].ToString().Trim() + " " + labOrderTbale.Rows[0]["father_name"].ToString().Trim();

            bool status = NotificationApi.InsertNotification(
                new Notification()
                {
                    message = "The Laboratory diagnosis result for " + paitentName + " with code " + order_id + " is ready for approval!",
                    type = NotificationType.LABORATORY_TEST_APPROVAL,
                    order_id = order_id,
                    ready = true,
                    created_by = user_id,
                    medical_employee_id = ""
                }
            );
            return status;
        }
        public static void ShowLaboratoryNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.created_by == user_id && n.type == NotificationType.LABORATORY);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, n.created_by))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }


        public static void ShowImagingNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.created_by == user_id && n.type == NotificationType.IMAGING);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, n.created_by))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }

        public static void ShowImagingTestApprovalNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.IMAGING_TEST_APPROVAL);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowLaboratoryTestApprovalNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.LABORATORY_TEST_APPROVAL);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }

        public static void ShowVisitNotification(int user_id, string usersId)
        {
            //this notification is for doctors.
            //the medical employee id in the notification table corresponds to the doctor
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            DataTable medicalTable = BasicClassLibrary.BasicClass.prepareDataTable(BasicClassLibrary.BasicClass.executeProcedureWithParameter("sp_get_MedicalEmployee_on_EmployeeId", new string[] { "EmployeeId" }, new object[] { usersId }));
            if (medicalTable.Rows.Count == 0)
                return;
            string medicalEmployeeId = medicalTable.Rows[0]["id"].ToString().Trim();
            List<Notification> filtered = notifications.FindAll(n => n.medical_employee_id == medicalEmployeeId && n.type == NotificationType.VISIT);

            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void UpdateLaboratoryNotification(string lab_code)
        {
            DataTable lab_customer = BasicClassLibrary.BasicClass.prepareDataTable(BasicClassLibrary.BasicClass.executeProcedureWithParameter("sp_get_lab_cutomer_by_code", new string[] { "code" }, new object[] { lab_code }));
            if (lab_customer.Rows.Count.Equals(0))
                return;
            Notification notification = NotificationApi.GetNotificationByTypeAndOrderId(lab_customer.Rows[0]["order_id"].ToString().Trim(), NotificationType.LABORATORY);
            if (notification != null)
            {
                notification.ready = true;
                notification.message = "The Laboratory examination you ordered for patient " + lab_customer.Rows[0]["first_name"].ToString().Trim() + " " + lab_customer.Rows[0]["father_name"].ToString().Trim() + " is ready!";
                NotificationApi.UpdateNotification(notification);
            }
        }

        public static void UpdateRadiologyNotification(string code)
        {
            DataTable rad_order = BasicClassLibrary.BasicClass.prepareDataTable(BasicClassLibrary.BasicClass.executeProcedureWithParameter("Radiology_get_order_by_rad_order_code", new string[] { "code" }, new object[] { code }));
            if (rad_order.Rows.Count.Equals(0))
                return;
            Notification notification = NotificationApi.GetNotificationByTypeAndOrderId(rad_order.Rows[0]["Id"].ToString().Trim(), NotificationType.IMAGING);
            if (notification != null)
            {
                notification.ready = true;
                notification.message = "The Imagin examination you ordered for patient " + rad_order.Rows[0]["FirstName"].ToString().Trim() + " " + rad_order.Rows[0]["MiddleName"].ToString().Trim() + " is ready!";
                NotificationApi.UpdateNotification(notification);
            }
        }
        private static void ShowBallon(Notification notification)
        {
            var ballon = new System.Windows.Forms.NotifyIcon()
            {
                Visible = true,
                Icon = System.Drawing.SystemIcons.Information,
                BalloonTipText = notification.message
            };
            ballon.ShowBalloonTip(2000);
        }

        public static bool CreateVisitNotification(string medicalEmployeeId, int created_by, string visit_id)
        {
            DataTable visitTable = BasicClassLibrary.BasicClass.prepareDataTable(BasicClassLibrary.BasicClass.executeProcedureWithParameter("sp_get_Visit_by_ID", new string[] { "id" }, new object[] { int.Parse(visit_id) }));
            int patientId = int.Parse(visitTable.Rows[0]["PatientId"].ToString().Trim());
            DataTable patientTable = BasicClassLibrary.BasicClass.prepareDataTable(BasicClassLibrary.BasicClass.executeProcedureWithParameter("sp_get_Patient_by_ID", new string[] { "id" }, new object[] { patientId }));

            string patientName = patientTable.Rows[0]["FirstName"].ToString().Trim() + " " + patientTable.Rows[0]["MiddleName"].ToString().Trim();
            bool status = NotificationApi.InsertNotification(
                new Notification()
                {
                    message = "Patient " + patientName + " is assigned to you!",
                    type = NotificationType.VISIT,
                    order_id = visit_id,
                    ready = true,
                    created_by = created_by,
                    medical_employee_id = medicalEmployeeId
                }
            );
            return status;
        }
        public static bool CreateAdmissionRequestNotification(int user_id, string patientName, string doctorName)
        {
            //DataTable visitTable = BasicClassLibrary.BasicClass.prepareDataTable(BasicClassLibrary.BasicClass.executeProcedureWithParameter("sp_get_Visit_by_ID", new string[] { "id" }, new object[] { int.Parse(visit_id) }));
            //int patientId = int.Parse(visitTable.Rows[0]["PatientId"].ToString().Trim());
            //DataTable patientTable = BasicClassLibrary.BasicClass.prepareDataTable(BasicClassLibrary.BasicClass.executeProcedureWithParameter("sp_get_Patient_by_ID", new string[] { "id" }, new object[] { patientId }));

            //string patientName = patientTable.Rows[0]["FirstName"].ToString().Trim() + " " + patientTable.Rows[0]["MiddleName"].ToString().Trim();
            bool status = NotificationApi.InsertNotification(
                new Notification()
                {
                    message = "New admission for pateint " + patientName + " requested from Dr. " + doctorName,
                    type = NotificationType.NEW_ADMISSION_REQUEST,
                    order_id = "",
                    ready = true,
                    created_by = user_id,
                    medical_employee_id = ""
                }
            );
            return status;
        }
        public static bool CreateNewPatientRegisteredNotification(int user_id, string patientName)
        {

            bool status = NotificationApi.InsertNotification(
                new Notification()
                {
                    message = "New pateint " + patientName + " registered!",
                    type = NotificationType.NEW_PATIENT_REGISTERED,
                    order_id = "",
                    ready = true,
                    created_by = user_id,
                    medical_employee_id = ""
                }
            );
            return status;
        }

        public static bool CreateNewLaboratoryOrderCreatedNotification(int user_id, string patientName)
        {

            bool status = NotificationApi.InsertNotification(
                new Notification()
                {
                    message = "New laboratory order for pateint " + patientName + " created!",
                    type = NotificationType.NEW_LABORATORY_ORDER_CREATED,
                    order_id = "",
                    ready = true,
                    created_by = user_id,
                    medical_employee_id = ""
                }
            );
            return status;
        }
        public static bool CreateNewImagingOrderCreatedNotification(int user_id, string patientName)
        {

            bool status = NotificationApi.InsertNotification(
                new Notification()
                {
                    message = "New Imaging order for pateint " + patientName + " created!",
                    type = NotificationType.NEW_IMAGING_ORDER_CREATED,
                    order_id = "",
                    ready = true,
                    created_by = user_id,
                    medical_employee_id = ""
                }
            );
            return status;
        }
        public static bool CreateNewPatientDischargedNotification(int user_id, string patientName)
        {

            bool status = NotificationApi.InsertNotification(
                new Notification()
                {
                    message = "Discharge for pateint " + patientName + " has been initiated!",
                    type = NotificationType.NEW_PATIENT_REGISTERED,
                    order_id = "",
                    ready = true,
                    created_by = user_id,
                    medical_employee_id = ""
                }
            );
            return status;
        }
        public static bool CreateNewPatientAssignedFromTriageNotification(int user_id, string patientName)
        {

            bool status = NotificationApi.InsertNotification(
                new Notification()
                {
                    message = "New pateint " + patientName + " assigned from triage!",
                    type = NotificationType.NEW_PATIENT_ASSIGNED_FROM_TRIAGE,
                    order_id = "",
                    ready = true,
                    created_by = user_id,
                    medical_employee_id = ""
                }
            );
            return status;
        }
        public static bool CreateNewPrescriptionNotification(int user_id, string patientName)
        {

            bool status = NotificationApi.InsertNotification(
                new Notification()
                {
                    message = "New prescription order for patient " + patientName + " created!",
                    type = NotificationType.NEW_PRESCRIPTION_CREATED,
                    order_id = "",
                    ready = true,
                    created_by = user_id,
                    medical_employee_id = ""
                }
            );
            return status;
        }
        public static bool CreateNewPrescriptionSalesNotification(int user_id, string patientName)
        {

            bool status = NotificationApi.InsertNotification(
                new Notification()
                {
                    message = "New prescription sales order for patient " + patientName + " created!",
                    type = NotificationType.NEW_PRESCRIPTION_SALES_CREATED,
                    order_id = "",
                    ready = true,
                    created_by = user_id,
                    medical_employee_id = ""
                }
            );
            return status;
        }

        public static bool CreateNewOtherServiceNotification(int user_id, string patientName)
        {

            bool status = NotificationApi.InsertNotification(
                new Notification()
                {
                    message = "New other service order for patient " + patientName + " created!",
                    type = NotificationType.NEW_OTHER_SERVICE_ORDER_CREATED,
                    order_id = "",
                    ready = true,
                    created_by = user_id,
                    medical_employee_id = ""
                }
            );
            return status;
        }
        public static bool CreateNewExtraMealNotification(int user_id, string admissionID)
        {
            DataTable tbl = GetPatientByAdmissionId(admissionID);
            string patientName = tbl.Rows[0]["Name"].ToString().Trim();
            bool status = NotificationApi.InsertNotification(
                new Notification()
                {
                    message = "New extra meal order for patient " + patientName + " created!",
                    type = NotificationType.NEW_EXTRA_MEAL_ORDER_CREATED,
                    order_id = "",
                    ready = true,
                    created_by = user_id,
                    medical_employee_id = ""
                }
            );
            return status;
        }
        public static bool CreateNewConsultationNotification(int user_id, string admissionID)
        {
            DataTable tbl = GetPatientByAdmissionId(admissionID);
            string patientName = tbl.Rows[0]["Name"].ToString().Trim();
            bool status = NotificationApi.InsertNotification(
                new Notification()
                {
                    message = "New consultation order for patient " + patientName + " created!",
                    type = NotificationType.NEW_CONSULTATION_ORDER_CREATED,
                    order_id = "",
                    ready = true,
                    created_by = user_id,
                    medical_employee_id = ""
                }
            );
            return status;
        }

        public static bool CreateNewOxygenNotification(int user_id, string admissionID)
        {
            DataTable tbl = GetPatientByAdmissionId(admissionID);
            string patientName = tbl.Rows[0]["Name"].ToString().Trim();
            bool status = NotificationApi.InsertNotification(
                new Notification()
                {
                    message = "New oxygen order for patient " + patientName + " created!",
                    type = NotificationType.NEW_OXYGEN_ORDER_CREATED,
                    order_id = "",
                    ready = true,
                    created_by = user_id,
                    medical_employee_id = ""
                }
            );
            return status;
        }
        private static int CheckIfReportExists(string procedure, int user_id, int daysleft, bool isministore)
        {
            DataTable stockList = BasicClassLibrary.BasicClass.prepareDataTable(BasicClassLibrary.BasicClass.executeProcedureWithParameter(procedure, new string[] { "user_id", "daysleft", "isministore" }, new object[] { user_id, daysleft, isministore }));
            return stockList.Rows.Count;
        }

        public static bool CreatePharmacyRelatedNotification(int user_id)
        {

            int exiredItemCountMinistore = CheckIfReportExists("sp_get_PharmacyStock_by_Store_access", user_id, 0, true);
            int exiredItemCountMainstore = CheckIfReportExists("sp_get_PharmacyStock_by_Store_access", user_id, 0, false);

            int nearExpiryItemCountMinistoreCount = CheckIfReportExists("sp_get_PharmacyStock_by_Store_access", user_id, 90, true);
            int nearExpiryItemCountMainstoreCount = CheckIfReportExists("sp_get_PharmacyStock_by_Store_access", user_id, 90, false);

            int stockOutAlertMinsitoreCount = CheckIfReportExists("sp_get_stock_out_alert", user_id, 0, true);
            int stockOutAlertMainstoreCount = CheckIfReportExists("sp_get_stock_out_alert", user_id, 0, false);

            int newMedicationCount = CheckIfReportExists("sp_get_new_medications", user_id, 0, true);

            if (exiredItemCountMinistore > 0 || exiredItemCountMainstore > 0)
                NotificationApi.InsertNotification(
                    new Notification()
                    {
                        message = "There are expired items! Check the list in the expired items report.",
                        type = NotificationType.EXPIRED_ITEM,
                        order_id = "",
                        ready = true,
                        created_by = user_id,
                        medical_employee_id = ""
                    }
                );
            if (nearExpiryItemCountMainstoreCount > 0 || nearExpiryItemCountMinistoreCount > 0)
                NotificationApi.InsertNotification(
                    new Notification()
                    {
                        message = "There are medications near to expiry! Check the list in the near expiry medication report",
                        type = NotificationType.NEAR_EXPIRY,
                        order_id = "",
                        ready = true,
                        created_by = user_id,
                        medical_employee_id = ""
                    }
                );
            if (stockOutAlertMinsitoreCount > 0 || stockOutAlertMainstoreCount > 0)
                NotificationApi.InsertNotification(
                   new Notification()
                   {
                       message = "Some medications are out of stock! Please check the list in the stockout report.",
                       type = NotificationType.STOCK_OUT,
                       order_id = "",
                       ready = true,
                       created_by = user_id,
                       medical_employee_id = ""
                   }
               );

            if (newMedicationCount > 0)
                NotificationApi.InsertNotification(
                   new Notification()
                   {
                       message = "New medications are purchased! Check the list in the new medication report.",
                       type = NotificationType.NEW_MEDICATION,
                       order_id = "",
                       ready = true,
                       created_by = user_id,
                       medical_employee_id = ""
                   }
               );

            return true;
        }

        //newly added

        public static void ShowColonoscopyNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.IMAGING_COLONOSCOPY_ORDER);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }

        public static void ShowExpiredItemsNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.EXPIRED_ITEM);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowNearExpiryItemsNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.NEAR_EXPIRY);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowNewMedicationNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.NEW_MEDICATION);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowStockoutNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.STOCK_OUT);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }




        public static void ShowExtraMealNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.NEW_EXTRA_MEAL_ORDER_CREATED);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowOxygenNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.NEW_OXYGEN_ORDER_CREATED);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowConsultationNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.NEW_CONSULTATION_ORDER_CREATED);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowNewLaboraotryOrderNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.NEW_LABORATORY_ORDER_CREATED);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        private static DataTable GetPatientByAdmissionId(string admissionID)
        {
            DataTable table = BasicClassLibrary.BasicClass.prepareDataTable(BasicClassLibrary.BasicClass.executeProcedureWithParameter("BedManagement_get_patient_by_admission_id", new string[] { "AdmissionID" }, new object[] { admissionID }));
            return table;
        }
        public static void ShowNewOtherSerivceOrderNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.NEW_OTHER_SERVICE_ORDER_CREATED);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowNewPrescriptionNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.NEW_PRESCRIPTION_CREATED);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowNewPrescriptionSalesNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.NEW_PRESCRIPTION_SALES_CREATED);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowNewImagingOrderNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.NEW_IMAGING_ORDER_CREATED);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowPatientAssignedFromTriageNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.NEW_PATIENT_ASSIGNED_FROM_TRIAGE);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowNewPatientRegisteredNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.NEW_PATIENT_REGISTERED);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowNewPatientDischargedNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.NEW_PATIENT_DISCHARGED);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowNewAdmissionNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.NEW_ADMISSION_REQUEST);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowCtscanNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.IMAGING_CT_SCAN_ORDER);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowEcgNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.IMAGING_ECG_ORDER);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowEchoNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.IMAGING_ECHO_ORDER);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowEndoscopyNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.IMAGING_ENDOSCOPY_ORDER);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowUltrasoundNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.IMAGING_ULTRASOUND_ORDER);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowXrayNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.IMAGING_X_RAY_ORDER);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowMamoNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.IMAGING_MAMMOGRAPHY_ORDER);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        public static void ShowProcedureNotification(int user_id)
        {
            List<Notification> notifications = NotificationApi.GetAllReadyNotifications();
            List<Notification> filtered = notifications.FindAll(n => n.type == NotificationType.IMAGING_PROCEDURE_ORDER);
            foreach (var n in filtered)
            {
                if (!NotificationApi.IsNotificationSeen(n.id, user_id))
                {
                    NotificationApi.InsertNotificationStatus(n, user_id.ToString());
                    ShowBallon(n);
                }
            }
        }
        private static bool HasAccess(string a, DataTable userAccess)
        {
            return BasicClassLibrary.BasicClass.userAccessContains(a, userAccess);
        }
        public static void ShowAllNotifications(int user_id, string usersID, DataTable userAccess)
        {
            if (HasAccess(BasicClassLibrary.NotificationAccess.LABORATORY, userAccess))
                BasicClassLibrary.NotificationHandler.ShowLaboratoryNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(BasicClassLibrary.NotificationAccess.IMAGING, userAccess))
                BasicClassLibrary.NotificationHandler.ShowImagingNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(BasicClassLibrary.NotificationAccess.VISIT, userAccess))
                BasicClassLibrary.NotificationHandler.ShowVisitNotification(user_id, usersID);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.IMAGING_TEST_APPROVAL, userAccess))
                BasicClassLibrary.NotificationHandler.ShowImagingTestApprovalNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.LABORATORY_TEST_APPROVAL, userAccess))
                BasicClassLibrary.NotificationHandler.ShowLaboratoryTestApprovalNotification(user_id);
            System.Threading.Thread.Sleep(5000);

            //imaging orders
            if (HasAccess(NotificationAccess.IMAGING_COLONOSCOPY_ORDER, userAccess))
                BasicClassLibrary.NotificationHandler.ShowColonoscopyNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.IMAGING_CT_SCAN_ORDER, userAccess))
                BasicClassLibrary.NotificationHandler.ShowCtscanNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.IMAGING_ECG_ORDER, userAccess))
                BasicClassLibrary.NotificationHandler.ShowEcgNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.IMAGING_ECHO_ORDER, userAccess))
                BasicClassLibrary.NotificationHandler.ShowEchoNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.IMAGING_ENDOSCOPY_ORDER, userAccess))
                BasicClassLibrary.NotificationHandler.ShowEndoscopyNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.IMAGING_ULTRASOUND_ORDER, userAccess))
                BasicClassLibrary.NotificationHandler.ShowUltrasoundNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.IMAGING_X_RAY_ORDER, userAccess))
                BasicClassLibrary.NotificationHandler.ShowXrayNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.IMAGING_MAMMOGRAPHY_ORDER, userAccess))
                BasicClassLibrary.NotificationHandler.ShowMamoNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.IMAGING_PROCEDURE_ORDER, userAccess))
                BasicClassLibrary.NotificationHandler.ShowProcedureNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.NEW_ADMISSION_REQUEST, userAccess))
                BasicClassLibrary.NotificationHandler.ShowNewAdmissionNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.NEW_PATIENT_REGISTERED, userAccess))
                BasicClassLibrary.NotificationHandler.ShowNewPatientRegisteredNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.NEW_PATIENT_DISCHARGED, userAccess))
                BasicClassLibrary.NotificationHandler.ShowNewPatientDischargedNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.NEW_PATIENT_ASSIGNED_FROM_TRIAGE, userAccess))
                BasicClassLibrary.NotificationHandler.ShowPatientAssignedFromTriageNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.NEW_LABORATORY_ORDER_CREATED, userAccess))
                BasicClassLibrary.NotificationHandler.ShowNewLaboraotryOrderNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.NEW_IMAGING_ORDER_CREATED, userAccess))
                BasicClassLibrary.NotificationHandler.ShowNewImagingOrderNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.NEW_PRESCRIPTION_CREATED, userAccess))
                BasicClassLibrary.NotificationHandler.ShowNewPrescriptionNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.NEW_PRESCRIPTION_SALES_CREATED, userAccess))
                BasicClassLibrary.NotificationHandler.ShowNewPrescriptionSalesNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.NEW_OTHER_SERVICE_ORDER_CREATED, userAccess))
                BasicClassLibrary.NotificationHandler.ShowNewOtherSerivceOrderNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.NEW_EXTRA_MEAL_ORDER_CREATED, userAccess))
                BasicClassLibrary.NotificationHandler.ShowExtraMealNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.NEW_CONSULTATION_ORDER_CREATED, userAccess))
                BasicClassLibrary.NotificationHandler.ShowConsultationNotification(user_id);
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.NEW_OXYGEN_ORDER_CREATED, userAccess))
                BasicClassLibrary.NotificationHandler.ShowOxygenNotification(user_id);
            System.Threading.Thread.Sleep(5000);

            DateTime currentDate = BasicClassLibrary.BasicClass.getServerTime();
            DataTable checkT = BasicClassLibrary.BasicClass.prepareDataTable(BasicClassLibrary.BasicClass.executeProcedureWithParameter("notification_get_notification_by_user_id_and_type", new string[] { "user_id", "type" }, new object[] { userAccess.Rows[0]["user_identifier"].ToString().Trim(), NotificationType.EXPIRED_ITEM }));

            if (currentDate.DayOfWeek == DayOfWeek.Monday && currentDate.Hour >= 10 && checkT.Rows.Count == 0)
            {
                BasicClassLibrary.NotificationHandler.CreatePharmacyRelatedNotification(int.Parse(userAccess.Rows[0]["user_identifier"].ToString().Trim()));
            }
            System.Threading.Thread.Sleep(5000);


            if (HasAccess(NotificationAccess.NEAR_EXPIRY, userAccess))
                BasicClassLibrary.NotificationHandler.ShowExpiredItemsNotification(int.Parse(userAccess.Rows[0]["user_identifier"].ToString().Trim()));
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.EXPIRED_ITEM, userAccess))
                BasicClassLibrary.NotificationHandler.ShowNearExpiryItemsNotification(int.Parse(userAccess.Rows[0]["user_identifier"].ToString().Trim()));
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.NEW_MEDICATION, userAccess))
                BasicClassLibrary.NotificationHandler.ShowNewMedicationNotification(int.Parse(userAccess.Rows[0]["user_identifier"].ToString().Trim()));
            System.Threading.Thread.Sleep(5000);
            if (HasAccess(NotificationAccess.STOCK_OUT, userAccess))
                BasicClassLibrary.NotificationHandler.ShowStockoutNotification(int.Parse(userAccess.Rows[0]["user_identifier"].ToString().Trim()));
            System.Threading.Thread.Sleep(5000);
        }

    }
}

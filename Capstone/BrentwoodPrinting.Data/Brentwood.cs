using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
//Created by: Jack
//Last edited by: Jack
namespace BrentwoodPrinting.Data
{
    [DataObject]
    public static class Brentwood
    {
        #region aspnet_Membership
        /// <summary>
        /// Retrieves an e-mail from the database. - Troy
        /// </summary>
        /// <returns>An e-mail address</returns>
        public static String GetEmailByJobID(int jobID)
        {
            return (new Entities()).aspnet_Membership_GetEmailAddressByJobID(jobID).FirstOrDefault();
        }
        #endregion

        #region aspnet_Users

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static string GetUserNameByUserID(Guid userid)
        {
            return (new Entities()).GetUserNameByUserID(userid).FirstOrDefault();
        }

        /// <summary>
        /// Lists all users in the database. - Jack
        /// </summary>
        /// <returns>A list of users</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<ListAllUsers_Result> ListAllUsers()
        {
            return (new Entities()).aspnet_Users_ListAllUsers().ToList<ListAllUsers_Result>();
        }

        /// <summary>
        /// Gets an aspnet_Users object. - Jack
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>An aspnet_Users object</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static aspnet_Users GetUser(string username)
        {
            return (new Entities()).aspnet_Users_GetUser(username).FirstOrDefault<aspnet_Users>();
        }

        #region Employees
        /// <summary>
        /// Lists all employees in the database. - Jack
        /// </summary>
        /// <returns>A list of employees.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<GetEmployees_Result> ListEmployees()
        {
            return (new Entities()).aspnet_Users_ListEmployees().ToList<GetEmployees_Result>();
        }

        /// <summary>
        /// List all archived employees in the database. - Jack
        /// </summary>
        /// <returns>A list of archived employees.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<GetEmployees_Result> ListArchivedEmployees()
        {
            return (new Entities()).aspnet_Users_ListArchivedEmployees().ToList<GetEmployees_Result>();
        }

        /// <summary>
        /// Updates an employee in the database. - Jack
        /// </summary>
        /// <param name="email">The email of the employee.</param>
        /// <param name="firstname">The employee's first name.</param>
        /// <param name="lastname">The employee's last name.</param>
        /// <param name="username">The employees username.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static int UpdateEmployee(string email, string firstname, string lastname, string username)
        {
            return (new Entities()).aspnet_Users_UpdateEmployee(username, firstname, lastname, email);
        }

        /// <summary>
        /// Unarchives the selected employee. - Jack
        /// </summary>
        /// <param name="EmployeeID">The ID of the employee to unarchive.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static int UnArchiveEmployee(Guid EmployeeID)
        {
            return (new Entities()).aspnet_Users_UnArchiveEmployee(EmployeeID);
        }

        /// <summary>
        /// Adds the employee to the specified roles. - Jack
        /// </summary>
        /// <param name="username">The employee to add to roles.</param>
        /// <param name="roles">A list of strings representing the names of the roles to add the employee to.</param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void AddEmployeeToRoles(string username, List<string> roles)
        {
            foreach (string s in roles)
                (new Entities()).aspnet_Users_AddEmployeeToRole(username, s);
        }

        /// <summary>
        /// Removes the employee fromthe specified roles. - Jack
        /// </summary>
        /// <param name="userid">The ID of the employee to remove from roles.</param>
        /// <param name="roles">A list of strings representing the names of the roles to remove the employee from.</param>
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void RemoveEmployeeFromRoles(Guid userid, List<string> roles)
        {
            foreach (string s in roles)
                (new Entities()).aspnet_Users_RemoveEmployeeFromRole(s, userid);
        }

        /// <summary>
        /// Gets the name of all the roles the employee is currently in. - Jack
        /// </summary>
        /// <param name="userid">The ID of the employee to list roles for.</param>
        /// <returns>A list of strings representing the names of the roles.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<string> GetRolesByEmployeeID(Guid userid)
        {
            return (new Entities()).EmployeeRole_GetByEmployee(userid).ToList<string>();
        }

        /// <summary>
        /// Indicates whether the selected employee is an admin or not. - Jack
        /// </summary>
        /// <param name="employeeid">The ID of the employee to look up.</param>
        /// <returns>A boolean specifying whether or not the employee is an admin.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static bool IsEmployeeAdmin(Guid employeeid)
        {
            return bool.Parse((new Entities()).aspnet_Users_IsEmployeeAdmin(employeeid).FirstOrDefault().ToString());
        }
        #endregion

        #region Customers
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<CustomersByJobStatusID_Result> ListCustomersByJobStatusID(int jobStatusID)
        {
            return (new Entities()).ListCustomersByJobStatusID(jobStatusID).ToList<CustomersByJobStatusID_Result>();
        }

        /// <summary>
        /// Archives a customer, setting the IsApproved value to 0, not allowing the user to log in. - Jack
        /// </summary>
        /// <param name="item">The user to archive (only the UserId is required).</param>
        /// <returns>An integer representing the result of the stored procedure.</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static int ArchiveCustomer(aspnet_Users item)
        {
            return (new Entities()).aspnet_Users_ArchiveCustomer(item.UserId);
        }

        /// <summary>
        /// Archives a customer, setting the IsApproved value to 0, not allowing the user to log in. - Jack
        /// </summary>
        /// <param name="item">The ID of the user to archive.</param>
        /// <returns>An integer representing the result of the stored procedure.</returns>
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static int ArchiveCustomer(Guid userID)
        {
            return (new Entities()).aspnet_Users_ArchiveCustomer(userID);
        }

        /// <summary>
        /// Gets a customer. - Jack
        /// </summary>
        /// <param name="userid">The ID of the customer to get.</param>
        /// <returns>The customer object.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ListCustomers_Result GetCustomer(Guid userid)
        {
            return (new Entities()).aspnet_Users_GetCustomer(userid).FirstOrDefault<ListCustomers_Result>();
        }

        /// <summary>
        /// Lists all archived customers in the database. - Jack
        /// </summary>
        /// <returns>A list of listcutomers_result.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<ListCustomers_Result> ListArchivedCustomers()
        {
            return (new Entities()).aspnet_Users_ListArchivedCustomers().ToList<ListCustomers_Result>();
        }

        /// <summary>
        /// Lists all the customers in the database. - Jack
        /// </summary>
        /// <returns>A list of customer objects.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<ListCustomers_Result> ListCustomers()
        {
            return (new Entities()).aspnet_Users_ListCustomers().ToList<ListCustomers_Result>();
        }

        /// <summary>
        /// Retrieves a customer object from the specified user id. - Jack
        /// </summary>
        /// <param name="userId">The user id of the item to retrieve.</param>
        /// <returns>A customer object.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static aspnet_Users LookupCustomer(Guid userId)
        {
            return (new Entities()).aspnet_Users_LookupCustomer(userId).FirstOrDefault<aspnet_Users>();
        }

        /// <summary>
        /// Gets the customer who has the specified username. - Jack
        /// </summary>
        /// <param name="username">The username to find the customer for.</param>
        /// <returns>A customer object.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static aspnet_Users LookupCustomerByUsername(string username)
        {
            return (new Entities()).aspnet_Users_LookupCustomerByName(username).FirstOrDefault<aspnet_Users>();
        }

        /// <summary>
        /// Sets the IsApproved value of the Membership table to 1, allowing the user to log in. - Jack
        /// </summary>
        /// <param name="userId">The Id of the user to reactivate.</param>
        /// <returns>An integer representing the result of the stored procedure.</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static int UnarchiveCustomer(Guid userId)
        {
            return (new Entities()).aspnet_Users_UnArchiveCustomer(userId);
        }

        /// <summary>
        /// Updates the customer information. - Jack
        /// </summary>
        /// <param name="item">The customer object to update.</param>
        /// <returns>An integer representing the exit code of the database's stored procedure.</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static int UpdateCustomer(aspnet_Users item, string email)
        {
            return (new Entities()).aspnet_Users_UpdateCustomer(item.UserName, item.FirstName,
                item.LastName, item.CustomerAddress, item.City, item.Province, item.PostalCode, item.PhoneNumber,
                item.CompanyID, item.Approved, email);
        }

        /// <summary>
        /// Lists customers by company. - Jack
        /// </summary>
        /// <param name="CompanyID">The ID of the company.</param>
        /// <returns>A list of aspnet_Users objects.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<aspnet_Users> ListCustomersByCompany(int CompanyID)
        {
            return (new Entities()).aspnet_Users_ListByCompany(CompanyID).ToList<aspnet_Users>();
        }

        /// <summary>
        /// Lists archived customers by company. - Jack
        /// </summary>
        /// <param name="CompanyID">The ID of the company.</param>
        /// <returns>A list of aspnet_Users objects.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<aspnet_Users> ListArchivedCustomersByCompany(int CompanyID)
        {
            return (new Entities()).aspnet_Users_ListArchivedByCompany(CompanyID).ToList<aspnet_Users>();
        }

        /// <summary>
        /// Gives the customer admin priviledges for corporate accounts. - Jack
        /// </summary>
        /// <param name="username">The username of the user to make an admin.</param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void MakeCustomerAdmin(string username)
        {
            (new Entities()).aspnet_Users_MakeCustomerAdmin(username);
        }

        /// <summary>
        /// Gives the customer admin priviledges for corporate accounts. - Jack
        /// </summary>
        /// <param name="userID">The user ID of the user to make an admin.</param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void MakeCustomerAdmin(Guid userID)
        {
            (new Entities()).aspnet_Users_MakeCustomerAdminByUserId(userID);
        }

        /// <summary>
        /// Removes the customer admin priviledges for corporate accounts. - Jack
        /// </summary>
        /// <param name="userID">The user ID of the user to make an admin.</param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void MakeCustomerNotAdmin(Guid userID)
        {
            (new Entities()).aspnet_Users_MakeCustomerNotAdmin(userID);
        }
        #endregion
        #endregion

        #region Company
        /// <summary>
        /// Approves the selected company for purchases. - Jack
        /// </summary>
        /// <param name="CompanyID">The ID of the company to approve.</param>
        public static void ApproveCompany(int CompanyID)
        {
            (new Entities()).Company_Approve(CompanyID);
        }

        /// <summary>
        /// Disapproves the selected company for purchases. - Jack
        /// </summary>
        /// <param name="CompanyID">The ID of the company to approve.</param>
        public static void DisapproveCompany(int CompanyID)
        {
            (new Entities()).Company_Disapprove(CompanyID);
        }

        /// <summary>
        /// Retrieves a list of companies from the database. - Jack
        /// </summary>
        /// <returns>A list of company objects.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<Company> ListCompanies()
        {
            return (new Entities()).Company_List().ToList<Company>();
        }

        /// <summary>
        /// Gets a company object that the specified customer object belongs to. Returns nothing if there are no companies associated with the customer. - Jack
        /// </summary>
        /// <param name="userId">The user id of the customer to lookup companies for.</param>
        /// <returns>A company object.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Company GetCompanyByCustomerId(Guid userId)
        {
            return (new Entities()).Company_GetByCustomer(userId).FirstOrDefault<Company>();
        }

        /// <summary>
        /// Archives a company. - Jack
        /// </summary>
        /// <param name="CompanyID">The ID of the company to archive.</param>
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void ArchiveCompany(int CompanyID)
        {
            (new Entities()).Company_Archive(CompanyID);
        }

        /// <summary>
        /// Inserts a new company. - Jack
        /// </summary>
        /// <param name="name">The name of the company.</param>
        /// <param name="address">The address of the company.</param>
        /// <param name="city">The city of the company.</param>
        /// <param name="province">The province of the company.</param>
        /// <param name="postalcode">The postal code of the company.</param>
        /// <param name="phonenumber">The company's phone number.</param>
        /// <param name="website">The company's website.</param>
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static int AddCompany(string name, string address, string city, string province,
            string postalcode, string phonenumber, string website)
        {
            int? rval = (new Entities()).Company_Insert(name, address, city, province, postalcode, phonenumber,
                website, false).FirstOrDefault();

            return rval ?? default(int);
        }

        /// <summary>
        /// Updates a company. - Jack
        /// </summary>
        /// <param name="companyid">The ID of the company to update.</param>
        /// <param name="name">The name of the company.</param>
        /// <param name="address">The address of the company.</param>
        /// <param name="city">The city of the company.</param>
        /// <param name="province">The province of the company.</param>
        /// <param name="postalcode">The postal code of the company.</param>
        /// <param name="phonenumber">The company's phone number.</param>
        /// <param name="website">The company's website.</param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateCompany(int companyid, string name, string address, string city, 
            string province, string postalcode, string phonenumber, string website)
        {
            (new Entities()).Company_Update(companyid, name, address, city, province, postalcode, phonenumber,
                website, false);
        }

        /// <summary>
        /// Lists all archived companies. - Jack
        /// </summary>
        /// <returns>A list of company objects.</returns>
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static List<Company> ListArchivedCompanies()
        {
            return (new Entities()).Company_ListArchived().ToList<Company>();
        }

        /// <summary>
        /// Unarchives the selected company. - Jack
        /// </summary>
        /// <param name="companyID">The ID of the company to reactivate.</param>
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void UnArchiveCompany(int companyID)
        {
            (new Entities()).Company_UnArchive(companyID);
        }

        /// <summary>
        /// Retrieves the selected company object. - Jack
        /// </summary>
        /// <param name="CompanyID">The ID of the company object to get.</param>
        /// <returns>A Company object.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static Company GetCompany(int CompanyID)
        {
            return (new Entities()).Company_Get(CompanyID).FirstOrDefault<Company>();
        }

        /// <summary>
        /// Adds the user to the company. - Jack
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="CompanyID">The ID of the company.</param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void AddUserToCompany(string username, int CompanyID)
        {
            aspnet_Users user = LookupCustomerByUsername(username);
            (new Entities()).aspnet_Users_AddUserToCompany(user.UserId, CompanyID);
        }
        #endregion

        #region EmployeeRole
        /// <summary>
        /// Lists all roles an employee may belong to. - Jack
        /// </summary>
        /// <returns>A list of employeerole objects.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<EmployeeRole> ListEmployeeRoles()
        {
            return (new Entities()).EmployeeRole_List().ToList<EmployeeRole>();
        }

        /// <summary>
        /// Gets the associated employee role from the status id. - Jack
        /// </summary>
        /// <param name="JobStatusID">The ID representing the job to get the role for.</param>
        /// <returns>An employeerole object.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EmployeeRole GetEmployeeRoleByStatusID(int JobStatusID)
        {
            return (new Entities()).EmployeeRole_GetByStatusID(JobStatusID).FirstOrDefault<EmployeeRole>();
        }

        /// <summary>
        /// Lists the roles by employee. - Jack
        /// </summary>
        /// <param name="EmployeeID">The employee id.</param>
        /// <returns>A list of employee role.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<EmployeeRole> ListRolesByEmployee(Guid EmployeeID)
        {
            return (new Entities()).EmployeeRole_ListByEmployeeID(EmployeeID).ToList<EmployeeRole>();
        }

        /// <summary>
        /// Checks to see if the employee is in the specified employee role. - Jack
        /// </summary>
        /// <param name="username">The username of the employee.</param>
        /// <param name="rolename">The name of the role.</param>
        /// <returns></returns>
        public static bool IsEmployeeInRole(string username, string rolename)
        {
            if (int.Parse((new Entities()).EmployeeRole_IsEmployeeInRole(username, rolename).FirstOrDefault<int?>().ToString()) == 0)
                return false;
            else
                return true;
        }

        public static void InsertEmployeeRole(string RoleName)
        {
            (new Entities()).EmployeeRole_Insert(RoleName);
        }

        public static void UpdateEmployeeRole(int RoleID, string RoleName)
        {
            (new Entities()).EmployeeRole_Update(RoleID, RoleName);
        }

        public static void ArchiveEmployeeRole(int RoleID)
        {
            (new Entities()).EmployeeRole_Archive(RoleID);
        }

        public static void UnArchiveEmployeeRole(int RoleID)
        {
            (new Entities()).EmployeeRole_UnArchive(RoleID);
        }

        public static List<EmployeeRole> ListArchivedEmployeeRoles()
        {
            return (new Entities()).EmployeeRole_ListArchived().ToList<EmployeeRole>();
        }
        #endregion

        #region Job Assets

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static int AddJobAsset(string Filepath, int JobID)
        {
            return (new Entities()).JobAsset_Insert(Filepath, JobID);
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static void AddAssetsToJob(List<string> filepaths, int jobID)
        {
            JobAssetAuthority.ProcessJobAssets(filepaths, jobID);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<JobAsset> ListJobAssetsByJobID(int jobID)
        {
            return (new Entities()).JobAsset_ListByJob(jobID).ToList<JobAsset>();
        }
        #endregion

        #region Job Control
        /// <summary>
        /// Lists all the job controls in the database. - Jack
        /// </summary>
        /// <returns>A list of job controls.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<JobControl_Get_Result> ListJobControls()
        {
            return (new Entities()).JobControl_List().ToList<JobControl_Get_Result>();
        }
        
        /// <summary>
        /// Lists all the job controls belonging to a job type. - Jack
        /// </summary>
        /// <param name="JobTypeID">The ID of the job type to look up.</param>
        /// <returns>A list of job controls belonging to the specified job type ID.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<JobControl_Get_Result> ListJobControlByJobType(int JobTypeID)
        {
            return (new Entities()).JobControl_ListByJobType(JobTypeID).ToList<JobControl_Get_Result>();
        }

        /// <summary>
        /// Inserts a new job control object into the database. - Jack
        /// </summary>
        /// <param name="item">The job control to add.</param>
        /// <returns>An integer representing the ID of the job control given by the database.</returns>
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static int AddJobControl(JobControl item, int jobTypeID)
        {
            int? rval = (new Entities()).JobControl_Insert(item.ControlName, 
                item.JobControlID, jobTypeID).FirstOrDefault();
            return rval ?? default(int);
        }

        /// <summary>
        /// An overloaded method to add a job control to the database with just a name and the ID of the type of control. - Jack
        /// </summary>
        /// <param name="controlName">The name of the control.</param>
        /// <param name="jobControlTypeID">An integer representing the ID of the control type.</param>
        /// <returns>An integer representing the ID of the job control given by the database.</returns>
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static int AddJobControl(string controlName, int jobControlTypeID, int jobTypeID)
        {
            int? rval = (new Entities()).JobControl_Insert(controlName, 
                jobControlTypeID, jobTypeID).FirstOrDefault();
            return rval ?? default(int);
        }

        /// <summary>
        /// Creates a new job type with controls. - Jack
        /// </summary>
        /// <param name="item">The job type object to insert.</param>
        /// <param name="controls">A list of job controls to insert into the database and add to the job type object.</param>
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static void ProcessJobType(JobType item, List<JobControl> controls)
        {
            int jtid = AddJobType(item);
            
            JobControlsAuthority.ProcessJobControlsList(jtid, controls);
        }

        /// <summary>
        /// Updates the specified job type with the list of controls. - Jack
        /// </summary>
        /// <param name="item">The job type object to update.</param>
        /// <param name="controls">A list of controls to update.</param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void ProcessJobTypeUpdate(JobType item, List<JobControl> controls)
        {
            UpdateJobType(item);
            JobControlsAuthority.ProcessJobControlsListUpdate(item.JobTypeID, controls);
        }

        /// <summary>
        /// Updates the specified job control. - Jack
        /// </summary>
        /// <param name="item">The updated job control.</param>
        /// <returns>An integer representing the ID of the job control given by the database.</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateJobControl(JobControl item)
        {
            int? rval = (new Entities()).JobControl_Update(item.JobControlID, item.ControlName,
                item.JobControlID).FirstOrDefault();
        }

        #endregion

        #region Job Control Type
        /// <summary>
        /// Lists all the types of job controls in the database. - Jack
        /// </summary>
        /// <returns>A list of job control type objects.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<JobControlType> ListJobControlTypes()
        {
            return (new Entities()).JobControlType_List().ToList<JobControlType>();
        }

        /// <summary>
        /// Gets the specified job control type from the given ID number. - Jack
        /// </summary>
        /// <param name="jobControlTypeID">The ID of the job control type.</param>
        /// <returns>A job control type object.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static JobControlType GetJobControlType(int jobControlTypeID)
        {
            return (new Entities()).JobControlType_Get(jobControlTypeID).FirstOrDefault<JobControlType>();
        }

        /// <summary>
        /// Gets the type of the control. - Jack
        /// </summary>
        /// <param name="controlID">The ID of the control to lookup.</param>
        /// <returns>A jobcontroltype object.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static JobControlType GetJobControlTypeByControl(int controlID)
        {
            return (new Entities()).JobControlType_GetControlTypeByControlID(controlID).FirstOrDefault<JobControlType>();
        }

        /// <summary>
        /// Gets a job control type object by the specified name. - Jack
        /// </summary>
        /// <param name="name">The name of the job control type.</param>
        /// <returns>A job control type object. NOTE: Will only return one object; if there are multiple results in the stored procedure, only the first will be returned.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static JobControlType GetJobControlTypeByName(string name)
        {
            return (new Entities()).JobControlType_GetByName(name).FirstOrDefault<JobControlType>();
        }
        #endregion

        #region Job Info
        /// <summary>
        /// Adds a piece of information to a job. - Jack
        /// </summary>
        /// <param name="item">The information object to add.</param>
        /// <returns></returns>
        public static int AddJobInfo(JobInfo item)
        {
            return (new Entities()).JobInfo_Insert(item.NameKey, item.DataValue, item.JobID);
        }

        /// <summary>
        /// Adds a list of information to a job. - Jack
        /// </summary>
        /// <param name="info">The JobInfo objects to add.</param>
        /// <param name="jobID">The Job to add them to.</param>
        public static void AddInfoToJob(List<JobInfo> info, int jobID)
        {
            JobInfoAuthority.ProcessJobInfo(info, jobID);
        }

        /// <summary>
        /// Lists all info for a job. - Jack
        /// </summary>
        /// <param name="JobID">The ID of the job.</param>
        /// <returns>A list of JobInfo objects.</returns>
        public static List<JobInfo> ListJobInfoByJob(int JobID)
        {
            return (new Entities()).JobInfo_ListByJob(JobID).ToList<JobInfo>();
        }

        /// <summary>
        /// Updates job info. - Jack
        /// </summary>
        /// <param name="info">The list of job info items.</param>
        public static void UpdateJobInfo(List<JobInfo> info)
        {
            JobInfoAuthority.UpdateJobInfo(info);
        }
        #endregion

        #region Job Job Status
        /// <summary>
        /// Updates the status of the job. - Jack
        /// </summary>
        /// <param name="jobStatusID">The ID of the job status.</param>
        /// <param name="jobID">The ID of the job.</param>
        /// <param name="userID">The user who updated the status.</param>
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static void ChangeJobJobStatus(int jobStatusID, int jobID, Guid userID)
        {
            (new Entities()).JobJobStatus_Change(jobStatusID, jobID, userID);
        }

        /// <summary>
        /// Sets the initial job status of a job to the one with the lowest ID in the database. - Jack
        /// </summary>
        /// <param name="JobID">The job to set the status for.</param>
        /// <param name="UserID">The user who is initializing the job.</param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void InitializeJobJobStatus(int JobID, Guid UserID)
        {
            (new Entities()).JobJobStatus_Initialize(JobID, UserID);
        }

        /// <summary>
        /// Lists all the job statuses belonging to that job. - Jack
        /// </summary>
        /// <param name="JobID">The ID of the job the lookup statuses for.</param>
        /// <returns>A list of JobStatusesByJobID_Result objects.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<JobStatusesByJobID_Result> ListJobStatusesByJob(int JobID)
        {
            return (new Entities()).JobJobStatus_ListJobStatusesByJobID(JobID).ToList<JobStatusesByJobID_Result>();
        }

        /// <summary>
        /// Gets the current status associated with the given job id. - Jack
        /// </summary>
        /// <param name="JobID">The ID of the job to lookup.</param>
        /// <returns>A JobStatus object.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static JobJobStatus GetCurrentJobStatus(int JobID)
        {
            return (new Entities()).JobJobStatus_GetCurrentByJobID(JobID).FirstOrDefault<JobJobStatus>();
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void UpdateJobJobStatus(int jobID, Guid userID, int jobStatusID)
        {
            (new Entities()).JobJobStatus_Update(jobID, userID, jobStatusID);
        }
        #endregion

        #region Job Proofs
        /// <summary>
        /// Inserts a new job proof into the database.
        /// </summary>
        /// <param name="Item">The file path and associated jobID.</param>
        /// <returns>An integer representing the ID given to the job proof by the database.</returns>
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static int InsertJobProof(int jobid, string filepath)
        {
            int? rval = (new Entities()).JobProof_Insert(jobid, filepath).FirstOrDefault();
            return rval ?? -1;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<JobProof> ListJobProofsByJobID(int jobID)
        {
            return (new Entities()).JobProof_ListByJobID(jobID).ToList<JobProof>();
        }
        #endregion

        #region JobStatus
        /// <summary>
        /// Lists all job statuses in the database. - Jack
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<JobStatus> ListJobStatuses()
        {
            return (new Entities()).JobStatus_List().ToList<JobStatus>();
        }

        /// <summary>
        /// Lists all job statuses in the database with the name of the associated EmployeeRole. - Jack
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<ListJobStatusWithStatusName_Result> ListJobStatusWithName()
        {
            return (new Entities()).JobStatus_ListWithStatusName().ToList<ListJobStatusWithStatusName_Result>();
        }

        /// <summary>
        /// Lists all archived job statuses. - Jack
        /// </summary>
        /// <returns>A list of JobStatus objects.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<JobStatus> ListArchivedJobStatuses()
        {
            return (new Entities()).JobStatus_ListArchived().ToList<JobStatus>();
        }

        /// <summary>
        /// Returns the selected Job Status object. - Jack
        /// </summary>
        /// <param name="JobStatusID">The ID of the job status to get.</param>
        /// <returns>A job status object.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static JobStatus GetJobStatus(int JobStatusID)
        {
            return (new Entities()).JobStatus_Get(JobStatusID).FirstOrDefault<JobStatus>();
        }

        /// <summary>
        /// Gets a job status based on it's name. Returns only the first result. - Jack
        /// </summary>
        /// <param name="name">The name of the job status to get.</param>
        /// <returns>The first job status object.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static JobStatus GetJobStatusByName(string name)
        {
            return (new Entities()).JobStatus_GetByName(name).FirstOrDefault<JobStatus>();
        }

        /// <summary>
        /// Inserts a new job status. - Jack
        /// </summary>
        /// <param name="name">The name of the job status to insert.</param>
        /// <param name="employeeRoleID">The id of the employeerole to associate with this job status.</param>
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static void InsertJobStatus(string name, int employeeRoleID)
        {
            (new Entities()).JobStatus_Insert(name, employeeRoleID);
        }

        /// <summary>
        /// Updates the specified job status. - Jack
        /// </summary>
        /// <param name="name">The name of the job status.</param>
        /// <param name="jobstatusID">The ID of the job status to update.</param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void UpdateJobStatus(string name, int jobstatusID)
        {
            (new Entities()).JobStatus_Update(jobstatusID, name);
        }

        /// <summary>
        /// Archives the selected job status. - Jack
        /// </summary>
        /// <param name="JobStatusID">The ID of the job status to archive.</param>
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void ArchiveJobStatus(int JobStatusID)
        {
            (new Entities()).JobStatus_Archive(JobStatusID);
        }

        /// <summary>
        /// Unarchives the selected job status. - Jack
        /// </summary>
        /// <param name="JobStatusID">The ID of the job status to reactivate.</param>
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static void UnArchiveJobStatus(int JobStatusID)
        {
            (new Entities()).JobStatus_UnArchive(JobStatusID);
        }

        /// <summary>
        /// Lists all statuses associated with an employee role. - Jack
        /// </summary>
        /// <param name="roleID">The role id to get statuses for.</param>
        /// <returns>A list of job statuses.</returns>
        public static List<JobStatus> ListStatusesByRole(int roleID)
        {
            return (new Entities()).JobStatus_ListByRole(roleID).ToList<JobStatus>();
        }

        /// <summary>
        /// Gets the ordering number for the 'Payment Complete' status. - Jack
        /// </summary>
        /// <returns>An integer representing the ordering number for Payment Complete.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static int GetLastOrderingNo()
        {
            int? rval = (new Entities()).JobStatus_GetLastOrderingNo().FirstOrDefault<int?>();
            return rval ?? default(int);
        }

        /// <summary>
        /// Gets the ordering number for the 'Designs Submitted' status. - Jack
        /// </summary>
        /// <returns>An integer representing the ordering number for Payment Complete.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static int GetFirstOrderingNo()
        {
            int? rval = (new Entities()).JobStatus_GetFirstOrderingNo().FirstOrDefault<int?>();
            return rval ?? default(int);
        }

        /// <summary>
        /// Gets a job status based off it's ordering no. - Jack
        /// </summary>
        /// <param name="orderingNo">The ordering number to get the JobStatus for.</param>
        /// <returns>A JobStatus object.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static JobStatus GetStatusByOrderingNumber(int orderingNo)
        {
            return (new Entities()).JobStatus_GetByOrderingNumber(orderingNo).FirstOrDefault<JobStatus>();
        }

        /// <summary>
        /// Changes the ordering number of the selected job status item. - Jack
        /// </summary>
        /// <param name="orderingNo">The number to change to.</param>
        /// <param name="jobStatusID">The id of the job status to change.</param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void ChangeStatusOrderingNo(int orderingNo, int jobStatusID)
        {
            (new Entities()).JobStatus_ChangeOrderingNumber(jobStatusID, orderingNo);
        }
        #endregion

        #region Job Type
        /// <summary>
        /// Retrieves a list of job types from the database. - Jack
        /// </summary>
        /// <returns>A list of job type objects.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<JobType> ListJobTypes()
        {
            return (new Entities()).JobType_List().ToList<JobType>();
        }

        /// <summary>
        /// Retrieves a list of archived job types from the database. - Jack
        /// </summary>
        /// <returns>A list of job type objects, where 'Archived' is equal to 'Y'.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<JobType> ListArchivedJobTypes()
        {
            return (new Entities()).JobType_ListArchived().ToList<JobType>();
        }

        /// <summary>
        /// Gets a job type with the specified Job Type ID. - Jack
        /// </summary>
        /// <param name="JobTypeID">An integer representing the ID of the job type.</param>
        /// <returns>A job type with the specified job type ID.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static JobType GetJobType(int JobTypeID)
        {
            return (new Entities()).JobType_Get(JobTypeID).FirstOrDefault<JobType>();
        }

        /// <summary>
        /// Gets the type of a job. - Jack
        /// </summary>
        /// <param name="JobID">The ID of the job to get the type for.</param>
        /// <returns>A jobType object.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static JobType GetJobTypeByJob(int JobID)
        {
            return (new Entities()).JobType_GetByJob(JobID).FirstOrDefault<JobType>();
        }

        /// <summary>
        /// Inserts a new job type into the database. - Jack
        /// </summary>
        /// <param name="Item">The job type to add.</param>
        /// <returns>An integer representing the ID given to the job type by the database.</returns>
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static int AddJobType(JobType Item)
        {
            int? rval = (new Entities()).JobType_Insert(Item.Name, Item.EstimatedTimeToComplete, Item.JobTypeDescription).FirstOrDefault<int?>();
            return rval ?? default(int);
        }

        /// <summary>
        /// Updates the job type with the specified information. - Jack
        /// </summary>
        /// <param name="Item">The updated job type object.</param>
        /// <returns>An integer representing the ID given to the job type by the database.</returns>
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static int UpdateJobType(JobType Item)
        {
            int? rval = (new Entities()).JobType_Update(Item.Name, Item.EstimatedTimeToComplete, Item.JobTypeDescription, Item.JobTypeID).FirstOrDefault();
            return rval ?? default(int);
        }

        /// <summary>
        /// Archives the specified job type, setting the 'Archived' property to 'Y'. - Jack
        /// </summary>
        /// <param name="Item">The job type object to archive.</param>
        /// <returns>An integer representing the ID given to the job type by the database.</returns>
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static int ArchiveJobType(JobType Item)
        {
            return (new Entities()).JobType_Archive(Item.JobTypeID);
        }

        /// <summary>
        /// An overloaded method to archive a job type based on the ID of the job type. - Jack
        /// </summary>
        /// <param name="JobTypeID">The ID of the job type to archive.</param>
        /// <returns>An integer representing the ID given to the job type by the database.</returns>
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public static int ArchiveJobType(int JobTypeID)
        {
            return (new Entities()).JobType_Archive(JobTypeID);
        }

        /// <summary>
        /// Resets the archvied property of the specified job type to 'N'. - Jack
        /// </summary>
        /// <param name="JobTypeID">The ID of the job type to unarchive.</param>
        /// <returns>An integer representing the ID given to the job type by the database.</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static int UnArchiveJobType(int JobTypeID)
        {
            return (new Entities()).JobType_UnArchive(JobTypeID);
        }
        #endregion

        #region Job
        public static void MarkJobAsPaid(int JobID)
        {
            (new Entities()).Job_MarkAsPaid(JobID);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<JobsByCustomer_Result> ListJobsByCustomer(Guid CustomerID)
        {
            return (new Entities()).Job_ListByCustomer(CustomerID).ToList<JobsByCustomer_Result>();
        }

        public static void UpdateJob(Job data)
        {
            (new Entities()).Job_Update(data.JobID, data.JobTypeID, data.SpecialInstructions, data.Quantity, data.DeliveryOrPickup, data.CustomerID, data.PromiseDate, data.StartDate);
        }

        /// <summary>
        /// Returns a list of jobs by company id. - Jack
        /// </summary>
        /// <param name="CompanyID">The ID of the company to retrieve jobs for.</param>
        /// <returns>A list of job objects.</returns>
        public static List<CustomerDashboard> ListJobsByCompany(int CompanyID)
        {
            return (new Entities()).Job_ListByCompany(CompanyID).ToList<CustomerDashboard>();
        }

        /// <summary>
        /// Returns a list of ouststanding invoices by customer. - Jack
        /// </summary>
        /// <param name="Username">The username of the customer to lookup invoices for.</param>
        /// <returns>A list of customer invoices.</returns>
        public static List<GetOutstandingInvoicesByCustomer_Result> GetOutstandingInvoicesByCustomer(string Username)
        {
            return (new Entities()).Job_GetOutstandingInvoicesByCustomer(Username).ToList<GetOutstandingInvoicesByCustomer_Result>();
        }

        /// <summary>
        /// Retrieves general information on outstanding invoices - Troy, Reworked by Jack
        /// </summary>
        /// <returns>User First and Last Name, Job Name, Job ID, Delivery or Pickup, Invoiced Date, Messaged On</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<GetOutstandingInvoices_Result> GetOutstandingInvoices()
        {
            return (new Entities()).Job_GetOutstandingInvoices().ToList<GetOutstandingInvoices_Result>();
        }

        /// <summary>
        /// Retrieves a list of jobs, and other information, based on job status ID.
        /// </summary>
        /// <param name="JobTypeID">The jobs status ID int.</param>
        /// <returns>A list of complex type 'JobsByJobStatusID_Result'.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<JobsByJobStatusID_Result> ListJobsByJobStatusID(int jobStatusID)
        {
            return (new Entities()).ListJobsByJobStatusID(jobStatusID).ToList<JobsByJobStatusID_Result>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobStatusid"></param>
        /// <param name="customerid"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<JobsByJobStatusIDCustomerID_Result> ListJobsByJobStatusIDCustomerID(int jobStatusid, Guid customerid)
        {
            return (new Entities()).ListJobsByJobStatusIDCustomerID(jobStatusid, customerid).ToList<JobsByJobStatusIDCustomerID_Result>();
        }

        /// <summary>
        /// Lists all jobs by the given status id.
        /// </summary>
        /// <param name="JobStatusID">The ID of the job status to list for.</param>
        /// <returns>A list of job status objects.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<ListByJobStatus_Result> ListJobsByJobStatus(int JobStatusID)
        {
            return (new Entities()).Job_ListByJobStatus(JobStatusID).ToList<ListByJobStatus_Result>();
        }

        /// <summary>
        /// Retrieves details for one job, for the given Job ID.
        /// </summary>
        /// <param name="JobTypeID">The job ID int.</param>
        /// <returns>A single instance of the complex type 'JobDetailsByJobID_Result'.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<JobDetailsByJobID_Result> GetJobDetailsByJobID(int jobID)
        {
            return (new Entities()).GetJobDetailsByJobID(jobID).ToList<JobDetailsByJobID_Result>();
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Job GetJob(int JobID)
        {
            return (new Entities()).Job_Get(JobID).FirstOrDefault<Job>();
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static int AddJob(int jobTypeID, string specialInstructions, int quantity, string deliveryOrPickup, Guid customerID, DateTime promiseDate)
        {
            return (int)(new Entities()).Job_Insert(jobTypeID, specialInstructions, quantity, deliveryOrPickup, customerID, promiseDate).FirstOrDefault();
        }

        /// <summary>
        /// Gets the job items for a customer's dashboard. - Jack
        /// </summary>
        /// <param name="username">The username of the customer.</param>
        /// <returns>A list of Customer Dashboard objects.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<CustomerDashboard> GetDashboard(string username)
        {
            return (new Entities()).Job_ListByCustomerUsername(username).ToList<CustomerDashboard>();
        }

        /// <summary>
        /// Gets the current status of a job. - Jack
        /// </summary>
        /// <param name="JobID">The ID of the Job.</param>
        /// <returns>A JobStatus object.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static JobStatus GetJobStatusByJob(int JobID)
        {
            return (new Entities()).Job_GetStatus(JobID).FirstOrDefault<JobStatus>();
        }

        /// <summary>
        /// Mark
        /// </summary>
        /// <param name="jobid"></param>
        /// <param name="quote"></param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static void Job_AddQuote(int jobid, decimal quote)
        {
            (new Entities()).Job_AddQuote(jobid, quote);
        }

        /// <summary>
        /// Mark
        /// </summary>
        /// <param name="jobStatusID"></param>
        /// <param name="nameSearch"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<JobsByJobStatusID_Result> ListJobsByJobStatusIDCustomerName(int jobStatusID, string nameSearch)
        {
            return (new Entities()).ListJobsByJobStatusIDCustomerName(jobStatusID, nameSearch).ToList<JobsByJobStatusID_Result>();
        }

        /// <summary>
        /// Mark
        /// </summary>
        /// <param name="jobStatusID"></param>
        /// <param name="nameSearch"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<JobsByJobStatusID_Result> ListJobsByJobStatusIDCompanyName(int jobStatusID, string nameSearch)
        {
            return (new Entities()).ListJobsByJobStatusIDCompanyName(jobStatusID, nameSearch).ToList<JobsByJobStatusID_Result>();
        }

        /// <summary>
        /// Marks a job as having been invoiced. - Jack
        /// </summary>
        /// <param name="JobID">The ID of the job to mark.</param>
        public static void MarkJobAsInvoiced(int JobID)
        {
            (new Entities()).Job_MarkAsInvoiced(JobID);
        }
        #endregion

        #region User Message
        /// <summary>
        /// Inserts a new record into UserMessage - Troy
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static void AddUserMessage(string messageSubject, string messageBody, int jobID, Guid sentFrom, Guid sentTo, DateTime messagedOn)
        {
            (new Entities()).UserMessage_Insert(messageSubject, messageBody, jobID, sentFrom, sentTo, messagedOn);
        }
        #endregion
    }
}
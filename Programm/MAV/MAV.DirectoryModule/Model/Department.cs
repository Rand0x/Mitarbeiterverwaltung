using System.Collections.Generic;

namespace MAV.DirectoryModule.Model
{
    public class Department
    {
        private List<string> departments = new List<string>();

        private static string termForAllDeps = "Alle Abteilungen";

        public static string TermForAllDeps
        {
            get { return termForAllDeps; }
        }

        public List<string> Departments
        {
            get { return departments; }
        }

        public Department()
        {
            departments.Add("AdminOffices");
            departments.Add("ExecutiveOffice");
            departments.Add("IT/IS");
            departments.Add("Production");
            departments.Add("Sales");
            departments.Add("SoftwareEngineering");
        }
    }
}

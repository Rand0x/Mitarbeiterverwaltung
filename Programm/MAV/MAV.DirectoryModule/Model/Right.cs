using System.Collections.Generic;

namespace MAV.DirectoryModule.Model
{
    public class Right
    {
        private List<string> rights = new List<string>();

        public List<string> Rights
        {
            get { return rights; }
        }

        public Right()
        {
            rights.Add("Administrator");
            rights.Add("Personal");
            rights.Add("Mitarbeiter");
        }
    }
}

using System;

using Windows.ApplicationModel;

namespace AppStudio.ViewModels
{
    public class AboutThisAppViewModel
    {
        public string Publisher
        {
            get
            {
                return Package.Current.Id.Publisher.Substring(3);
            }
        }

        public string AppVersion
        {
            get
            {
                return string.Format("{0}.{1}", Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor);
            }
        }

        public string AboutText
        {
            get
            {
                return "This app provides daily tips on how you can be useful to improve environment. \r\nH" +
    "ow a normal person can Revalue, Reduce, Reuse, Renew and Recycle?";
            }
        }
    }
}


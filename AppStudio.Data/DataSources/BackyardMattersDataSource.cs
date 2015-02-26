using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class BackyardMattersDataSource : IDataSource<RssSchema>
    {
        private const string _url =@"http://www.thedailygreen.com/living-green/blogs/recycling-design-technology/recycling-design-technology-rss";

        private IEnumerable<RssSchema> _data = null;

        public BackyardMattersDataSource()
        {
        }

        public async Task<IEnumerable<RssSchema>> LoadData()
        {
            if (_data == null)
            {
                try
                {
                    var rssDataProvider = new RssDataProvider(_url);
                    _data = await rssDataProvider.Load();
                }
                catch (Exception ex)
                {
                    AppLogs.WriteError("BackyardMattersDataSourceDataSource.LoadData", ex.ToString());
                }
            }
            return _data;
        }

        public async Task<IEnumerable<RssSchema>> Refresh()
        {
            _data = null;
            return await LoadData();
        }
    }
}

using System.Collections.ObjectModel;
using System.Linq;

namespace XAML_test_app.Models
{
    public class RegionCollection
    {
        public ObservableCollection<Region> RegionList { get; set; }

        public RegionCollection()
        {
            //initialize region list
            RegionList = new ObservableCollection<Region>()
            {
                new Region("United States", "en-US"),
                new Region("United Kingdom", "en-GB"),
                new Region("Canada", "en-CA"),
                new Region("Australia", "en-AU"),
                new Region("France", "fr-FR"),
                new Region("Germany", "de-DE"),
                new Region("Brazil", "pt-BR"),
                new Region("India", "en-IN"),
                new Region("China", "zh-CN"),
                new Region("Japan", "ja-JP"),
                new Region("Others", "en-ww") //this have to be last
            };
        }

        public Region GetRegion(string value)
        {
            foreach (var region in RegionList)
            {
                if (region.Value == value)
                {
                    return region;
                }
            }

            return RegionList.Last();
        }

        public Region GetDefaultRegion() => RegionList.Last();
    }
}

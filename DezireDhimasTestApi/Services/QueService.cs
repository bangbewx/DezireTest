using DezireDhimasTestApi.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DezireDhimasTestApi.Services
{
    public class QueService : IQueService
    {
        private MQueOne _oneItems;
        private List<MQueTwo> _twoItems;
        private List<MQueThree> _threeItems;
        public QueService()
        {
            _oneItems = new MQueOne();
            _twoItems = new List<MQueTwo>();
            _threeItems = new List<MQueThree>();
        }

        public MQueOne GetOne()
        {
            return _oneItems;
        }

        public List<MQueTwo> GetTwo()
        {
            return _twoItems;
        }

        public List<MQueThree> PostThree()
        {
            return _threeItems;
        }
    }
}

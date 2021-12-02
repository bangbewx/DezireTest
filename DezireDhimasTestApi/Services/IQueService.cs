using DezireDhimasTestApi.Model;
using System.Collections.Generic;

namespace DezireDhimasTestApi.Services
{
    public interface IQueService
    {
        public MQueOne GetOne();
        public List<MQueTwo> GetTwo();
        public List<MQueThree> PostThree();
    }
}

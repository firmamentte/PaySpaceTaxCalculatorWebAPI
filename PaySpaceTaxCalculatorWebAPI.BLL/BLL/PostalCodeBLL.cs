using PaySpaceTaxCalculatorWebAPI.BLL.DataContract;
using PaySpaceTaxCalculatorWebAPI.Data.DAL;
using PaySpaceTaxCalculatorWebAPI.Data.Entities;

namespace PaySpaceTaxCalculatorWebAPI.BLL.BLL
{
    public class PostalCodeBLL
    {
        private readonly PostalCodeDAL PostalCodeDAL;

        public PostalCodeBLL()
        {
            PostalCodeDAL = new();
        }

        public async Task<List<PostalCodeResp>> GetPostalCodes()
        {
            using PaySpaceTaxCalculatorContext _dbContext = new();

            List<PostalCodeResp> _postalCodeResp = new();

            foreach (var item in await PostalCodeDAL.GetPostalCodes(_dbContext))
            {
                _postalCodeResp.Add(FillPostalCodeResp(item));
            }

            return _postalCodeResp;
        }

        private PostalCodeResp FillPostalCodeResp(PostalCode postalCode)
        {
            return new PostalCodeResp()
            {
                PostalCode = postalCode.PostalCodeValue
            };
        }
    }
}

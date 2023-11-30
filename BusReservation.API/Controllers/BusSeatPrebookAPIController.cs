using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserData.Core.IServices;
using UserData.Core.Model;

namespace BusReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusSeatPrebookAPIController : ControllerBase
    {
        private readonly IServices _services;
        public BusSeatPrebookAPIController(IServices services)
        {
            _services = services;
        }
        //        #region Login Detail

        //        //[HttpPost]
        //        //[Route("UserOrAdminLoginVerification")]
        //        //public IActionResult UserOrAdminLoginVerification(UserLogin logindetail)
        //        //{

        //        //}
        //        //#endregion

        #region SignUp Detail
        [HttpPost]
        public IActionResult InsertUserOrAdminDetails(UserOrAdminSignUp signupDetail)
        {

            bool exist = _services.AddSignUpDetails(signupDetail);
            return Ok(exist);
        }
        #endregion
    }
}

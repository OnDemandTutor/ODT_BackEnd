using System;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Tools;
using ODT_Model.DTO.Request;
using ODT_Service.Interface;

namespace ODT_API.Controllers.Wallet
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : BaseController
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            this._walletService = walletService;
        }

        [HttpGet("GetAllWallets")]
        public async Task<IActionResult> GetAllWallets([FromQuery] QueryObject queryObject)
        {
            try
            {
                var wallets = await _walletService.GetAllWalletsAsync(queryObject);
                return CustomResult("Data loaded!", wallets);
            }
            catch (CustomException.DataNotFoundException e)
            {
                return CustomResult(e.Message, HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetWalletById/{id}")]
        public async Task<IActionResult> GetWalletById(long id)
        {
            var wallet = await _walletService.GetWalletByIdAsync(id);

            if (wallet == null)
            {
                return CustomResult("Wallet not found", HttpStatusCode.NotFound);
            }

            return CustomResult("Data loaded!", wallet);
        }

        [HttpGet("GetWalletByUserId/{userId}")]
        public async Task<IActionResult> GetWalletByUserId(long userId)
        {
            var wallet = await _walletService.GetWalletByUserIdAsync(userId);
            if (wallet == null)
            {
                return CustomResult("Wallet not found", HttpStatusCode.NotFound);
            }
            return CustomResult("Data loaded!", wallet);
        }

        [HttpPost("CreateWallet")]
        public async Task<IActionResult> CreateWallet([FromBody] WalletRequest walletRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return CustomResult(ModelState, HttpStatusCode.BadRequest);
                }

                var createdWallet = await _walletService.CreateWalletAsync(walletRequest);
                return CustomResult("Created successfully", createdWallet);
            }
            catch (CustomException.DataNotFoundException e)
            {
                return CustomResult(e.Message, HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("UpdateWallet/{walletId}")]
        public async Task<IActionResult> UpdateWallet(long walletId, [FromBody] WalletRequest walletRequest)
        {
            if (!ModelState.IsValid)
            {
                return CustomResult(ModelState, HttpStatusCode.BadRequest);
            }

            try
            {
                var updateWallet = await _walletService.UpdateWalletAsync(walletRequest, walletId);
                return CustomResult("Update successfully", updateWallet);
            }
            catch (CustomException.DataNotFoundException e)
            {
                return CustomResult(e.Message, HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("DeleteWallet/{walletId}")]
        public async Task<IActionResult> DeleteWallet(long walletId)
        {
            try
            {
                await _walletService.DeleteWalletAsync(walletId);
                return CustomResult("Delete wallet successfully", HttpStatusCode.NoContent);
            }
            catch (CustomException.DataNotFoundException e)
            {
                return CustomResult(e.Message, HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}


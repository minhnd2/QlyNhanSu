namespace ManagerAccount.API.Controllers
{
    using ManagerAccount.Infrastructure;
    using Infrastructure.DataModel;
    using API.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Data;
    using ManagerAccount.API.Common;

    public class AccountsController : ApiController
    {
        private ToolCoreDB db = new ToolCoreDB();
        private string currentAccount = "Admin";

        // GET api/account/5
        /// <summary>
        /// Thực hiện tìm tài khoản theo id
        /// </summary>
        ///<param name="id"></param>
        /// <Modified>
        /// Name                 date           comments
        /// minhnd2              16/06          created
        /// </Modified>
        [HttpGet]
        public IEnumerable<AccountsDTO> Get(int id)
        {
            DataTable tb = db.accounts.GetDataByID(id);
            List<AccountsDTO> list = db.MapDTO<AccountsDTO>(tb);
            return list;
        }

        // GET api/account/getall
        /// <summary>
        /// Thực hiện lấy tất cả dữ liệu
        /// </summary>
        /// <Modified>
        /// Name                 date           comments
        /// minhnd2              16/06          created
        /// </Modified>
        [HttpGet]
        public IEnumerable<AccountsDTO> GetAll()
        {
            DataTable tb = db.accounts.GetData();
            List<AccountsDTO> list = db.MapDTO<AccountsDTO>(tb);
            return list;
        }

        // Post api/account/search
        /// <summary>
        /// Thực hiện tìm kiếm dữ liệu
        /// </summary>
        ///<param name="AccountSearchDTO"></param>
        /// <Modified>
        /// Name                 date           comments
        /// minhnd2              16/06          created
        /// </Modified>
        [HttpPost]
        public AccountsResultDTO Search(AccountSearchDTO accounts)
        {
            int? totalRow = 0;
            DataTable tb = db.sAccountSearch.GetData(accounts.Name,accounts.Mail,accounts.Phone, accounts.PageIndex, accounts.PageSize, ref totalRow);
            List<AccountsDTO> list = db.MapDTO<AccountsDTO>(tb);

            AccountsResultDTO result = new AccountsResultDTO();
            result.TotalRow = totalRow.Value;
            result.ListAccount = list;
            return result;
        }

        // Post api/account/insert
        /// <summary>
        /// Thực hiện ghi dữ liệu vào bảng account
        /// </summary>
        ///<param name="AccountsDTO"></param>
        /// <Modified>
        /// Name                 date           comments
        /// minhnd2              16/06          created
        /// </Modified>
        [HttpPost]
        public IHttpActionResult Insert(AccountsDTO accounts)
        {
            try
            {
                accounts.SecretKey = Guid.NewGuid().ToString("n").Substring(0, 8);
                accounts.PassWord = Md5.Encrypt(accounts.PassWord, accounts.SecretKey);
                db.accounts.Insert(accounts.Name, accounts.Mail, accounts.Phone, accounts.PassWord, DateTime.Now, currentAccount, null, null, null, null, true, true, false, accounts.SecretKey);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // Post api/account/update
        /// <summary>
        /// Thực hiện cập nhật dữ liệu vào bảng account
        /// </summary>
        ///<param name="AccountsDTO"></param>
        /// <Modified>
        /// Name                 date           comments
        /// minhnd2              16/06          created
        /// </Modified>
        [HttpPost]
        public IHttpActionResult Update(AccountsDTO accounts)
        {
            try
            {
                if(accounts.ID < 1)
                {
                    return BadRequest("Không có bản ghi để chỉnh sửa!");
                }
                db.accounts.UpdateQuery(accounts.Name, accounts.Mail, accounts.Phone, DateTime.Now, currentAccount, accounts.ID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // Post api/account/delete
        /// <summary>
        /// Thực hiện xóa dữ liệu vào bảng account
        /// </summary>
        ///<param name="AccountsDTO"></param>
        /// <Modified>
        /// Name                 date           comments
        /// minhnd2              16/06          created
        /// </Modified>
        [HttpPost]
        public IHttpActionResult Delete(AccountsDTO accounts)
        {
            try
            {
                if (accounts.ID < 1)
                {
                    return BadRequest("Không có bản ghi để xóa!");
                }
                db.accounts.DeleteQuery(DateTime.Now, currentAccount, accounts.ID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // Post api/account/login
        /// <summary>
        /// Thực hiện xóa dữ liệu vào bảng account
        /// </summary>
        ///<param name="AccountsDTO"></param>
        /// <Modified>
        /// Name                 date           comments
        /// minhnd2              16/06          created
        /// </Modified>
        [HttpPost]
        public IHttpActionResult Login(AccountsDTO accounts)
        {
            try
            {
                DataTable tb = db.accounts.GetDataByEmail(accounts.Mail);
                List<AccountsDTO> list = db.MapDTO<AccountsDTO>(tb);
                if(list.Count < 1)
                {
                    return BadRequest("Email không đúng, vui lòng thử lại!");
                }
                AccountsDTO acc = list[0];
                string inputPass = Md5.Encrypt(accounts.PassWord, acc.SecretKey);
                if (inputPass.Equals(acc.PassWord))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Sai mật khẩu, vui lòng thử lại!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.BUS;
using MicroService_QLBanDienThoai.Models;
namespace MicroService_QLBanDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : BaseController
    {

        public IActionResult Index(string thongbao)
        {
            //Thông báo
            if (thongbao != null)
            {
                ViewBag.ThongBao = thongbao;
            }
            //List          
            AccountBUS bus = new AccountBUS();
            List<Account> list = bus.GetAccount();
            //ViewBag
            ViewBag.TrangThai = "index";
            return View(list);
        }
        [HttpPost]
        public IActionResult CreateAccount(string item_them_AccountID, string item_them_AccountName, string item_them_Password, string item_them_Email, string item_them_PhoneNumber, string item_them_Address, string item_them_AccountType)
        {
            AccountBUS Account = new AccountBUS();
            string thongbao = Account.CreateAccount(item_them_AccountID, item_them_AccountName, item_them_Password, item_them_PhoneNumber, item_them_Email, item_them_Address, item_them_AccountType);
            return RedirectToAction("Index", "Account", new { thongbao = thongbao });
        }
        public IActionResult EditAccount(string item_sua_AccountID, string item_sua_AccountName, string item_sua_Password, string item_sua_PhoneNumber, string item_sua_Email, string item_sua_Address, string item_sua_AccountType)
        {
            AccountBUS Account = new AccountBUS();
            string thongbao = Account.EditAccount(item_sua_AccountID, item_sua_AccountName, item_sua_Password, item_sua_PhoneNumber, item_sua_Email, item_sua_Address, item_sua_AccountType);
            return RedirectToAction("Index", "Account", new { thongbao = thongbao });
        }
        public IActionResult DeleteAccount(string AccountId)
        {
            AccountBUS Account = new AccountBUS();
            string thongbao = Account.DeleteAccount(AccountId);
            return RedirectToAction("Index", "Account", new { thongbao = thongbao });
        }
        public IActionResult Search(string search)
        {
            AccountBUS bus = new AccountBUS();
            List<Account> list = bus.SearchAccount(search);
            ViewBag.TrangThai = "search";
            ViewBag.Search = search;
            return View("Index", list);
        }

    }
}
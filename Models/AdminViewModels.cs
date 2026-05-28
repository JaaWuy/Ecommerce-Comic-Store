using System;
using System.Collections.Generic;
using System.Linq;

namespace DoAnWeb.Models
{
    // ViewModel cho Dashboard
    public class DashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<Order> RecentOrders { get; set; }
        public List<Truyen> LowStockProducts { get; set; }
    }

    // ViewModel cho quản lý user
    public class UserManagementViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
    }

    // ViewModel cho tạo user
    public class CreateUserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
    }

    // ViewModel cho sửa user
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public List<string> CurrentRoles { get; set; }
        public string[] SelectedRoles { get; set; }
    }
}

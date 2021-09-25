using Engineers.Models;
using System.Collections.Generic;
using System.Linq;
using Engineers.Api.Models;

namespace Engineers
{
    public static class Expansion
    {
        public static User ConverToUser(this ApiUser apiUser)
        {
            User user = new();

            user.FullName = apiUser.FullName;
            user.UserName = apiUser.UserName;
            user.Image = apiUser.Image;
            user.PhoneNumber = apiUser.PhoneNumber;
            user.Comments = apiUser.Comments;
            user.City = apiUser.City;
            user.Age = apiUser.Age;
            user.Qualification = apiUser.Qualification;
            user.Description = apiUser.Description;
            user.Balance = apiUser.Balance;
            user.Role = apiUser.Role;

            return user;
        }

        public static bool IsValid(this ApiUser user)
        {
            if (user.FullName != null
                && user.UserName != null && user.Role != null
                && user.PhoneNumber != null) return true;

            return false;
        }

        public static User UpdateInfo(this User user, User updateUser)
        {
            user.FullName = updateUser.FullName;
            user.UserName = updateUser.UserName;
            user.Image = updateUser.Image;
            user.PhoneNumber = updateUser.PhoneNumber;
            user.Comments = updateUser.Comments;
            user.City = updateUser.City;
            user.Age = updateUser.Age;
            user.Qualification = updateUser.Qualification;
            user.Description = updateUser.Description;
            user.Balance = updateUser.Balance;
            user.Role = updateUser.Role;
            user.Orders = updateUser.Orders;

            return user;
        }

        public static ApiUser ConverToApiUser(this User user)
        {
            ApiUser apiUser = new();

            apiUser.FullName = user.FullName;
            apiUser.UserName = user.UserName;
            apiUser.Image = user.Image;
            apiUser.PhoneNumber = user.PhoneNumber;
            apiUser.Comments = user.Comments;
            apiUser.City = user.City;
            apiUser.Age = user.Age;
            apiUser.Qualification = user.Qualification;
            apiUser.Description = user.Description;
            apiUser.Balance = user.Balance;
            apiUser.Role = user.Role;

            return apiUser;
        }


        public static Order ConverToOrder(this ApiOrder apiOrder)
        {
            Order order = new()
            {
                Id = apiOrder.Id,
                Name = apiOrder.Name,
                Description = apiOrder.Description,
                Images = apiOrder.Images,
                Cost = apiOrder.Cost,
                Longitude = apiOrder.Longitude,
                Latitude = apiOrder.Latitude,
                State = apiOrder.State,
                Created_at = apiOrder.Created_at,
                Updated_at = apiOrder.Updated_at,
                OwnerId = apiOrder.OwnerId,
                InWorkId = apiOrder.InWorkId
            };

            return order;
        }

        public static List<ApiOrder> ConverToListApiOrder(this List<Order> orders)
        {
            List<ApiOrder> apiOrders = new();

            orders.ForEach(order => 
            {
                apiOrders.Add(order.ConverToApiOrder());
            });

            return apiOrders;
        }

        public static Order UpdateInfo(this Order order, Order updateOrder)
        {
            order.Id = updateOrder.Id;
            order.Name = updateOrder.Name;
            order.Description = updateOrder.Description;
            order.Images = updateOrder.Images;
            order.Cost = updateOrder.Cost;
            order.Longitude = updateOrder.Longitude;
            order.Latitude = updateOrder.Latitude;
            order.State = updateOrder.State;
            order.Created_at = updateOrder.Created_at;
            order.Updated_at = updateOrder.Updated_at;
            order.OwnerId = updateOrder.OwnerId;
            order.InWorkId = updateOrder.InWorkId;

            return order;
        }

        public static ApiOrder ConverToApiOrder(this Order order)
        {
            if (order is null) return null;

            if (order.Owner is null)
                return null;

            ApiOrder apiOrder = new()
            {
                Id = order.Id,
                Name = order.Name,
                Description = order.Description,
                Images = order.Images,
                Cost = order.Cost,
                Longitude = order.Longitude,
                Latitude = order.Latitude,
                State = order.State,
                Created_at = order.Created_at,
                Updated_at = order.Updated_at,
                OwnerId = order.OwnerId,
                OwnerName = order.Owner.UserName,
                InWorkId = order.InWorkId
            };

            return apiOrder;
        }

        public static bool IsValid(this ApiOrder apiOrder)
        {
            if (apiOrder.Name is null ||
                    apiOrder.Description is null ||
                    apiOrder.Cost == 0 ||
                    apiOrder.Longitude == 0 ||
                    apiOrder.Latitude == 0 ||
                    apiOrder.OwnerId is null)
                return false;

            return true;
        }

        public static Order ConverToOrder(this Blueprint blueprint)
        {
            Order order = new();

            order.Name = blueprint.Name;
            order.Description = blueprint.Description;
            order.Cost = blueprint.Cost;

            return order;
        }

        public static string ConvertListToString(this List<string> array)
        {
            string result = "";

            array.ForEach(item =>
            {
                if (item == array.Last()) result += item;
                else result += $"{item};";
            });

            return result;
        }

        public static List<string> ConvertStringToList(this string path)
        {
            List<string> result = new();

            result.AddRange(path.Split(';'));

            return result;
        }


        public static Response ReturnResponse(this Response resp, string text, int code = 400, object data = null, bool success = false)
        {
            return new Response()
            {
                Code = code,
                Text = text,
                Data = data,
                Success = success
            };
        }

        public static Response ReturnOKResponse(this Response resp, object data = null, string text = "OK", int code = 200, bool success = true)
        {
            return new Response()
            {
                Code = code,
                Text = text,
                Data = data,
                Success = success
            };
        }

        public static Response ReturnBADResponse(this Response resp, string text = "BAD", object data = null, int code = 400, bool success = false)
        {
            return new Response()
            {
                Code = code,
                Text = text,
                Data = data,
                Success = success
            };
        }
    }
}

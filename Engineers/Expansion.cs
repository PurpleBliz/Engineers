using Engineers.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            user.MinDescription = apiUser.MinDescription;
            user.City = apiUser.City;
            user.Age = apiUser.Age;
            user.Education = apiUser.Education;
            user.Fulldescription = apiUser.Fulldescription;
            user.Balance = apiUser.Balance;
            user.Role = apiUser.Role;
            user.Orders = apiUser.Orders;

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
            user.MinDescription = updateUser.MinDescription;
            user.City = updateUser.City;
            user.Age = updateUser.Age;
            user.Education = updateUser.Education;
            user.Fulldescription = updateUser.Fulldescription;
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
            apiUser.MinDescription = user.MinDescription;
            apiUser.City = user.City;
            apiUser.Age = user.Age;
            apiUser.Education = user.Education;
            apiUser.Fulldescription = user.Fulldescription;
            apiUser.Balance = user.Balance;
            apiUser.Role = user.Role;
            apiUser.Orders = user.Orders;

            return apiUser;
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

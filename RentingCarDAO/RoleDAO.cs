﻿using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarDAO
{
    public class RoleDAO
    {
        private static RoleDAO instance = null;
        private static exe201Context db = null;

        public RoleDAO()
        {
            db = new exe201Context();
        }
        public static RoleDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoleDAO();
                }
                return instance;
            }
        }

        public List<Role> GetRoles()
        {
            try
            {
                return db.Set<Role>().ToList();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public Role? GetRoleById(long id)
        {
            try
            {
                return db.Set<Role>().Where(x => x.RoleId == id).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public bool Add(Role role)
        {
            try
            {
                Role existRole = db.Set<Role>()
                    .FirstOrDefault(x => x.RoleName.Equals(role.RoleName));
                if (existRole != null)
                {
                    return false;
                }
                var checkExist = db.Accounts.Find(role.RoleId);
                if (checkExist != null)
                {
                    db.Entry(checkExist).State = EntityState.Detached;
                }
                db.Add(role);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public bool Update(Role role)
        {
            try
            {
                Role existRole = db.Set<Role>()
                    .FirstOrDefault(x => x.RoleName.Equals(role.RoleName));
                if (existRole != null)
                {
                    return false;
                }
                var checkExist = db.Accounts.Find(role.RoleId);
                if (checkExist != null)
                {
                    db.Entry(checkExist).State = EntityState.Detached;
                }
                db.Update(role);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}

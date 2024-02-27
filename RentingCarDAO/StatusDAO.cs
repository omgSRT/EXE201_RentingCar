using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace RentingCarDAO
{
    public class StatusDAO
    {
        private static StatusDAO instance = null;
        private static exe201Context db = null;

        public StatusDAO()
        {
            db = new exe201Context();
        }
        public static StatusDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StatusDAO();
                }
                return instance;
            }
        }

        public List<Status> GetStatuses()
        {
            try
            {
                return db.Set<Status>().ToList();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public Status? GetStatusById(long id)
        {
            try
            {
                return db.Set<Status>().Where(x => x.StatusId == id).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public bool Update(Status status)
        {
            try
            {
                Status existStatus = db.Set<Status>()
                    .FirstOrDefault(x => x.StatusName.Equals(status.StatusName));
                if (existStatus != null)
                {
                    return false;
                }
                var checkExist = db.Accounts.Find(status.StatusId);
                if (checkExist != null)
                {
                    db.Entry(checkExist).State = EntityState.Detached;
                }
                db.Update(status);
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

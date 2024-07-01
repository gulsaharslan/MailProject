using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfMessagesDal : GenericRepository<Messages>, IMessagesDal
    {
        private readonly MailContext _mailContext;

        public EfMessagesDal(MailContext context) : base(context)
        {
            _mailContext = context;
        }

        public int CountMessage(int id)
        {
           return _mailContext.Messages.Where(x=>x.ReceiverUserId == id).Where(y=>y.IsRead==false).Where(z => z.IsDraft == false).Where(t => t.IsDeleted == false).Count();
        }

        public Messages GetByMessageId(int id)
        {
            var value=_mailContext.Messages.Where(x=>x.MessageId== id).Include(y=>y.SenderUser).Include(z=>z.ReceiverUser).FirstOrDefault();
            return value;
            
        }

        public List<Messages> GetListBySentMessages(int id)
        {
            var values = _mailContext.Messages.Where(x => x.SenderUserId == id).Where(z => z.IsDraft == false).Where(t => t.IsDeleted == false).Include(y => y.ReceiverUser).OrderByDescending(z => z.SentAt).ToList();
            return values;
        }

        public List<Messages> GetListByUserId(int id)
        {
          var values= _mailContext.Messages.Where(x=>x.ReceiverUserId==id).Where(z => z.IsDraft == false).Where(t => t.IsDeleted == false).Include(y=>y.SenderUser).OrderByDescending(z=>z.SentAt).ToList();
            return values;
        }

        public List<Messages> GetListDeletedMail(int id)
        {
            var values = _mailContext.Messages
                    .Where(x => x.AppUserId == id && x.IsDeleted)
                    .ToList();
            return values;
        }

        public List<Messages> GetListDraftMail(int id)
        {
            
                var values = _mailContext.Messages
                    .Where(x => x.AppUserId == id && x.IsDraft && !x.IsDeleted)
                    .ToList();
                return values;
            
        }

    }
}

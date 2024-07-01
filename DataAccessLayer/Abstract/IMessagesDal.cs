using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IMessagesDal : IGenericDal<Messages>
    {
        List<Messages> GetListByUserId(int id);

        Messages GetByMessageId(int id);

        List<Messages> GetListBySentMessages(int id);

       int CountMessage(int id);

        List<Messages> GetListDraftMail(int id);
        List<Messages> GetListDeletedMail(int id);

    }
}

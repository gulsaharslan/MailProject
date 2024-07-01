using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMessagesService:IGenericService<Messages>
    {
        List<Messages> TGetListByUserId(int id);
        Messages TGetByMessageId(int id);
        List<Messages> TGetListBySentMessages(int id);

        int TCountMessage(int id);
        List<Messages> TGetListDraftMail(int id);
        List<Messages> TGetListDeletedMail(int id);
    }
}

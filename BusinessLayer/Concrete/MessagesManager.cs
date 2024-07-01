using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MessagesManager : IMessagesService
    {
        private readonly IMessagesDal _messagesDal;

        public MessagesManager(IMessagesDal messagesDal)
        {
            _messagesDal = messagesDal;
        }

        public int TCountMessage(int id)
        {
            return _messagesDal.CountMessage(id);
        }

        public void TDelete(int id)
        {
           _messagesDal.Delete(id);
        }

        public Messages TGetById(int id)
        {
          return  _messagesDal.GetById(id);
        }

        public Messages TGetByMessageId(int id)
        {
            return _messagesDal.GetByMessageId(id);
        }

        public List<Messages> TGetListAll()
        {
           return _messagesDal.GetListAll();
        }

        public List<Messages> TGetListBySentMessages(int id)
        {
            return _messagesDal.GetListBySentMessages(id);
        }

        public List<Messages> TGetListByUserId(int id)
        {
            return _messagesDal.GetListByUserId(id);
        }

        public List<Messages> TGetListDeletedMail(int id)
        {
            return _messagesDal.GetListDeletedMail(id);
        }

        public List<Messages> TGetListDraftMail(int id)
        {
            return _messagesDal.GetListDraftMail(id);
        }

        public void TInsert(Messages entity)
        {
           _messagesDal.Insert(entity);
        }

        public void TUpdate(Messages entity)
        {
           _messagesDal.Update(entity);
        }
    }
}

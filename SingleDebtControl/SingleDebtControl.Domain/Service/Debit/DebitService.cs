using AutoMapper;
using SingleDebtControl.Domain.Service.Debit.Dto;
using SingleDebtControl.Domain.Service.Debit.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils.Message;

namespace SingleDebtControl.Domain.Service.Debit
{
    public class DebitService : IDebitService
    {
        private readonly IMapper _mapper;
        private readonly IDebitRepository _debitRepository;
        private readonly IMessageErrorService _messageError;

        public DebitService(IMapper mapper, IDebitRepository debitRepository, IMessageErrorService messageError)
        {
            _mapper = mapper;
            _debitRepository = debitRepository;
            _messageError = messageError;
        }

        public async Task<bool> AddTax()
        {
            var dateNow = DateTime.Now;
            var dateCurrentMonth = new DateTime(dateNow.Year, dateNow.Month, 1);

            var LastDayMonth = dateCurrentMonth.AddMonths(1).AddDays(-1).Day;
            if (dateNow.Day != LastDayMonth)
                return _messageError.AddWithReturn<bool>("Opss... só é permitido adicionar os juros da divida no ultimo dia do mes!");

            var debitEntity = _debitRepository.Get(x => x.Active == true).FirstOrDefault();
            if (debitEntity == null)
                return _messageError.AddWithReturn<bool>("Não existe nenhum debito ativo!");

            if (debitEntity.CreationDate.Day == dateNow.Day)
                return _messageError.AddWithReturn<bool>("N");

            debitEntity.LastUpdateDate = DateTime.Now;
            debitEntity.Active = false;
            _debitRepository.Put(debitEntity);

            var value = debitEntity.Value;
            var percent = 1.0 / 100.0;

            var debitDto = new DebitDto
            {
                Value = ((long)(value + (percent * value))),
                Active = true,
                Description = debitEntity.Description,
                CreationDate = DateTime.Now,
            };

            _debitRepository.Post(_mapper.Map<DebitEntity>(debitDto));

            return true;
        }

        public IEnumerable<DebitDto> Get()
        {
            return _mapper.Map<IEnumerable<DebitDto>>(_debitRepository.Get());
        }

        public IEnumerable<DebitDto> Get(bool active)
        {
            return _mapper.Map<IEnumerable<DebitDto>>(_debitRepository.Get(x => x.Active == true));
        }

        public int Post(DebitDto dto)
        {
            var debitEntity = _debitRepository.Get(x => x.Active == true).FirstOrDefault();
            if (debitEntity != null)
                return _messageError.AddWithReturn<int>("Já existe uma divida aberta!");

            dto.LastUpdateDate = DateTime.Now;
            dto.CreationDate = DateTime.Now;

            return _debitRepository.Post(_mapper.Map<DebitEntity>(dto));
        }

        public bool Put(DebitDto dto)
        {
            var debitEntity = _debitRepository.Get(x => x.Id == dto.Id).FirstOrDefault();
            if (debitEntity == null)
                return _messageError.AddWithReturn<bool>("Não localizamos a divida para realizarmos o update!");

            dto.LastUpdateDate = DateTime.Now;
            _debitRepository.Put(_mapper.Map<DebitEntity>(dto));

            return true;
        }
    }
}

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
        private readonly INotification _messageError;

        public DebitService(IMapper mapper, IDebitRepository debitRepository, INotification messageError)
        {
            _mapper = mapper;
            _debitRepository = debitRepository;
            _messageError = messageError;
        }

        public async Task<bool> AddTax()
        {
            var dateNow = DateTime.Now;
            var dateCurrentMonth = new DateTime(dateNow.Year, dateNow.Month, 1);

            var lastDayMonth = dateCurrentMonth.AddMonths(1).AddDays(-1).Day;
            if (dateNow.Day != lastDayMonth)
                return _messageError.AddWithReturn<bool>("Opss... só é permitido adicionar os juros da divida no ultimo dia do mes!", "error");

            var debitEntity = _debitRepository.Get(x => x.Active == true).FirstOrDefault();
            if (debitEntity == null)
                return _messageError.AddWithReturn<bool>("Não existe nenhum debito ativo!", "error");

            if (debitEntity.CreationDate.Day == dateNow.Day)
                return _messageError.AddWithReturn<bool>("Ops... já foi adicionado o juros do mês!", "error");

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
            if (dto == null)
                return _messageError.AddWithReturn<int>("Ops... dados não informados para realizar o cadastro!", "error");

            if (dto.IsValid(_messageError))
                return default;

            var debitEntity = _debitRepository.Get(x => x.Active == true).FirstOrDefault();
            if (debitEntity != null)
                return _messageError.AddWithReturn<int>("Ops... já existe uma divida ativa!", "error");

            dto.LastUpdateDate = DateTime.Now;
            dto.CreationDate = DateTime.Now;

            return _debitRepository.Post(_mapper.Map<DebitEntity>(dto));
        }

        public bool Put(DebitDto dto)
        {
            if (dto == null)
                return _messageError.AddWithReturn<bool>("Ops... dados não informados para realizar update!", "error");

            if (dto.Id <= 0)
                return _messageError.AddWithReturn<bool>("Ops... é obrigatório informar o debito!", "error");

            if (dto.IsValid(_messageError))
                return default;

            var debitEntity = _debitRepository.Get(x => x.Id == dto.Id).FirstOrDefault();
            if (debitEntity == null)
                return _messageError.AddWithReturn<bool>("Não localizamos a divida para realizarmos o update!", "error");

            dto.LastUpdateDate = DateTime.Now;
            _debitRepository.Put(_mapper.Map<DebitEntity>(dto));

            return true;
        }
    }
}

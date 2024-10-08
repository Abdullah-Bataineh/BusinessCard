﻿using BusinessCardApi.Model;

namespace BusinessCardApi.Repository
{
    public interface IBusinessCardRepository
    {
        public Task CreateBusinessCard(BusinessCard businessCard);
        public Task<IEnumerable<BusinessCard>> GetAllBusinessCard();
        public Task DeleteBusinessCard(int id);

    }
}

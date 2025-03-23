using System;
using System.Collections.Generic;
using APPZ_lab1_v6.Models.Interfaces;
using APPZ_lab1_v6.Services;

namespace APPZ_lab1_v6.Models.Environments
{
    public class PetShop : LivingEnvironment, ICleanable
    {
        private readonly string _shopName;
        public override string Name => $"Зоомагазин \"{_shopName}\"";
        public override bool NeedsCleaning => true;
        public DateTime LastCleaningTime { get; set; }

        public PetShop(string shopName)
        {
            _shopName = shopName;
            LastCleaningTime = DateTime.Now;
        }
    }
}